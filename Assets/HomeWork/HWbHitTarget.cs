using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HWbHitTarget : MonoBehaviour, IHitTarget
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void HitTarget(RaycastHit hit, int damage)
    {
        if (rb != null)
        {
            rb.AddForceAtPosition(hit.normal * -10, hit.point, ForceMode.Impulse);
        }
    }
}
