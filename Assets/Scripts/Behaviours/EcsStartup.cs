using Leopotam.Ecs;
using UnityEngine;

public class EcsStartup : MonoBehaviour
{
    EcsWorld world;
    EcsSystems UpdateSystems,
        FixedUpdateSystems;

    public void StartGame(StaticData staticData, PoolSystem pool)
    {
        world = new EcsWorld();
        UpdateSystems = new EcsSystems(world);
        UpdateSystems
            .Add(new SpawnSystem())
            .Add(new EnemyTargetDispencerSystem())
            .Add(new StopToAttackSystem())
            .Add(new AttackSystem())
            .Inject(staticData)
            .Inject(pool)
            .Init();
        FixedUpdateSystems = new EcsSystems(world);
        FixedUpdateSystems.Add(new MovingSystem()).Inject(staticData).Init();
    }

    void Update()
    {
        UpdateSystems?.Run();
    }

    private void FixedUpdate()
    {
        FixedUpdateSystems?.Run();
    }

    void OnDestroy()
    {
        // Уничтожаем подключенные системы.
        UpdateSystems.Destroy();
        // Очищаем окружение.
        world.Destroy();
    }
}
