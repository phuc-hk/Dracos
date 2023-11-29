using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowing : MonoBehaviour
{
    public float force = 10f; // Force applied to the axe for movement
    public float torque = -360f; // Torque applied to the axe for rotation
    public int damage = 3;
    private Rigidbody2D rb; // Reference to the Rigidbody component

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody2D>();

        Thowing();
    }

    

    void Update()
    {
        // Move the axe along the x-axis at a constant speed
        //transform.Translate(force * Time.deltaTime, 0, 0);
        //Thowing();
        // Rotate the axe around its own axis
        transform.Rotate(0, 0, torque * Time.deltaTime);
    }

    private void Thowing()
    {
        //    // Apply a force to the axe to move it along the x-axis
        rb.AddForce(new Vector2(force, 0), ForceMode2D.Impulse);

        //    // Apply torque to the axe to make it spin
        //    rb.AddTorque(0, 0, torque, ForceMode.Impulse);
    }
    //private void OnTriggerEnter2D(Collider other)
    //{
    //    CombatTarget combatTarget = other.GetComponent<CombatTarget>();
    //    if (combatTarget == null) return;
    //    combatTarget.GetComponent<Health>().TakeDamage(damage);
    //    Destroy(gameObject, 1f);
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CombatTarget combatTarget = collision.GetComponent<CombatTarget>();
        if (combatTarget == null) return;
        combatTarget.GetComponent<Health>().TakeDamage(damage);
        Destroy(gameObject, 1f);
    }
}
