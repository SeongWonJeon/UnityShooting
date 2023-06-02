using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IHittable
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Hit(RaycastHit hit, int damage)
    {
        if (rb != null)
        {
            // AddForceAtPosition부딪힌 포지션의 반대방향으로 힘을 받도록 한다
            rb.AddForceAtPosition(-10 * hit.normal, hit.point, ForceMode.Impulse);

        }
    }
}
