using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpPathFinding.cs;

public class StaticData : MonoBehaviour
{
    public Transform healthBarCanvas;
    public GameObject healthBarPrefab;

    public UnitData[] EnemiesData,
        TowersData;

    public int MaxEnemies;
    public BaseGrid Field;
    public JumpPointParam PathSearchingParams;
    public Vector2Int FieldSize;

    public Transform towers;

    public enum UnitType
    {
        Enemy,
        Tower,
        Projectile
    }

    public enum EnemyType
    {
        Goblin,
        Archer
    }

    public enum TowerType
    {
        BaseTower,
        BuildPlace,
        DefenceTower,
        AttackTower
    }
}
