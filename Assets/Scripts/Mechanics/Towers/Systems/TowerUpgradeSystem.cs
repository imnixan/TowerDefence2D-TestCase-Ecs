using System.Collections;
using UnityEngine;
using Leopotam.Ecs;
using UnityEngine.UI;

public class TowerUpgradeSystem : IEcsRunSystem
{
    private EcsFilter<UpgradeTowerMarker, ObjectComponent, Tower> upgradeFilter;
    private StaticData staticData;

    public void Run()
    {
        foreach (int i in upgradeFilter)
        {
            ref EcsEntity towerEntity = ref upgradeFilter.GetEntity(i);
            ref Tower tower = ref upgradeFilter.Get3(i);
            switch (tower.TowerType)
            {
                case StaticData.TowerType.BuildPlace:
                    UpgradeTowerToDefence(towerEntity);
                    towerEntity.Get<UpdateTowersMarker>();
                    break;
                case StaticData.TowerType.DefenceTower:
                    UpgradeTowerToAttacker(towerEntity);

                    break;
                case StaticData.TowerType.AttackTower:
                    HealTower(towerEntity);
                    break;
                case StaticData.TowerType.BaseTower:
                    HealTower(towerEntity);
                    break;
            }
        }
    }

    private void UpgradeTowerToAttacker(EcsEntity towerEntity)
    {
        ref ObjectComponent objComp = ref towerEntity.Get<ObjectComponent>();
        ref Tower towerComp = ref towerEntity.Get<Tower>();
        towerComp.TowerType = StaticData.TowerType.AttackTower;
        towerEntity.AddObjectComp(
            staticData,
            objComp.ObGo,
            objComp.UnitType,
            objComp.ObTransform.position
        );
        objComp.unitSprites = staticData.TowersSprites[(int)towerComp.TowerType];
        objComp.ObSr.sprite = objComp.unitSprites.IdleSprites[0];

        UnitData unitData = staticData.TowersData[(int)towerComp.TowerType];

        ref Attacker attacker = ref towerEntity.Get<Attacker>();
        attacker.AttackRange = unitData.AttackRange;
        attacker.Damage = unitData.Damage;
        towerEntity.Get<RangeAttackUnit>();

        HealTower(towerEntity, unitData);
    }

    private void UpgradeTowerToDefence(EcsEntity towerEntity)
    {
        ref ObjectComponent objComp = ref towerEntity.Get<ObjectComponent>();
        ref Tower tower = ref towerEntity.Get<Tower>();
        tower.TowerType = StaticData.TowerType.DefenceTower;
        towerEntity.AddObjectComp(
            staticData,
            objComp.ObGo,
            objComp.UnitType,
            objComp.ObTransform.position
        );
        objComp.unitSprites = staticData.TowersSprites[(int)tower.TowerType];
        objComp.ObSr.sprite = objComp.unitSprites.IdleSprites[0];
        UnitData unitData = staticData.TowersData[(int)tower.TowerType];

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
        healthBar.healthBarFill = healthBar.hpBarTransform.Find("Filler").GetComponent<Image>();
        staticData.Field.SetWalkableAt(
            objComp.ObTransform.position.ConvertToNav(staticData.FieldSize),
            false
        );

        HealTower(towerEntity, unitData);
    }

    private void HealTower(EcsEntity towerEntity, UnitData unitData)
    {
        ref Tower towerComp = ref towerEntity.Get<Tower>();

        ref Health health = ref towerEntity.Get<Health>();
        health.HP = unitData.HP;
        health.MaxHp = unitData.HP;
    }

    private void HealTower(EcsEntity towerEntity)
    {
        ref Tower towerComp = ref towerEntity.Get<Tower>();
        UnitData unitData = staticData.TowersData[(int)towerComp.TowerType];
        ref Health health = ref towerEntity.Get<Health>();
        health.HP = unitData.HP;
        health.MaxHp = unitData.HP;
    }
}
