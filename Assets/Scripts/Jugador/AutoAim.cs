using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public GameObject Ballesta;
    public Transform aimPosition;
    GameObject cuerrentTarget;
    public float distance = 10f;

    bool isAiming;
    public float Timer;
    public float CD;
    bool isactive; 

    private void Start()
    {
        cuerrentTarget.transform.position = aimPosition.position;
        Timer = CD;

    }

    void Update()
    {
        checkTarget();

        isactive = Input.GetKeyDown(KeyCode.Q);


        if (isAiming && isactive && Timer >= 0)
        {
            while (Timer >=0)
            {
                AutoAiming();
                Timer -= Time.deltaTime;
            }

            Timer = CD;
        }
    }

    private void checkTarget()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position,transform.forward,out hit, distance))
        {
            if(hit.transform.gameObject.tag == "Enemy")
            {
                if (!isAiming) 
                    print("target found");

                cuerrentTarget = hit.transform.gameObject;
                isAiming = true; 


            }
            else
            {

                cuerrentTarget = null;
                isAiming = false; 

            }


        }

    }

    private void AutoAiming()
    {



        Ballesta.transform.LookAt(cuerrentTarget.transform);


    }




}
