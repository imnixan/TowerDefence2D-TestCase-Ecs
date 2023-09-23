using Leopotam.Ecs;
using System.Collections.Generic;

struct Tower
{
    public StaticData.TowerType TowerType;
}

struct BaseTowerMarker { }

struct TowerObserver { }

struct UpdateTowersMarker { }

struct UpgradeTowerMarker { }

struct AttackTower : IEcsAutoReset<AttackTower>
{
    public List<EcsEntity> TargetList;

    public void AutoReset(ref AttackTower c)
    {
        c.TargetList = new List<EcsEntity>();
    }
}
