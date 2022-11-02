using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    public float throwForce;


    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    protected override void ShootBehaviour()
    {
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        // implement throwCooldown
        Invoke(nameof(Reload), _shootCooldown);
    }
}
