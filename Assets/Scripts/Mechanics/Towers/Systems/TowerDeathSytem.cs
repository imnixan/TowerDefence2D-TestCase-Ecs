using Leopotam.Ecs;
using UnityEngine;

sealed class TowerDeathSystem : IEcsRunSystem
{
    private EcsFilter<Tower, DeadMarker> towerFilter;
    private StaticData staticData;

    public void Run()
    {
        foreach (int i in towerFilter)
        {
            ref EcsEntity towerEntity = ref towerFilter.GetEntity(i);

            ref Tower tower = ref towerFilter.Get1(i);
            ref ObjectComponent objComp = ref towerEntity.Get<ObjectComponent>();
            towerEntity.Get<UpdateTowersMarker>();
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
                towerEntity.Del<RangeAttackUnit>();
                towerEntity.Del<HasTarget>();

                objComp.unitSprites = staticData.TowersSprites[(int)tower.TowerType];
                objComp.ObSr.sprite = objComp.unitSprites.IdleSprites[0];
            }
        }
    }
}
