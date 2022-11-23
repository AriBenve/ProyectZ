using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] float dmg;

    public KeyCode attackKey;

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if(Input.GetKeyDown(attackKey))
        {
            //Cosas de animación
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Enemy>().GetHit(dmg);

        if (other.GetComponent<Enemy>()._life <= 0)
            FindObjectOfType<Player>().Heal(Random.Range(50, other.GetComponent<Enemy>().maxLife));
    }
}
