using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SlimeScript : MonoBehaviour
{
    #region Variables
    private enum AIState
    {
        Idle,
        Roaming,
        Follow,
        Alert,
        Investigate,
        Attack,
    }
    private Transform target;

    public float speed;

    public Animator anim;
    private Rigidbody2D rb;

    public float damageCooldown;
    private float damageTimer;
    public float attackDamage = 15;
    public float attackRange = 1f;
    public float walkRadius = 1.5f;

    public LayerMask layerMask;

    private AIState _currentState;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //StartCoroutine(WaitIdle());
        _currentState = AIState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (damageTimer >= 0)
        {
            damageTimer -= Time.deltaTime;
        }
        if (RemainingDistance(target, this.transform) <= 1f && _currentState != AIState.Attack && WallCheck())
        {
            _currentState = AIState.Attack;
        }

        switch (_currentState)
        {
            default:
            case AIState.Idle:
                {
                    Follow(false);
                    anim.SetBool("Attack", false);
                    break;
                }
            case AIState.Roaming:
                {
                    Wander();
                    break;
                }
            case AIState.Follow:
                {
                    Follow(true);
                    break;
                }
            case AIState.Alert:
                {
                    break;
                }
            case AIState.Investigate:
                {
                    break;
                }
            case AIState.Attack:
                {
                    Follow(false);
                    Attack();
                    break;
                }
        }
    } 

    private void OnTriggerStay2D(Collider2D col) //This is when player is in range to be followed.
    {
        if (col.tag == "Player")
        {
            _currentState = AIState.Follow;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            _currentState = AIState.Idle;
        }
    }

    /*private IEnumerator WaitIdle()
    {
        float waitTime = Random.Range(3, 5);
        yield return new WaitForSeconds(waitTime);

        if (!HasDestination() || _currentState == AIState.Idle)
        {
            InvokeRepeating("Wander", 1, waitTime);
        }
        else
        {
            CancelInvoke("Wander");
        }
        StartCoroutine(WaitIdle());
    }*/
    private void WaitTime()
    {

    }

    private void Wander()  //Set Random Point. Then go to random point and set state to roaming.
    {
        //Random Point
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        transform.position = Vector2.MoveTowards(transform.position, randomDirection, speed * Time.deltaTime);
        //randomDirection += transform.position;
        //_currentState = AIState.Roaming;

        //Go to Random Point
    }

    private void Follow(bool check)
    {
        if (check == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }      
    }

    private void Attack()
    {
        if (RemainingDistance(target, this.transform) <= 1)
        {
            anim.SetBool("Attack", true);

            if (RemainingDistance(target, this.transform) <= attackRange && damageTimer <= 0)
            {
                target.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                damageTimer = damageCooldown;
            }
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    private void Lunge(float time)
    {
        if (WallCheck())
            StartCoroutine(LungeTime(time));
    }
    
    private IEnumerator LungeTime(float time)
    {   
        Vector3 AiPosition = transform.position;
        Vector3 TargetPosition = (target.position - AiPosition).normalized * 1f;

        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            this.transform.position = Vector2.Lerp(AiPosition, TargetPosition + AiPosition, t);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, walkRadius);
    }

    #region AI Commands (Will move later)
    private bool HasDestination()
    {
        if (_currentState == AIState.Follow || _currentState == AIState.Roaming)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float RemainingDistance(Transform point1, Transform point2)
    {
        return Mathf.Sqrt(Mathf.Pow(point2.position.x - point1.position.x, 2) + Mathf.Pow(point2.position.y - point1.position.y, 2));
    }

    private bool WallCheck()
    {
        Vector2 TargetDirection = (target.position - transform.position).normalized * 1f;

        RaycastHit2D ray = Physics2D.Raycast(transform.position, TargetDirection, 1, layerMask);

        Debug.DrawRay(transform.position, TargetDirection, Color.red, 1);
        return !ray;
    }
    #endregion
}