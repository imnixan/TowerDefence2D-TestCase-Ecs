using Leopotam.Ecs;
using UnityEngine;

sealed class EnemyDeathSystem : IEcsRunSystem
{
    private EcsFilter<Enemy, DeadMarker> enemyFilter;

    public void Run()
    {
        foreach (int i in enemyFilter)
        {
            ref EcsEntity enemyEntity = ref enemyFilter.GetEntity(i);
            ref ObjectComponent objComp = ref enemyEntity.Get<ObjectComponent>();
            Object.Destroy(objComp.ObGo);
            enemyEntity.Destroy();
        }
    }
}
