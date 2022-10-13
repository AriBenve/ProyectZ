using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaPlayer : MonoBehaviour
{
    CharacterController characterController;

    Vector3 grounPos; //posicion actual
    Vector3 lastGrounPos; //ultima posicion
    Vector3 currentPos; //posicion recalculada

    string grounName;     //variables para detectar si la plataforma
    string lastGrounName; //sobre la que estamos se movio de su lugar base

    bool isJump; //si esta variable es verdadera, el personaje dejara de moverse con la plataforma

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnTriggerStay(Collider other)  //esta funcion se cumpliria siempre y cuando el personaje se encuentre dentro
    {                                           //de nuestro collider marcado como isTrigger
        if (other.tag == "platform")
        {
            if (!isJump)
            {
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, characterController.radius, -transform.up, out hit))
                {
                    GameObject inGround = hit.collider.gameObject;
                    grounName = inGround.name;
                    grounPos = inGround.transform.position;

                    if (grounPos != lastGrounPos && grounName == lastGrounName)
                    {
                        currentPos = Vector3.zero;
                        currentPos += grounPos - lastGrounPos;
                        characterController.Move(currentPos);
                    }

                    lastGrounName = grounName;
                    lastGrounPos = grounPos;
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (!characterController.isGrounded)
                {
                    currentPos = Vector3.zero;
                    lastGrounPos = Vector3.zero;
                    lastGrounName = null;
                    isJump = true;
                }
            }

            if (characterController.isGrounded)
            {
                isJump = false;
            }
        }
    }
}
