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
    public float Speed;
}

struct MeleeAttackUnit { }

struct InBattleMarker
{
    public float LastAttack;
}
