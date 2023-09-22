using UnityEngine;
using UnityEngine.UI;
using Leopotam.Ecs;
using System.Collections.Generic;

struct ObjectComponent
{
    public GameObject ObGo;
    public SpriteRenderer ObSr;
    public Transform ObTransform;
    public StaticData.UnitType UnitType;
}

struct Attacker
{
    public float AttackRange;
    public float Damage;
    public float RechargeTime;
}

struct HasTarget
{
    public EcsEntity target;
}

struct RangeAttackUnit
{
    public Sprite projectileSprite;
    public float ProjecttileSpeed;
}

struct CreateProjectileMarker { }

struct Projectile
{
    public EcsEntity target;
    public float Damage;
    public float Speed;
}

struct Health
{
    public float MaxHp;
    public float HP;
}

struct DamageRecieveMarker
{
    public float Damage;
}

struct HealthBar
{
    public Image healthBarFill;
    public Transform hpBarTransform;
}

struct DeadMarker { }
