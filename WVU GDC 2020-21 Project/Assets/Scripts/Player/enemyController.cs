using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    [Header("Character Attributes:")]
    //base speed of character
    public float MOVEMENT_BASE_SPEED = 1.0f;

    [Space]
    [Header("Character Statistics:")]
    //speed of character
    public float movementSpeed = 1.0f;
    //holds the direction of the character
    public Vector3 movementDirection;
    public Transform target;

    [Space]
    [Header("References:")]
    //basic physics for the character
    public Rigidbody2D rb;
    //Animation for character
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (target.position - transform.position).normalized * movementSpeed * Time.deltaTime;
    }
}
