using Leopotam.Ecs;
using UnityEngine;

public class EcsStartup : MonoBehaviour
{
    EcsWorld world;
    EcsSystems UpdateSystems,
        FixedUpdateSystems;

    public void StartGame(StaticData staticData, Pool pool)
    {
        world = new EcsWorld();
        UpdateSystems = new EcsSystems(world);
        UpdateSystems
            .Add(new BaseTowersInit())
            .Add(new InputSystem())
            .Add(new TowerUpgradeSystem())
            .OneFrame<UpgradeTowerMarker>()
            .Add(new TowerUpdateSystem())
            .OneFrame<UpdateTowersMarker>()
            .Add(new SpawnSystem())
            .Add(new EnemyTargetDispencerSystem())
            .Add(new TowerTargetDispencerSystem())
            .Add(new EnemyNavigationSystem())
            .Add(new StopToAttackSystem())
            .Add(new AttackSystem())
            .Add(new CreateProjectileSystem())
            .OneFrame<CreateProjectileMarker>()
            .Add(new ProjectileDamageSystem())
            .Add(new DamageSystem())
            .OneFrame<DamageRecieveMarker>()
            .Add(new CheckTargetAliveSystem())
            .Add(new UpdateHPBarSystem())
            .Add(new EnemyDeathSystem())
            .Add(new TowerDeathSystem())
            .OneFrame<DeadMarker>()
            .Inject(staticData)
            .Inject(pool)
            .Init();
        FixedUpdateSystems = new EcsSystems(world);
        FixedUpdateSystems
            .Add(new MovingSystem())
            .Add(new HealthBarMovingSystem())
            .Inject(staticData)
            .Init();
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
        if (UpdateSystems != null)
        {
            UpdateSystems.Destroy();
        }
        if (FixedUpdateSystems != null)
        {
            FixedUpdateSystems.Destroy();
        }
        // Очищаем окружение.
        if (world != null)
        {
            world.Destroy();
        }
    }
}
