using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWbObjectPool : MonoBehaviour
{
    [SerializeField] private HWbPoolable hwPoolablePrefab;
    [SerializeField] int poolSize;      // �̸� �������� ������
    [SerializeField] int maxSize;

    private Stack<HWbPoolable> objectPool;     // ������ �̿��Ͽ� 
    private void Awake()
    {
        objectPool = new Stack<HWbPoolable>();
        CreatePool();
    }

    private void CreatePool()       // ���� �� ��
    {
        for (int i = 0; i < poolSize; i++)
        {
            HWbPoolable poolable = Instantiate(hwPoolablePrefab);
            poolable.gameObject.SetActive(false);               // �����ڸ��� ��Ȱ��ȭ ���ѵΰ�
            poolable.transform.SetParent(transform);            // �θ� ��ũ��Ʈ�� ���� ������Ʈ�� ��ġ��
            poolable.Pool = this;
            objectPool.Push(poolable);
        }
    }

    public HWbPoolable Get()           // �̰� ������ ���� �ǹ̷� Get
    {
        if (objectPool.Count > 0)
        {
            HWbPoolable poolable = objectPool.Pop();           // ������ �����ٰ� ����
            poolable.gameObject.SetActive(true);
            poolable.transform.parent = null;
            return poolable;
        }
        else
        {
            HWbPoolable poolable = Instantiate(hwPoolablePrefab);            // ������ ���� ���
            poolable.Pool = this;
            return poolable;
        }
    }

    public void Release(HWbPoolable poolable)      // �ݳ��Ұ�
    {
        if (objectPool.Count < maxSize)          // maxSize��ŭ�� �ݳ��ϰ� �������� �����ش޶�� ������ �ݳ�����
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
