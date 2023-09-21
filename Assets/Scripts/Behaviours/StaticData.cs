using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpPathFinding.cs;

public class StaticData : MonoBehaviour
{
    public UnitData[] EnemiesData,
        TowersData;

    public int MaxEnemies;
    public BaseGrid Field;
    public JumpPointParam PathSearchingParams;
    public Vector2Int FieldSize;
    public int TowerAttackRange;
    public float TowerRechargeTime;
    public float TowerDamage;
    public Transform towers;

    public enum UnitType
    {
        Enemy,
        Tower
    }

    public enum EnemyType
    {
        Goblin
    }

    public enum TowerType
    {
        BaseTower,
        DefenceTower,
        AttackTower
    }
}
