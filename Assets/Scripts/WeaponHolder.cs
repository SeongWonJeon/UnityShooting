using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] Gun gun;

    /*private List<Gun> gunList;
    public void GetWeapon(Gun gun)
    {
        gunList.Add(gun);   
    }*/
    public void Fire()
    {
        gun.Fire();
    }

    
}
