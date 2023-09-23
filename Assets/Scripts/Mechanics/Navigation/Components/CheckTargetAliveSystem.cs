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
            ref EcsEntity entity = ref targetsFilter.GetEntity(i);
            ref HasTarget targets = ref targetsFilter.Get1(i);
            ref EcsEntity targetEntity = ref targets.Target;
            if (targetEntity.Has<DeadMarker>())
            {
                entity.Del<HasTarget>();
                entity.Del<InBattleMarker>();
                entity.Del<InBattleMarker>();
                entity.AddMovable(staticData);
            }
        }
    }
}
