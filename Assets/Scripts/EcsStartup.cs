using Leopotam.Ecs;
using UnityEngine;

public class EcsStartup : MonoBehaviour
{
    EcsWorld world;
    EcsSystems systems;

    private StaticData staticData;
    private PoolSystem pool;

    public void StartGame(StaticData staticData, PoolSystem pool)
    {
        this.staticData = staticData;
        this.pool = pool;
        world = new EcsWorld();
        systems = new EcsSystems(world);
        systems.Init();
    }

    void Update()
    {
        // Выполняем все подключенные системы.
        systems.Run();
    }

    void OnDestroy()
    {
        // Уничтожаем подключенные системы.
        systems.Destroy();
        // Очищаем окружение.
        world.Destroy();
    }
}
