using Leopotam.Ecs;
using UnityEngine;

sealed class MovingSystem : IEcsRunSystem
{
    private EcsWorld world;
    private StaticData staticData;
    private EcsFilter<Movable, Navigated> movableFilter;
    private Vector2 finalPos;

    public void Run()
    {
        if (movableFilter.GetEntitiesCount() > 0)
        {
            foreach (int i in movableFilter)
            {
                ref Movable movable = ref movableFilter.Get1(i);
                Transform movTransform = movable.ObjectTransform;
                ref Navigated navigation = ref movableFilter.Get2(i);

                if (navigation.PathPointIndex >= navigation.Path.Count)
                {
                    ref EcsEntity entity = ref movableFilter.GetEntity(i);
                    entity.Del<Movable>();
                    entity.Del<Navigated>();
                    return;
                }
                finalPos = navigation.Path[navigation.PathPointIndex].ConvertToWorld();

                movTransform.position = Vector2.MoveTowards(
                    movTransform.position,
                    finalPos,
                    movable.Speed
                );

                if ((Vector2)movTransform.position == finalPos)
                {
                    navigation.PathPointIndex++;
                }
            }
        }
    }
}
