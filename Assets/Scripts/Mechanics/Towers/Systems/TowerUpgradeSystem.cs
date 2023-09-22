using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

public class TowerUpgradeSystem : IEcsRunSystem
{
    private EcsFilter<UpgradeTowerMarker, ObjectComponent, Tower> upgradeFilter;
    private StaticData staticData;

    public void Run()
    {
        foreach (int i in upgradeFilter)
        {
            EcsEntity towerEntity = upgradeFilter.GetEntity(i);
            ref Tower tower = ref upgradeFilter.Get3(i);
            switch (tower.TowerType)
            {
                case StaticData.TowerType.BuildPlace:
                    UpgradeTowerToDefence(towerEntity);
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

        HealTower(towerEntity);
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

        HealTower(towerEntity);
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
