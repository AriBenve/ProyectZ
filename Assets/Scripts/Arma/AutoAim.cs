using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public GameObject Ballesta;
    public Transform aimPosition;
    private Transform _normalPos;
    GameObject CurrentTarget;
    public float Distance;
    public float CD;
    private float Timer;

    bool isAiming;
    bool activateAim;

    private void Start()
    {
        Ballesta.transform.transform.position = aimPosition.position;
        _normalPos = Ballesta.transform;
    }

    private void Update()
    {
        CheckTarget();
        
        if(Input.GetKeyDown(KeyCode.Q)) 
            activateAim = true;

        if (isAiming && Timer <= CD && activateAim && Ballesta.activeSelf)
        {
            Timer += Time.deltaTime;
            print(Timer);
            AutoAiminig();
        }
        else if(Timer > CD)
            ResetInputs();
    
    }

    private void ResetInputs()
    {
        print("Entre al ResetInputs()");
        Timer = 0;
        isAiming = false;
        activateAim = false;
        transform.position = _normalPos.position;
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
                transform.position = _normalPos.position;
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
