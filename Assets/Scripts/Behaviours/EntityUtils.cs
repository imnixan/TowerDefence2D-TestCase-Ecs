using Leopotam.Ecs;
using UnityEngine;

public static class EntityUtils
{
    public static void AddMovable(this EcsEntity entity, StaticData staticData)
    {
        ref Movable entityMovable = ref entity.Get<Movable>();
        ref ObjectComponent objComp = ref entity.Get<ObjectComponent>();
        entityMovable.ObjectTransform = objComp.ObGo.transform;

        UnitData unitData = staticData.EnemiesData[GetEntityTypeNum(entity)];
        entityMovable.Speed = unitData.Speed;
        entityMovable.Rb = objComp.ObGo.GetComponent<Rigidbody2D>();
    }

    public static void AddAttacker(this EcsEntity entity, StaticData staticData)
    {
        ref Attacker attacker = ref entity.Get<Attacker>();
        UnitData unit = staticData.EnemiesData[GetEntityTypeNum(entity)];
        attacker.AttackRange = unit.AttackRange;
        attacker.Damage = unit.Damage;
        attacker.RechargeTime = unit.RechargeTime;
    }

    public static void AddObjectComp(
        this EcsEntity entity,
        StaticData staticData,
        PoolSystem pool,
        StaticData.UnitType unitType
    )
    {
        ref ObjectComponent objComp = ref entity.Get<ObjectComponent>();
        objComp.ObGo = pool.GetEnemyObject();
        objComp.ObSr = objComp.ObGo.GetComponent<SpriteRenderer>();
        objComp.ObTransform = objComp.ObGo.transform;
        objComp.UnitType = unitType;
    }

    private static int GetEntityTypeNum(EcsEntity ecsEntity)
    {
        ref ObjectComponent objComp = ref ecsEntity.Get<ObjectComponent>();
        switch (objComp.UnitType)
        {
            case StaticData.UnitType.Enemy:
                ref Enemy enemy = ref ecsEntity.Get<Enemy>();
                return (int)enemy.EnemyType;
            case StaticData.UnitType.Tower:
                ref Tower tower = ref ecsEntity.Get<Tower>();
                return (int)tower.TowerType;
        }
        return 0;
    }
}
