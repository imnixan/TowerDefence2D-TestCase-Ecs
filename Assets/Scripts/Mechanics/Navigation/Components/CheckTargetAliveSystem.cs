using Leopotam.Ecs;
using UnityEngine;

public class CheckTargetAliveSystem : IEcsRunSystem
{
    private EcsFilter<HasTarget> targetsFilter;
    private StaticData staticData;

    public void Run()
    {
        foreach (int i in targetsFilter)
        {
            EcsEntity entity = targetsFilter.GetEntity(i);
            ref HasTarget targets = ref targetsFilter.Get1(i);
            EcsEntity targetEntity = targets.Target;
            if (targetEntity.Has<DeadMarker>())
            {
                entity.Del<HasTarget>();
                entity.Del<InBattleMarker>();
                entity.Del<InBattleMarker>();
                entity.ChangeColor(Color.red);
                entity.AddMovable(staticData);
            }
        }
    }
}
