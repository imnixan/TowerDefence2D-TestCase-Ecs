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
                targetSeaker.Del<Movable>();
                targetSeaker.Del<HasTarget>();
            }
        }
    }
}
