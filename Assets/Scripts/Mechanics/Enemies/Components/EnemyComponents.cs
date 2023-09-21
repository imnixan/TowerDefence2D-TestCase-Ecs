using UnityEngine;
using EpPathFinding.cs;
using System.Collections.Generic;
using Leopotam.Ecs;

struct Enemy
{
    public StaticData.EnemyType EnemyType;
}

struct Movable
{
    public Transform ObjectTransform;
    public Rigidbody2D Rb;
    public float Speed;
}

struct Attacker
{
    public float AttackRange;
    public float Damage;
    public float RechargeTime;
}

struct RangeAttackUnit
{
    public Sprite projectile;
}

struct MeleeAttackUnit { }

struct HasTarget : IEcsAutoReset<HasTarget>
{
    public List<EcsEntity> KillList;

    public void AutoReset(ref HasTarget c)
    {
        c.KillList = new List<EcsEntity>();
    }
}

struct InBattleMarker
{
    public float LastAttack;
}
