using Leopotam.Ecs;
using UnityEngine;

class SpawnSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsFilter<Enemy> enemyFilter;
    private StaticData staticData;
    private EcsWorld world;
    private PoolSystem pool;
    private int maxTop,
        maxRight;

    public void Init()
    {
        maxTop = staticData.FieldSize.y / 2;
        maxRight = staticData.FieldSize.x / 2;
        foreach (Transform tower in staticData.towers)
        {
            EcsEntity towerEntity = world.NewEntity();
            ref Tower towerComp = ref towerEntity.Get<Tower>();
            ref ObjectComponent objComp = ref towerEntity.Get<ObjectComponent>();
            objComp.ObGo = tower.gameObject;
            objComp.ObTransform = tower;
            objComp.ObSr = tower.gameObject.GetComponent<SpriteRenderer>();
        }
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
        enemy.EnemyType = StaticData.EnemyType.Goblin;

        ref ObjectComponent enemyObj = ref enemyEntity.Get<ObjectComponent>();
        enemyObj.ObGo = pool.GetEnemyObject();
        enemyObj.ObSr = enemyObj.ObGo.GetComponent<SpriteRenderer>();
        enemyObj.ObTransform = enemyObj.ObGo.transform;
        enemyObj.ObTransform.position = GetSpawnPoint();

        ref Movable movableComp = ref enemyEntity.Get<Movable>();
        movableComp.ObjectTransform = enemyObj.ObGo.transform;
        movableComp.Speed = staticData.EnemiesSpeed[(int)enemy.EnemyType];

        ref Attacker attacker = ref enemyEntity.Get<Attacker>();
        attacker.AttackRange = staticData.EnemiesAttackRange[(int)enemy.EnemyType];
        Debug.Log("test");
    }

    private Vector2 GetSpawnPoint()
    {
        bool fromVertical = Random.value > 0.5f;
        if (fromVertical)
        {
            bool fromTop = Random.value > 0.5f;
            return new Vector2(Random.Range(-maxRight, maxRight), fromTop ? maxTop : -maxTop);
        }
        else
        {
            bool fromRight = Random.value > 0.5f;
            return new Vector2(fromRight ? maxRight : -maxRight, Random.Range(-maxTop, maxTop));
        }
    }
}
