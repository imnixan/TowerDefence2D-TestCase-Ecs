using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

sealed class BaseTowersInit : IEcsInitSystem
{
    private StaticData staticData;
    private EcsWorld world;

    public void Init()
    {
        EcsEntity towerObserver = world.NewEntity();
        ref TowerObserver to = ref towerObserver.Get<TowerObserver>();

        foreach (Transform tower in staticData.towers)
        {
            EcsEntity towerEntity = world.NewEntity();
            ref Tower towerComp = ref towerEntity.Get<Tower>();
            towerComp.TowerType = StaticData.TowerType.BaseTower;

            UnitData unitData = staticData.TowersData[(int)towerComp.TowerType];
            ref ObjectComponent objComp = ref towerEntity.Get<ObjectComponent>();
            towerEntity.AddObjectComp(
                staticData,
                tower.gameObject,
                StaticData.UnitType.Tower,
                tower.position
            );

            if (objComp.ObGo.CompareTag("BaseTower"))
            {
                towerEntity.Get<BaseTowerMarker>();
                ref Health health = ref towerEntity.Get<Health>();
                health.HP = unitData.HP;
                health.MaxHp = unitData.HP;

                ref HealthBar healthBar = ref towerEntity.Get<HealthBar>();
                healthBar.hpBarTransform = Object
                    .Instantiate(
                        staticData.healthBarPrefab,
                        (Vector2)objComp.ObTransform.position + Vector2.up * 3,
                        new Quaternion(),
                        staticData.healthBarCanvas
                    )
                    .transform;
                healthBar.healthBarFill = healthBar.hpBarTransform
                    .Find("Filler")
                    .GetComponent<Image>();
            }
            staticData.Field.SetWalkableAt(
                objComp.ObTransform.position.ConvertToNav(staticData.FieldSize),
                false
            );
        }
    }
}
