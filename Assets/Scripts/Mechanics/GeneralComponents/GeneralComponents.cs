﻿using UnityEngine;
using UnityEngine.UI;
using Leopotam.Ecs;
using System.Collections.Generic;
using TMPro;

struct GuiComp
{
    public float EnemiesKilled;
    public TextMeshProUGUI KillCounter;
}

struct ObjectComponent
{
    public GameObject ObGo;
    public SpriteRenderer ObSr;
    public Transform ObTransform;
    public UnitSprites unitSprites;
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
    public EcsEntity Target;
}

struct RangeAttackUnit
{
    public Sprite ProjectileSprite;
    public float ProjecttileSpeed;
}

struct CreateProjectileMarker { }

struct Projectile
{
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
