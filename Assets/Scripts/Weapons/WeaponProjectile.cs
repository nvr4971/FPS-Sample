using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [SerializeField] private Transform projectileFirePoint;
    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private float projectileLifetime;

    [SerializeField] private Transform player;

    public void ProjectileGenerator()
    {
        Projectile projectile = Instantiate(projectilePrefab, projectileFirePoint.position, projectileFirePoint.rotation);
        //projectile.transform.LookAt(player);
        //Rigidbody rb = projectile.gameObject.GetComponent<Rigidbody>();

        //rb.AddForce(projectileFirePoint.forward * projectile.projectileSpeed, ForceMode.Acceleration);

       // Destroy(projectile, projectileLifetime);
    }
}
