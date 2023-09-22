using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.UI;

class SpawnSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsFilter<Enemy> enemyFilter;
    private StaticData staticData;
    private EcsWorld world;
    private Pool pool;

    private int maxTop,
        maxRight;

    public void Init()
    {
        maxTop = staticData.FieldSize.y / 2;
        maxRight = staticData.FieldSize.x / 2;
    }

    public void Run()
    {
        if (enemyFilter.GetEntitiesCount() < staticData.MaxEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        EcsEntity enemyEntity = world.NewEntity();
        ref Enemy enemy = ref enemyEntity.Get<Enemy>();
        enemy.EnemyType = (StaticData.EnemyType)
            UnityEngine.Random.Range(0, Enum.GetNames(typeof(StaticData.EnemyType)).Length);

        ref ObjectComponent enemyObj = ref enemyEntity.Get<ObjectComponent>();
        enemyEntity.AddObjectComp(
            staticData,
            pool.GetEnemyObject(),
            StaticData.UnitType.Enemy,
            GetSpawnPoint()
        );
        enemyObj.unitSprites = staticData.EnemiesSprites[(int)enemy.EnemyType];
        enemyObj.ObSr.sprite = enemyObj.unitSprites.IdleSprites[0];

        UnitData unit = staticData.EnemiesData[(int)enemy.EnemyType];
        if (unit.Ranged)
        {
            enemyEntity.Get<RangeAttackUnit>();
        }
        else
        {
            enemyEntity.Get<MeleeAttackUnit>();
        }

        enemyEntity.AddAttacker(staticData);

        ref Health health = ref enemyEntity.Get<Health>();
        health.HP = unit.HP;
        health.MaxHp = unit.HP;
    }

    private Vector2 GetSpawnPoint()
    {
        bool fromVertical = UnityEngine.Random.value > 0.5f;
        if (fromVertical)
        {
            bool fromTop = UnityEngine.Random.value > 0.5f;
            return new Vector2(
                UnityEngine.Random.Range(-maxRight, maxRight),
                fromTop ? maxTop - 1 : -maxTop
            );
        }
        else
        {
            bool fromRight = UnityEngine.Random.value > 0.5f;
            return new Vector2(
                fromRight ? maxRight - 1 : -maxRight,
                UnityEngine.Random.Range(-maxTop, maxTop)
            );
        }
    }
}
