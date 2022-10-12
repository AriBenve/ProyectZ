using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    [Header("Gun Stats")]
    public int dmg;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int _bulletsLeft, _bulletsShot;

    [Header("Bools")]
    bool _shooting, _readyToShoot, _reloading;

    [Header("References")]
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    [Header("Graphics")]
    public GameObject muzzleFlash, bulletHoleGraphic;
    public CameraShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    private void Awake()
    {
        _bulletsLeft = magazineSize;
        _readyToShoot = true;
    }

    private void Update()
    {
        _MyInput();

        //text.SetText(_bulletsLeft + " / " + magazineSize);
    }

    private void _MyInput()
    {
        if(allowButtonHold) _shooting = Input.GetKey(KeyCode.Mouse0);
        else _shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && _bulletsLeft < magazineSize && !_reloading) Reload();

        if (_readyToShoot && _shooting && !_reloading && _bulletsLeft > 0)
        {
            _bulletsShot = bulletsPerTap;
            Shoot();
        }
            
    }

    private void Shoot()
    {
        _readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x,y,0);

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            if(rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<Enemy>().GetHit(dmg);
        }

        camShake.Shake(camShakeDuration, camShakeMagnitude);

        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        _bulletsLeft--;
        _bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if (_bulletsShot > 0 && _bulletsShot > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        _readyToShoot = true;
    }

    private void Reload()
    {
        _reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        _bulletsLeft = magazineSize;
        _reloading = false;
    }
}
