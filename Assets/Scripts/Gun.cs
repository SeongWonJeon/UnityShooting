using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour            // ��ź��, �ִ� źâ�� ���� �ִ°� ���� �� ����
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] TrailRenderer bulletTrail;
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxDistance;         
    [SerializeField] int damage;
    public virtual void Fire()
    {
        // ���� ������ ���� ����� �ؾ��ϴ� ���� ���
        muzzleEffect.Play();

        RaycastHit hit;
        // �¾����� hit�ϵ��� ����
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            IHittable hittable = hit.transform.GetComponent<IHittable>();   // �������̽��� GetComponent�� �� �ִ�
            ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform;        // ���� ������Ʈ�� �����ڽ����� ��ġ�� �־��ָ� ���� ������ ��
            Destroy(effect, 3f);

            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, hit.point));

            hittable?.Hit(hit, damage);     // ?. �����ؼ� ���̸� �ϰ� ���� �ƴϸ� ���Ѵ�.

        }
        else
        {
            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, Camera.main.transform.forward * maxDistance));
        }
    }
    IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
    {
        TrailRenderer trail = Instantiate(bulletTrail, muzzleEffect.transform.position, Quaternion.identity); //Quaternion.identityȸ���� �ʿ���� �� ������
        float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed; // �� / �� * �� �� �̿��Ͽ�

        // �ݺ����� ���ؼ� 
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
