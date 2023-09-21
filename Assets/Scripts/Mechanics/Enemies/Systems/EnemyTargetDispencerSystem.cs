using Leopotam.Ecs;

using EpPathFinding.cs;

using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetDispencerSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld world;
    private StaticData staticData;

    private EcsFilter<Movable, Enemy>.Exclude<Navigated, Attacking> noNavFilter;
    private EcsFilter<Tower> towersFilter;
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
        foreach (int i in noNavFilter)
        {
            ref EcsEntity obj = ref noNavFilter.GetEntity(i);

            ref Navigated navigatedComp = ref obj.Get<Navigated>();

            navigatedComp.Path = GetPathForNearestTower(ref obj);
            if (navigatedComp.Path != null)
            {
                Debug.Log("nav path " + navigatedComp.Path);
                ref HasTargets hasTargets = ref obj.Get<HasTargets>();
                hasTargets.KillList.Add(closestTower);
            }
            else
            {
                obj.Del<Movable>();
            }
        }
    }

    private List<GridPos> GetPathForNearestTower(ref EcsEntity entity)
    {
        ref Movable movable = ref entity.Get<Movable>();
        Vector2 startPos = movable.ObjectTransform.position;
        SetClosestTower(ref entity);
        if (closestTower != EcsEntity.Null)
        {
            ref ObjectComponent objComp = ref closestTower.Get<ObjectComponent>();
            finalPos = objComp.ObTransform.position.ConvertToNav();
            pathSearchingParams.Reset(startPos.ConvertToNav(), finalPos);
            return JumpPointFinder.FindPath(pathSearchingParams);
        }
        return null;
    }

    private void SetClosestTower(ref EcsEntity entity)
    {
        float distance = 0;
        float minimalDistance = 0;
        foreach (int i in towersFilter)
        {
            ref EcsEntity tower = ref towersFilter.GetEntity(i);
            ref ObjectComponent towerOb = ref tower.Get<ObjectComponent>();
            ref ObjectComponent entityOb = ref entity.Get<ObjectComponent>();
            distance = Vector2.Distance(
                towerOb.ObGo.transform.position,
                entityOb.ObGo.transform.position
            );
            if (minimalDistance < distance)
            {
                closestTower = tower;
                minimalDistance = distance;
            }
        }
    }
}
