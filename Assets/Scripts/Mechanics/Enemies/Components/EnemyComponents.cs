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
}

struct HasTargets : IEcsAutoReset<HasTargets>
{
    public List<EcsEntity> KillList;

    public void AutoReset(ref HasTargets c)
    {
        c.KillList = new List<EcsEntity>();
    }
}

struct Attacking
{
    public float AttackRecharge;
    public float LastAttack;
}
