using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoSingleton<PoolManager>
{
    [System.Serializable]
    private class ObjectInfo
    {
        public string objName;
        public GameObject prefab;
        public int count;   
    }

    public bool IsReady { get; private set; }
    [SerializeField] private ObjectInfo[] objInfos;

    private string objName;

    private Dictionary<string, IObjectPool<GameObject>> objPoolDic = new Dictionary<string, IObjectPool<GameObject>>();
    private Dictionary<string, GameObject> gameobjDic = new Dictionary<string, GameObject>();

    private void Init()
    {
        IsReady = false;

        for (int i = 0; i < objInfos.Length; i++)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>
                (CreateNewObject,
                OnGetPoolObject,
                OnReleasePoolObject,
                OnDestroyPoolObject
                , true, objInfos[i].count, objInfos[i].count);
            if (gameobjDic.ContainsKey(objInfos[i].objName))
            {
                Debug.LogFormat("{0} : Already Assgined.", objInfos[i].objName);
            }

            gameobjDic.Add(objInfos[i].objName, objInfos[i].prefab);
            objPoolDic.Add(objInfos[i].objName, pool);

            for (int j = 0; j < objInfos[i].count; j++)
            {
                objName = objInfos[i].objName;
                Poolable poolable = CreateNewObject().GetComponent<Poolable>();
                poolable.pool.Release(poolable.gameObject);
            }

            Debug.Log("[PoolManager] Ready to pool");
            IsReady = true;
        }
    }

    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    private void OnReleasePoolObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnGetPoolObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    private GameObject CreateNewObject()
    {
        GameObject newobj = Instantiate(gameobjDic[objName]);
        newobj.transform.parent = gameObject.transform;
        newobj.GetComponent<Poolable>().pool = objPoolDic[objName];
        return newobj;
    }

    public GameObject Spawn(string name)
    {
        objName = name;
        if(!gameobjDic.ContainsKey(name))
        {
            Debug.LogFormat("{0} : Key is not found in pool", name);
            return null;
        }

        return objPoolDic[name].Get();
    }

    private void Start()
    {
        Init();
    }
}
