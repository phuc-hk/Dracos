using Assets.FantasyMonsters.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float patrolRange = 5f;
    public float chaseRange = 3f;

    private Monster monster;
    private Rigidbody2D rb;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingRight = true;
    private Vector3 faceLeft;
    private Vector3 faceRight;

    private void Awake()
    {
        monster = GetComponent<Monster>();        
        rb = GetComponent<Rigidbody2D>();       
    }

    private void Start()
    {
        monster.SetState(MonsterState.Walk);
        startPos = transform.position;
        endPos = new Vector3(startPos.x + patrolRange, startPos.y, startPos.z);
        faceRight = transform.localScale;
        faceLeft = faceRight;
        faceLeft.x *= -1;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > chaseRange)
        {
            Patrol();
        }
        else
        {
            Chase();
        }
    }

    private void Patrol()
    {
        if (movingRight)
        {
            rb.velocity = transform.right * moveSpeed;
            if (transform.position.x >= endPos.x)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            rb.velocity = -transform.right * moveSpeed;
            if (transform.position.x <= startPos.x)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    private void Chase()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= 2)
        {
            //EnemyAttack enemyAttack = GetComponent<EnemyAttack>();
            //if (enemyAttack != null)
            //{
            //    enemyAttack.Attack();
            //}
            rb.velocity = Vector3.zero;
        }

        if (direction.x > 0)
        {
            transform.localScale = faceRight;
        }
        else if (direction.x < 0)
        {
            transform.localScale = faceLeft;
        }
    }

    private void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
