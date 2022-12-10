using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int weaponMaxAmmo;
    [SerializeField] private int weaponCurrentAmmo;

    [SerializeField] private float weaponFireRate;

    [SerializeField] private WeaponProjectile weaponProjectile;
    [SerializeField] private WeaponCooldown weaponCooldown;
    private bool isOnCooldown;

    private void Start()
    {
        weaponCooldown.cooldownComplete = HandleCooldownComplete;
        isOnCooldown = false;
        weaponCurrentAmmo = weaponMaxAmmo;
    }

    public void Fire()
    {
        if (!isOnCooldown && weaponCurrentAmmo > 0)
        {
            weaponProjectile.ProjectileGenerator();
            weaponCurrentAmmo--;
            isOnCooldown = true;
            weaponCooldown.StartCooldown(weaponFireRate);
        }
    }

    public void Reload()
    {
        weaponCurrentAmmo = weaponMaxAmmo;
    }

    private void HandleCooldownComplete()
    {
        isOnCooldown = false;
    }
}
