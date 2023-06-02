using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour     // 보관소
{
    [SerializeField] Poolable poolablePrefab;

    [SerializeField] int poolSize;      // 미리 만들어놓을 사이즈
    [SerializeField] int maxSize;

    private Stack<Poolable> objectPool;     // 스택을 이용하여 
    private void Awake()
    {
        objectPool = new Stack<Poolable>();
        CreatePool();
    }

    private void CreatePool()       // 만들어서 쓸 때
    {
        for (int i = 0; i< poolSize; i++)
        {
            Poolable poolable = Instantiate(poolablePrefab);
            poolable.gameObject.SetActive(false);               // 만들자마자 비활성화 시켜두고
            poolable.transform.SetParent(transform);            // 부모를 스크립트를 넣은 오브젝트의 위치로
            poolable.Pool = this;
            objectPool.Push(poolable);
        }
    }

    public Poolable Get()           // 이거 가져다 써의 의미로 Get
    {
        if (objectPool.Count > 0) 
        {
            Poolable poolable = objectPool.Pop();           // 있으면 꺼내다가 쓰고
            poolable.gameObject.SetActive(true);
            poolable.transform.parent = null;
            return poolable;
        }
        else
        {
            Poolable poolable = Instantiate(poolablePrefab);            // 없으면 만들어서 사용
            poolable.Pool = this;
            return poolable;
        }
    }

    public void Release(Poolable poolable)      // 반납할게
    {
        if(objectPool.Count < maxSize)          // maxSize만큼만 반납하고 나머지는 삭제해달라는 컨셉의 반납조건
        {
            poolable.gameObject.SetActive(false);
            poolable.transform.SetParent(transform);
            objectPool.Push(poolable);
        }
        else
        {
            Destroy(poolable.gameObject);
        }
        
    }
}
