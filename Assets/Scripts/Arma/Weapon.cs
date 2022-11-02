using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Transform _startShootTransform;

    [SerializeField] float _reloadingCooldown;
    [SerializeField] int _totalBullets;
    [SerializeField] int _bulletsPerMagazine;
    [SerializeField] int _currentMagazine;
    [SerializeField] protected float _damage;

    public float _shootCooldown;

    bool _isReloading;
    bool _canShoot;

    protected Ray _ray;
    protected RaycastHit _hitInfo;
    protected LayerMask _hitlayerMasks;

    public void SetInitialParameters(Transform startShootTransform, LayerMask hitLayerMasks)
    {
        _startShootTransform = startShootTransform;
        _hitlayerMasks = hitLayerMasks;

        _canShoot = true;

        _isReloading = false;
    }

    abstract protected void ShootBehaviour();

    virtual public void Shoot()
    {
        if (!_canShoot || _isReloading) return;

        if (_currentMagazine > 0)
        {
            _currentMagazine--;

            Debug.Log($"Shoot, remaining bullets {_currentMagazine}");

            ShootBehaviour();

            StartCoroutine(ShootCooldown());
        }
        else
        {
            Reload();
        }
    }

    virtual public void Reload()
    {
        if (_isReloading) return;

        if (_totalBullets > 0 && _currentMagazine < _bulletsPerMagazine)
        {
            int bulletsNeeded = _bulletsPerMagazine - _currentMagazine;

            if (bulletsNeeded <= _totalBullets)
            {
                _totalBullets -= bulletsNeeded;
                _currentMagazine = _bulletsPerMagazine;
            }
            else
            {
                _currentMagazine += _totalBullets;
                _totalBullets = 0;
            }

            StartCoroutine(ReloadCooldown());

        }
    }

    IEnumerator ShootCooldown()
    {
        Debug.Log($"Shoot cooldown started");

        _canShoot = false;

        yield return new WaitForSeconds(_shootCooldown);

        Debug.Log($"Shoot cooldown end");

        _canShoot = true;

    }

    IEnumerator ReloadCooldown()
    {
        Debug.Log($"Reload cooldown started");

        _isReloading = true;
        _canShoot = false;

        yield return new WaitForSeconds(_reloadingCooldown);

        Debug.Log($"Reload cooldown end");

        _isReloading = false;
        _canShoot = true;
    }
}
