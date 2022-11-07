using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDone : MonoBehaviour
{
    [Header("Settings")]
    public int damage;
    public bool destroyOnHit;

    private Rigidbody rb;

    private bool hitTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitTarget)
            return;
        else
            hitTarget = true;

        // check if you hit an enemy
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.GetHit(damage);
        }

        // make sure projectile sticks to surface
        rb.isKinematic = true;

        // make sure projectile moves with target
        transform.SetParent(collision.transform);
    }
}