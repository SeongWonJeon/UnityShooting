using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTaster : MonoBehaviour
{
    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            Poolable poolable = objectPool.Get();
            // Vector3의 사각형태의 부분에 랜덤의 위치에 나오도록
            poolable.transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        }
    }
}
