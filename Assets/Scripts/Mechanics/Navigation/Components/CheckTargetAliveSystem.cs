using Leopotam.Ecs;

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
            EcsEntity targetEntity = targets.target;
            if (targetEntity.Has<DeadMarker>())
            {
                entity.Del<HasTarget>();
                entity.Del<InBattleMarker>();
                if (entity.Has<Enemy>() && !entity.Has<Movable>())
                {
                    entity.AddMovable(staticData);
                }
            }
        }
    }
}
