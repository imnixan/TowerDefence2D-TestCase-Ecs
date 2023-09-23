using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

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
            ref ObjectComponent objComp = ref towerEntity.Get<ObjectComponent>();
            towerEntity.AddObjectComp(
                staticData,
                tower.gameObject,
                StaticData.UnitType.Tower,
                tower.position
            );

            if (objComp.ObGo.CompareTag("BaseTower"))
            {
                towerComp.TowerType = StaticData.TowerType.BaseTower;
                towerEntity.Get<BaseTowerMarker>();

                UnitData unitData = staticData.TowersData[(int)towerComp.TowerType];

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
                staticData.Field.SetWalkableAt(
                    objComp.ObTransform.position.ConvertToNav(staticData.FieldSize),
                    false
                );
            }
            else
            {
                towerComp.TowerType = StaticData.TowerType.BuildPlace;
            }
            objComp.unitSprites = staticData.TowersSprites[(int)towerComp.TowerType];
            objComp.ObSr.sprite = objComp.unitSprites.IdleSprites[0];

            staticData.Field.SetWalkableAt(
                objComp.ObTransform.position.ConvertToNav(staticData.FieldSize),
                false
            );
        }
    }
}
