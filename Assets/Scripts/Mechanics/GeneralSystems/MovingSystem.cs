using Leopotam.Ecs;
using UnityEngine;

sealed class MovingSystem : IEcsRunSystem
{
    private EcsWorld world;
    private StaticData staticData;
    private EcsFilter<Movable> movableFilter;
    private Vector2 finalPos;

    public void Run()
    {
        if (movableFilter.GetEntitiesCount() > 0)
        {
            foreach (int i in movableFilter)
            {
                EcsEntity entity = movableFilter.GetEntity(i);
                if (!entity.Has<Enemy>() || entity.Has<Navigated>() && entity.Has<HasTarget>())
                {
                    ref Movable movable = ref movableFilter.Get1(i);
                    Transform movTransform = movable.ObjectTransform;

                    movTransform.position = Vector2.MoveTowards(
                        movTransform.position,
                        movable.Destination,
                        movable.Speed
                    );
                    if (
                        Vector2.Distance((Vector2)movTransform.position, movable.Destination)
                        <= movable.Speed
                    )
                    {
                        entity.Del<Movable>();
                    }
                }
                else
                {
                    entity.Del<Movable>();
                    entity.ChangeColor(Color.green);
                }
            }
        }
    }

    private void MoveEntity(ref EcsEntity entity) { }
}
