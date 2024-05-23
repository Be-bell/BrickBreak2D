using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [System.Serializable] 
    public class Pool
    {
        public string objTag;
        public GameObject prefab;
        [Range(0, 100)] public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        MakePool();
    }

    private void MakePool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.parent = this.transform;
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.objTag, objPool);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject obj = poolDictionary[tag].Dequeue();
        poolDictionary[tag].Enqueue(obj);
        return obj;
    }
}
