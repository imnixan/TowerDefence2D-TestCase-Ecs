using Leopotam.Ecs;
using UnityEngine;

sealed class BaseTowersInit : IEcsInitSystem
{
    private StaticData staticData;
    private EcsWorld world;

    public void Init()
    {
        foreach (Transform tower in staticData.towers)
        {
            EcsEntity towerEntity = world.NewEntity();
            ref Tower towerComp = ref towerEntity.Get<Tower>();
            towerComp.TowerType = StaticData.TowerType.BaseTower;

            UnitData unitData = staticData.TowersData[(int)towerComp.TowerType];
            ref ObjectComponent objComp = ref towerEntity.Get<ObjectComponent>();
            objComp.ObGo = tower.gameObject;
            objComp.ObTransform = tower;
            objComp.ObSr = tower.gameObject.GetComponent<SpriteRenderer>();
            objComp.UnitType = StaticData.UnitType.Tower;

            towerEntity.Get<BaseTowerMarker>();
            ref Health health = ref towerEntity.Get<Health>();
            health.HP = unitData.HP;
        }
    }
}
