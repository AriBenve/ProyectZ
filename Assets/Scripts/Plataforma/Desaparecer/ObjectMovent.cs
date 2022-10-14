using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovent : MonoBehaviour
{
    public bool willDestroy;
    public float timeTodestroy;
    public float destroyCd;
    public bool startCd;



    private void Start()
    {
        destroyCd = timeTodestroy;   
    }
    private void Update()
    {
        if (startCd)
        {
            destroyCd -= Time.deltaTime;
            if (destroyCd <= 0)
            {
                StartCoroutine(ReactivatePlatform());
                destroyCd = timeTodestroy;
                startCd = false;
            }

        }
    }
    IEnumerator ReactivatePlatform()
    {
       
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        BoxCollider[] myColliders = gameObject.GetComponents<BoxCollider>();
        foreach(var colliders in myColliders)
        {
            colliders.enabled = false;
        }

        yield return new WaitForSeconds(2f);

        foreach (var colliders in myColliders)
        {
            colliders.enabled = true;
        }
        gameObject.GetComponent<MeshRenderer>().enabled = true;

       
    }
}
