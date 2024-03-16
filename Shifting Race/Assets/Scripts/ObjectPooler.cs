using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : Singleton<ObjectPooler>
{
    public List<Pool> pools;

    private Dictionary<string, Queue<GameObject>> poolDictonary;
    private GameObject poolItem;

    protected override void Awake()
    {
        SpawnPool();
    }


    private void SpawnPool()
    {
        poolDictonary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                poolItem = Instantiate(pool.prefab);
                poolItem.transform.SetParent(this.transform);
                poolItem.gameObject.SetActive(false);
                objectPool.Enqueue(poolItem);
            }

            poolDictonary.Add(pool.tag, objectPool);
        }
    }

    
    public GameObject SpwanObject(string tag, Vector3 pos)
    {
        poolItem = poolDictonary[tag].Dequeue();
        poolItem.transform.localPosition = pos;
        poolItem.transform.rotation = Quaternion.identity;
        poolItem.gameObject.SetActive(true);

        poolDictonary[tag].Enqueue(poolItem);

        return poolItem;
    }
}


[System.Serializable]
public class Pool
{
    public string tag;
    public int size;
    public GameObject prefab;
}