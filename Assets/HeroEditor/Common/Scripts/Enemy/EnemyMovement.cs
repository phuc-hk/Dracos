using Assets.FantasyMonsters.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Monster monster;
    public Transform player;
    public float moveSpeed = 2f;
    public float patrolRange = 5f;

    private Rigidbody2D rb;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingRight = true;

    private void Awake()
    {
        monster = GetComponent<Monster>();
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        endPos = startPos + new Vector3(patrolRange, 0f, 0f);
    }

    private void Start()
    {
        monster.SetState(MonsterState.Walk);
    }

    private void Update()
    {
       // Vector3 direction = player.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (movingRight)
        {
            rb.velocity = transform.right * moveSpeed;
            if (transform.position.x >= endPos.x)
            {
                movingRight = false;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }
        else
        {
            rb.velocity = -transform.right * moveSpeed;
            if (transform.position.x <= startPos.x)
            {
                movingRight = true;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }
    }
}
