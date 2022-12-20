using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoBoss : MonoBehaviour
{
    public Animator ani;
    public Boss boss;
    public int melee;

    void OnTriggerEnter(Collider coll)
    {
        if (CompareTag("Player"))
        {

            melee = Random.Range(0, 4);
            switch (melee)
            {
                case 0:
                    ///--Golpe 1--///
                    ani.SetFloat("Skills", 0);
                    boss.hit_Select = 0;
                    ani.SetBool("attack", false);
                    break;
                case 1:
                    ///--Golpe 2--///
                    ani.SetFloat("Skills", 0.2f);
                    boss.hit_Select = 1;
                    ani.SetBool("attack", false);
                    break;
                case 2:
                    ///--Jump--///
                    ani.SetFloat("Skills", 0.4f);
                    boss.hit_Select = 2;
                    ani.SetBool("attack", false);
                    break;
                case 3:
                    /// Fire Ball///
                    if(boss.fase == 2)
                    {
                        ani.SetFloat("Skills", 1);
                    }
                    else
                    {
                        melee = 0;
                    }
                    break;
            }
        }
        ani.SetBool("walk", false);
        ani.SetBool("run", false);
        ani.SetBool("attack", true);
        boss.atacando = true;
        GetComponent<CapsuleCollider>().enabled = false;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
