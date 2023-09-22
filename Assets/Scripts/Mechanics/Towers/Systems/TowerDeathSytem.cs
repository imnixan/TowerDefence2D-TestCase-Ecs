using Leopotam.Ecs;
using UnityEngine;

sealed class TowerDeathSystem : IEcsRunSystem
{
    private EcsFilter<Tower, DeadMarker> towerFilter;

    public void Run()
    {
        foreach (int i in towerFilter)
        {
            EcsEntity towerEntity = towerFilter.GetEntity(i);
            ref Tower tower = ref towerFilter.Get1(i);
            ref ObjectComponent objComp = ref towerEntity.Get<ObjectComponent>();
            if (towerEntity.Has<BaseTowerMarker>())
            {
                objComp.ObSr.enabled = false;
                towerEntity.Del<BaseTowerMarker>();
            }
            else
            {
                tower.TowerType = StaticData.TowerType.BuildPlace;
                towerEntity.Del<Health>();
                towerEntity.Del<Attacker>();
            }
        }
    }
}
