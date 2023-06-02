using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Rig reloading;
    [SerializeField] private float reloadTime;
    [SerializeField] WeaponHolder weaponHolder;
 
    private Animator anim;
    private bool isReloading;
    private Coroutine able;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        // weaponHolder = GetComponentInChildren<WeaponHolder>();
    }
    private void OnShot(InputValue value)
    {
        if (isReloading)
            return;
        Fire();
    }
    private void OnReload(InputValue value)
    {
        if (isReloading)
            return;

        able = StartCoroutine(ReloadRoutine());
    }
    IEnumerator ReloadRoutine()
    {
        anim.SetTrigger("Reloading");
        isReloading = true;
        reloading.weight = 0f;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        reloading.weight = 1f;
    }

    public void Fire()
    {
        weaponHolder.Fire();
        anim.SetTrigger("Fire");
    }
}
