using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeLoop : MonoBehaviour
{
    public Animation spikeAnimation;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {


        StartCoroutine(Active());



    }

   
    
        IEnumerator Active()
        {
            while (true)
            {
                GetComponent<Animation>().Play();
                yield return new WaitForSeconds(1f);
            }
        }



    

}
