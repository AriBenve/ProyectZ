using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWalls : MonoBehaviour
{
    public float dmg;
    public float timer;
    public float camShakeMagnitude, camShakeDuration;

    [Header("References")]
    [SerializeField] CameraShake camShake;
    [SerializeField] AudioSource AS;

    private void OnTriggerStay(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            if(!AS.isPlaying)
            {
                AS.Play();
            }
            StartCoroutine(player.GraduallyReduceHP(dmg, timer));
            StartCoroutine(camShake.Shake(camShakeMagnitude, camShakeDuration));
        }
    }
}
