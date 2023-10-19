using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolAmount;

    private List<GameObject> pooledGameObjectList = new();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject poolObject = Instantiate(bulletPrefab);
            poolObject.SetActive(false);
            poolObject.transform.parent = transform;
            pooledGameObjectList.Add(poolObject);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledGameObjectList.Count; i++)
        {
            if (!pooledGameObjectList[i].activeInHierarchy)
            {
                return pooledGameObjectList[i];
            }
        }

        return null;
    }
}
