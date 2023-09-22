using Leopotam.Ecs;

public class UpdateTowersSystem : IEcsRunSystem
{
    private EcsFilter<UpdateTowersMarker> updateTowersFilter;
    private EcsFilter<Movable, HasTarget> targetSeakers;

    public void Run()
    {
        if (updateTowersFilter.GetEntitiesCount() > 0)
        {
            foreach (int i in targetSeakers)
            {
                EcsEntity targetSeaker = targetSeakers.GetEntity(i);
                ref HasTarget hasTarget = ref targetSeakers.Get2(i);
                if (!(targetSeaker.Has<InBattleMarker>()))
                {
                    targetSeaker.Del<Movable>();
                    targetSeaker.Del<HasTarget>();
                    if (targetSeaker.Has<Navigated>())
                    {
                        targetSeaker.Del<Navigated>();
                    }
                }
                else if (!hasTarget.target.Has<DeadMarker>()) { }
            }
        }
    }
}
