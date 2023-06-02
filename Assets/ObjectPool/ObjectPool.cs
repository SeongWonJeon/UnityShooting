using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour     // ������
{
    [SerializeField] Poolable poolablePrefab;

    [SerializeField] int poolSize;      // �̸� �������� ������
    [SerializeField] int maxSize;

    private Stack<Poolable> objectPool;     // ������ �̿��Ͽ� 
    private void Awake()
    {
        objectPool = new Stack<Poolable>();
        CreatePool();
    }

    private void CreatePool()       // ���� �� ��
    {
        for (int i = 0; i< poolSize; i++)
        {
            Poolable poolable = Instantiate(poolablePrefab);
            poolable.gameObject.SetActive(false);               // �����ڸ��� ��Ȱ��ȭ ���ѵΰ�
            poolable.transform.SetParent(transform);            // �θ� ��ũ��Ʈ�� ���� ������Ʈ�� ��ġ��
            poolable.Pool = this;
            objectPool.Push(poolable);
        }
    }

    public Poolable Get()           // �̰� ������ ���� �ǹ̷� Get
    {
        if (objectPool.Count > 0) 
        {
            Poolable poolable = objectPool.Pop();           // ������ �����ٰ� ����
            poolable.gameObject.SetActive(true);
            poolable.transform.parent = null;
            return poolable;
        }
        else
        {
            Poolable poolable = Instantiate(poolablePrefab);            // ������ ���� ���
            poolable.Pool = this;
            return poolable;
        }
    }

    public void Release(Poolable poolable)      // �ݳ��Ұ�
    {
        if(objectPool.Count < maxSize)          // maxSize��ŭ�� �ݳ��ϰ� �������� �����ش޶�� ������ �ݳ�����
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
