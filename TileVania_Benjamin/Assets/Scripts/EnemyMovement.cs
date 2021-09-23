using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Config
    [SerializeField] float moveSpeed;

    // Cache
    Rigidbody2D myRigidBody;
    BoxCollider2D myGroundChecker;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myGroundChecker = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveVelocity = new Vector2(moveSpeed, 0f);

        myRigidBody.velocity = moveVelocity;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            transform.localScale = new Vector2(Mathf.Sign(-myRigidBody.velocity.x), 1f);
            moveSpeed = -moveSpeed;
        }
    }
}
