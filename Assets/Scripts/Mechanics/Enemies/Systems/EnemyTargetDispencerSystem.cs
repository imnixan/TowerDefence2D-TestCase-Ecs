using Leopotam.Ecs;

using EpPathFinding.cs;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetDispencerSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld world;
    private StaticData staticData;

    private EcsFilter<Enemy, Attacker>.Exclude<Movable, HasTarget> noNavFilter;
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
            if (aliveTowersFilter.GetEntitiesCount() > 0)
            {
                ref EcsEntity entity = ref noNavFilter.GetEntity(0);

                ref Navigated navigatedComp = ref entity.Get<Navigated>();
                navigatedComp.Path = GetPathForNearestTower(ref entity);
                ref HasTarget hasTargets = ref entity.Get<HasTarget>();
                hasTargets.Target = closestTower;
            }
            else
            {
                foreach (int i in noNavFilter)
                {
                    ref EcsEntity entity = ref noNavFilter.GetEntity(i);
                    entity.Del<Attacker>();
                    ref ObjectComponent objComp = ref entity.Get<ObjectComponent>();
                }
            }
        }
    }

    private List<GridPos> GetPathForNearestTower(ref EcsEntity entity)
    {
        ref ObjectComponent pbjComp = ref entity.Get<ObjectComponent>();
        Vector2 startPos = pbjComp.ObTransform.position;

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
