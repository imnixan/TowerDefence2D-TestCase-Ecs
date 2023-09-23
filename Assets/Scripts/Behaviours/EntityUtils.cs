using Leopotam.Ecs;
using UnityEngine;

public static class EntityUtils
{
    public static void AddMovable(this EcsEntity entity, StaticData staticData)
    {
        ref Movable entityMovable = ref entity.Get<Movable>();
        ref ObjectComponent objComp = ref entity.Get<ObjectComponent>();
        objComp.ObTransform = objComp.ObGo.transform;

        UnitData unitData = staticData.EnemiesData[GetEntityTypeNum(entity)];
        entityMovable.Speed = unitData.Speed;
    }

    public static void ChangeColor(this EcsEntity entity, Color color)
    {
        if (entity.Has<ObjectComponent>())
        {
            ref ObjectComponent objComp = ref entity.Get<ObjectComponent>();
            objComp.ObSr.color = color;
            if (color == Color.green)
            {
                Debug.Log("Green");
            }
        }
    }

    public static void ChangeColor(this EcsEntity entity, Sprite sprite)
    {
        if (entity.Has<ObjectComponent>())
        {
            ref ObjectComponent objComp = ref entity.Get<ObjectComponent>();
            objComp.ObSr.sprite = sprite;
        }
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
        GameObject go,
        StaticData.UnitType unitType,
        Vector2 pos
    )
    {
        ref ObjectComponent objComp = ref entity.Get<ObjectComponent>();
        objComp.ObGo = go;
        objComp.ObSr = objComp.ObGo.GetComponent<SpriteRenderer>();
        objComp.ObTransform = objComp.ObGo.transform;
        objComp.ObTransform.position = pos;
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
