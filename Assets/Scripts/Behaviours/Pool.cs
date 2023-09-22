using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private List<GameObject> enemiesPool = new List<GameObject>();
    private List<GameObject> prjectilesPool = new List<GameObject>();

    private GameObject go;

    public GameObject GetEnemyObject()
    {
        if (enemiesPool.Count > 0)
        {
            go = enemiesPool[0];
            go.SetActive(true);
            enemiesPool.RemoveAt(0);
        }
        else
        {
            go = Instantiate(enemyPrefab);
        }

        return go;
    }

    public void ReturnEnemyObjectInPool(GameObject objToPool)
    {
        objToPool.SetActive(false);
        enemiesPool.Add(objToPool);
    }

    public GameObject GetProjObj()
    {
        if (prjectilesPool.Count > 0)
        {
            go = prjectilesPool[0];
            go.SetActive(true);
            prjectilesPool.RemoveAt(0);
        }
        else
        {
            go = Instantiate(enemyPrefab);
        }

        return go;
    }

    public void ReturnProjObjectInPool(GameObject objToPool)
    {
        objToPool.SetActive(false);
        prjectilesPool.Add(objToPool);
    }
}
