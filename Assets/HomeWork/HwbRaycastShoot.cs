using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class HwbRaycastShoot : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] TrailRenderer bulletTrail;
    [SerializeField] float maxTrail;        // 이만큼의 거리만큼
    [SerializeField] float bulletSpeed;
    [SerializeField] int damage;
    public void Fire()
    {
        muzzleEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxTrail))
        {
            IHitTarget hitTarget = hit.transform.GetComponent<IHitTarget>();
            ParticleSystem Effect = Instantiate(hitEffect, hit.point, Quaternion.identity);
            Effect.transform.parent = hit.transform;
            Destroy(Effect, 3f);

            StartCoroutine(TrailRange(muzzleEffect.transform.position, hit.point));

            hitTarget?.HitTarget(hit, damage);
        }
        else
        {
            StartCoroutine(TrailRange(muzzleEffect.transform.position, Camera.main.transform.forward * maxTrail));
        }

    }

    IEnumerator TrailRange(Vector3 startPoint, Vector3 endPoint)
    {
        TrailRenderer Trail = Instantiate(bulletTrail, muzzleEffect.transform.position, Quaternion.identity);
        float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

        float rate = 0;
        while (rate < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
            rate += Time.deltaTime / totalTime;

            yield return null;
        }
        Destroy(Trail);
    }
}
