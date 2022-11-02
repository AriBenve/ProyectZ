using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunChanger : MonoBehaviour
{
    public GameObject[] guns = new GameObject[4];
    GameObject _actualGun;

    public Image image;
    public TextMeshProUGUI tmPro;

    private void Start()
    {
        _actualGun = guns[0];
        image.enabled = false;
        tmPro.enabled = false;
    }

    private void Update()
    {
        changeWeapon();   
    }

    void changeWeapon()
    {
        //Ballesta
        if(Input.GetKeyDown(KeyCode.Alpha1) && guns[0] != _actualGun)
        {
            _actualGun.SetActive(false);
            guns[0].SetActive(true);
            _actualGun = guns[0];
            image.enabled = false;
            tmPro.enabled = false;
        }
        //Escopeta
        else if(Input.GetKeyDown(KeyCode.Alpha2) && guns[1] != _actualGun)
        {
            _actualGun.SetActive(false);
            guns[1].SetActive(true);
            _actualGun = guns[1];
            image.enabled = true;
            tmPro.enabled = true;
        }
        /*else if (Input.GetKeyDown(KeyCode.Alpha3) && guns[2] != _actualGun)
        {
            _actualGun.SetActive(false);
            guns[2].SetActive(true);
            _actualGun = guns[2];
            image.enabled = false;
            tmPro.enabled = false;
        }*/
    }
}
