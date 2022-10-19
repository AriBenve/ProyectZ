using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public GameObject Ballesta;
    public Transform aimPosition;
    GameObject CurrentTarget;
    public float Distance;
    public float CD;
    private float Timer;

    bool isAiming;

    private void Start()
    {
        Timer = CD;
        Ballesta.transform.transform.position = aimPosition.position;
    }

    private void Update()
    {
        CheckTarget();
        
        if (isAiming)
            AutoAiminig();
    
    }

    public void CheckTarget()
    {
        RaycastHit Hit;

        if(Physics.Raycast(transform.position,transform.forward,out Hit, Distance))
        {
            if(Hit.transform.gameObject.tag == "Enemy")
            {
                if (!isAiming)
                    print("Target found");

                CurrentTarget = Hit.transform.gameObject;
                isAiming = true;

            }
            else
            {
                CurrentTarget = null;
                isAiming = false;
            }
        }

    }

    private void AutoAiminig()
    {

        Ballesta.transform.LookAt(CurrentTarget.transform);

    }
}
