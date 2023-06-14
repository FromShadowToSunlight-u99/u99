using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public string npcName;
    public int health;
    public float movementSpeed;
    public float fieldOfViewAngle;
    public float attackRange;
    public float detectionRange;

    private Transform target;
    private Rigidbody rb;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * movementSpeed);

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionRange)
        {
            if (Vector3.Angle(transform.forward, direction) <= fieldOfViewAngle && distanceToTarget <= attackRange)
            {
                // Attack the player
                Attack();
            }
        }
    }

    private void Attack()
    {
        // Attack logic goes here
        Debug.Log(npcName + " is attacking the player.");
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(npcName + " has died.");
        Destroy(gameObject);
    }
}


