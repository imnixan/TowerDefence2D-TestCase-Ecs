using Leopotam.Ecs;

using EpPathFinding.cs;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetDispencerSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld world;
    private StaticData staticData;

    private EcsFilter<Movable, Enemy, Attacker>.Exclude<HasTarget, InBattleMarker> noNavFilter;
    private EcsFilter<Tower, Health> aliveTowersFilter;
    private BaseGrid field;
    private JumpPointParam pathSearchingParams;
    private EcsEntity closestTower;
    private GridPos finalPos;

    public void Init()
    {
        this.field = staticData.Field;
        this.pathSearchingParams = staticData.PathSearchingParams;
    }

    public void Run()
    {
        if (noNavFilter.GetEntitiesCount() > 0)
        {
            ref EcsEntity obj = ref noNavFilter.GetEntity(0);
            if (aliveTowersFilter.GetEntitiesCount() > 0)
            {
                obj.Get<ObjectComponent>().ObSr.color = Color.blue;
                ref Navigated navigatedComp = ref obj.Get<Navigated>();
                navigatedComp.Path = GetPathForNearestTower(ref obj);
                ref HasTarget hasTargets = ref obj.Get<HasTarget>();
                hasTargets.target = closestTower;
            }
            else
            {
                EcsEntity entity = noNavFilter.GetEntity(0);
                entity.Del<Movable>();
                ref ObjectComponent objComp = ref entity.Get<ObjectComponent>();
                objComp.ObSr.color = Color.green;
            }
        }
    }

    private List<GridPos> GetPathForNearestTower(ref EcsEntity entity)
    {
        ref Movable movable = ref entity.Get<Movable>();
        Vector2 startPos = movable.ObjectTransform.position;

        SetClosestTower(ref entity);

        ref ObjectComponent objComp = ref closestTower.Get<ObjectComponent>();

        finalPos = objComp.ObTransform.position.ConvertToNav(staticData.FieldSize);
        pathSearchingParams.Reset(startPos.ConvertToNav(staticData.FieldSize), finalPos);
        return JumpPointFinder.FindPath(pathSearchingParams);
    }

    private void SetClosestTower(ref EcsEntity entity)
    {
        float distance = 0;
        float minimalDistance = staticData.FieldSize.x * staticData.FieldSize.y;
        foreach (int i in aliveTowersFilter)
        {
            ref EcsEntity tower = ref aliveTowersFilter.GetEntity(i);
            ref ObjectComponent towerOb = ref tower.Get<ObjectComponent>();
            ref ObjectComponent entityOb = ref entity.Get<ObjectComponent>();

            distance = Vector2.Distance(
                towerOb.ObGo.transform.position,
                entityOb.ObGo.transform.position
            );
            if (distance < minimalDistance)
            {
                closestTower = tower;
                minimalDistance = distance;
            }
        }
    }
}
