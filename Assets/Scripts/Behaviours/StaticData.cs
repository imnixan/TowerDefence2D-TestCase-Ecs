using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpPathFinding.cs;

public class StaticData : MonoBehaviour
{
    [SerializeField]
    private float GoblinRechargeTime,
        GoblinMovespeed,
        GoblinAttackRange,
        GoblinDamage,
        GoblinHp;
    public int MaxEnemies;
    public BaseGrid Field;
    public JumpPointParam PathSearchingParams;
    public Vector2Int FieldSize;
    public int TowerAttackRange;
    public float TowerRechargeTime;
    public float TowerDamage;
    public Transform towers;

    public Dictionary<int, float> EnemiesRechargeTime;
    public Dictionary<int, float> EnemiesSpeed;
    public Dictionary<int, float> EnemiesAttackRange;
    public Dictionary<int, float> EnemiesAttackDamage;
    public Dictionary<int, float> EnemiesHp;

    public enum EnemyType
    {
        Goblin,
        Wolf
    }

    public void InitData()
    {
        EnemiesRechargeTime = new Dictionary<int, float> { { 0, GoblinMovespeed } };
        EnemiesAttackRange = new Dictionary<int, float> { { 0, GoblinAttackRange } };
        EnemiesSpeed = new Dictionary<int, float> { { 0, GoblinMovespeed } };
        EnemiesAttackDamage = new Dictionary<int, float> { { 0, GoblinDamage } };
        EnemiesHp = new Dictionary<int, float> { { 0, GoblinHp } };
    }
}
