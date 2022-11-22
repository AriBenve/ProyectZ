using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunChanger : MonoBehaviour
{
    public GameObject[] guns = new GameObject[4];
    GameObject _actualGun;

    public Image imageShotgunMode;
    public TextMeshProUGUI tmProShotgunMode;

    private void Start()
    {
        _actualGun = guns[0];
        imageShotgunMode.enabled = false;
        tmProShotgunMode.enabled = false;
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
            imageShotgunMode.enabled = false;
            tmProShotgunMode.enabled = false;
        }
        //Escopeta
        else if(Input.GetKeyDown(KeyCode.Alpha2) && guns[1] != _actualGun)
        {
            _actualGun.SetActive(false);
            guns[1].SetActive(true);
            _actualGun = guns[1];
            imageShotgunMode.enabled = true;
            tmProShotgunMode.enabled = true;
        }
        //Arco
        else if (Input.GetKeyDown(KeyCode.Alpha3) && guns[2] != _actualGun)
        {
            _actualGun.SetActive(false);
            guns[2].SetActive(true);
            _actualGun = guns[2];
            imageShotgunMode.enabled = false;
            tmProShotgunMode.enabled = false;
        }
    }
}
