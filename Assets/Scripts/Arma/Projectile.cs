using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Stats")]
    public float damage;
    public bool destroyOnHit;

    [Header("Camera Shake")]
    public float camShakeMagnitude, camShakeDuration;
    public CameraShake camShake;

    [Header("Effects")]
    public GameObject muzzleEffect;
    public GameObject hitEffect;

    [Header("Explosive Projectile")]
    public bool isExplosive;
    public float explosionRadius;
    public float explosionForce;
    public int explosionDamage;
    public GameObject explosionEffect;

    [Header("Audio")]
    public List<AudioClip> audioClipList = new List<AudioClip>();
    private AudioSource audioSource;

    private Rigidbody rb;

    private bool hitTarget;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // get rigidbody component
        rb = GetComponent<Rigidbody>();

        camShake = FindObjectOfType<CameraShake>();

        // spawn muzzleEffect (if assigned)
        if(muzzleEffect != null)
            Instantiate(muzzleEffect, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitTarget)
            return;
        else
            hitTarget = true;

        // enemy hit
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // deal damage to the enemy
            enemy.GetHit(damage);

            // spawn hit effect (if assigned)
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            
            // destroy projectile
            if (!isExplosive && destroyOnHit)
                Invoke(nameof(DestroyProjectile), 0.1f);
        }

        // explode projectile if it's explosive
        if (isExplosive)
        {
            Explode();
            return;
        }

        // make sure projectile sticks to surface
        rb.isKinematic = true;

        // make sure projectile moves with target
        transform.SetParent(collision.transform);
    }

    private void Explode()
    {
        int z = Random.Range(0, audioClipList.Count);
        audioSource.PlayOneShot(audioClipList[z], 0.7f);

        StartCoroutine(camShake.Shake(camShakeMagnitude, camShakeDuration));

        // spawn explosion effect (if assigned)
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // find all the objects that are inside the explosion range
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRadius);

        // loop through all of the found objects and apply damage and explosion force
        for (int i = 0; i < objectsInRange.Length; i++)
        {
            if (objectsInRange[i].gameObject == gameObject)
            {
                // don't break or return please, thanks
            }
            else
            {
                // check if object is enemy, if so deal explosionDamage
                if (objectsInRange[i].GetComponent<Enemy>() != null)
                    objectsInRange[i].GetComponent<Enemy>().GetHit(explosionDamage);

                // check if object has a rigidbody
                if (objectsInRange[i].GetComponent<Rigidbody>() != null)
                {
                    // custom explosionForce
                    Vector3 objectPos = objectsInRange[i].transform.position;

                    // calculate force direction
                    Vector3 forceDirection = (objectPos - transform.position).normalized;

                    // apply force to object in range
                    objectsInRange[i].GetComponent<Rigidbody>().AddForceAtPosition(forceDirection * explosionForce + Vector3.up * explosionForce, transform.position + new Vector3(0,-0.5f,0), ForceMode.Impulse);

                    Debug.Log("Kabooom " + objectsInRange[i].name);
                }
            }
        }

        // destroy projectile with 0.1 seconds delay
        Invoke(nameof(DestroyProjectile), 5f);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    // just graphics stuff
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}