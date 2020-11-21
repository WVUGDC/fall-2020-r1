using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AiScript : MonoBehaviour
{
    /* IMPORTANT!!!
    If you want to make any changes to this script, please duplicate this script
    and rename the new script for whatever specific AI is. This is mainly for standard
    enemies who just see and attack. If there are any bugs or problems with the script
    please inform either London Bowen or Soren Kowalski.
    */
    #region Variables
    private enum AIState
    {
        Idle,
        Roaming,
        Follow,
        Attack,
    }

    private Transform target;
    private Animator anim;

    [Tooltip("The walk speed of the AI.")]
    public float speed;

    [Tooltip("How many seconds before the AI can deal damgage (not the same as attacking).")]
    public float damageCooldown;
    private float damageTimer;

    [Tooltip("How much damage the AI deals to the player.")]
    public float attackDamage = 15;
    [Tooltip("How far away the AI can attack from.")]
    public float attackRange = 1f;

    [Tooltip("The area in which the AI can wander in. Set to 0 if the AI does not move.")]
    public float wanderRadius = 1.5f;

    [Tooltip("ALWAYS SET THIS TO WALLS unless there is a special reason or condition for the AI.")]
    public LayerMask layerMask;

    private Rigidbody2D rb;

    private AIState _currentState;
    #endregion

    #region AI Functions
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _currentState = AIState.Idle;
        rb = GetComponent<Rigidbody2D>();
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
                    anim.SetBool("Attack", false);
                    break;
                }
            case AIState.Roaming:
                {
                    break;
                }
            case AIState.Follow:
                {
                    MoveTo(target.position);
                    break;
                }
            case AIState.Attack:
                {
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

    private void WaitTime()
    {

    }

    private void Wander()  //Set Random Point. Then go to random point and set state to roaming.
    {

    }

    private void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
    #endregion
    #region AI Checks and Other Things (DO NOT CHANGE)
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

    private bool IsMoving()
    {
        if (rb.velocity.magnitude > 0)
            return true;
        else
            return false;
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