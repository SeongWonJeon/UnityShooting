using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWbPoolable : MonoBehaviour
{
    [SerializeField] float releaseTime;

    private HWbObjectPool pool;        // �ݳ��� ��ġ

    public HWbObjectPool Pool { get { return pool; } set { pool = value; } }


    private void OnEnable()
    {
        StartCoroutine(ReleaseTimer());

    }
    IEnumerator ReleaseTimer()
    {
        yield return new WaitForSeconds(releaseTime);
        pool.Release(this);
    }
}
