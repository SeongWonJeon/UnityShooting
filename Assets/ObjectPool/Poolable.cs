using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour       // 실제로 사용될 오브젝트에 스크립트 추가        // 어디로 반납해야될지 꼭 정해줘야함
{
    [SerializeField] float releaseTime;

    private ObjectPool pool;        // 반납할 위치

    public ObjectPool Pool { get { return pool; }set { pool = value; } }


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
