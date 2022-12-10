using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCooldown : MonoBehaviour
{
    public Action cooldownComplete;

    public void StartCooldown(float cd)
    {
        StartCoroutine(Cooldown(cd));
    }

    IEnumerator Cooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        cooldownComplete?.Invoke();
    }
}
