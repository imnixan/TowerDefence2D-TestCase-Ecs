using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private List<GameObject> enemiesPool = new List<GameObject>();

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

    public void ReturnObjectInPool(GameObject objToPool)
    {
        objToPool.SetActive(false);
        enemiesPool.Add(objToPool);
    }
}
