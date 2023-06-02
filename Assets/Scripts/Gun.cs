using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour            // 잔탄수, 최대 탄창수 등을 넣는게 좋을 거 같다
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] TrailRenderer bulletTrail;
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxDistance;         
    [SerializeField] int damage;
    public virtual void Fire()
    {
        // 총이 나가기 전에 재생을 해야하니 위에 재생
        muzzleEffect.Play();

        RaycastHit hit;
        // 맞았으면 hit하도록 구현
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            IHittable hittable = hit.transform.GetComponent<IHittable>();   // 인터페이스도 GetComponent할 수 있다
            ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform;        // 맞은 오브젝트의 하위자식으로 위치를 넣어주면 같이 움직일 것
            Destroy(effect, 3f);

            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, hit.point));

            hittable?.Hit(hit, damage);     // ?. 을통해서 널이면 하고 널이 아니면 안한다.

        }
        else
        {
            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, Camera.main.transform.forward * maxDistance));
        }
    }
    IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
    {
        TrailRenderer trail = Instantiate(bulletTrail, muzzleEffect.transform.position, Quaternion.identity); //Quaternion.identity회전은 필요없을 거 같으니
        float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed; // 거 / 시 * 속 을 이용하여

        // 반복문을 통해서 
        float rate = 0;
        while ( rate < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
            rate += Time.deltaTime / totalTime;

            yield return null;
        }
        Destroy(trail);
    }
}
