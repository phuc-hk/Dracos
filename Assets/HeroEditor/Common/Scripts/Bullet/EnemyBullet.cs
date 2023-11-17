using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 1f;
    public float bulletGravity = 5f;
    public GameObject explosionPrefab;
    private Transform _transform;
    private Vector3 _targetPosition;
    private float _firingForce;

    private void Awake()
    {
        _transform = transform;
    }

    public void Fire(Vector3 targetPosition, float firingForce)
    {
        _targetPosition = targetPosition;
        _firingForce = firingForce;

        // Calculate the initial velocity of the bullet.
        Vector3 direction = _targetPosition - _transform.position;
        float distance = direction.magnitude;
        float time = distance / bulletSpeed;
        float verticalVelocity = (bulletGravity * time);
        float horizontalVelocity = distance / time;
        Vector3 velocity = direction.normalized * horizontalVelocity + Vector3.up * verticalVelocity;

        // Apply the initial velocity to the bullet.
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = velocity;

        // Destroy the bullet after some time.
        Destroy(gameObject, time + 0.3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        explosionPrefab.SetActive(true);
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health == null) return;
            health.TakeDamage(2);
        }
    }
}
