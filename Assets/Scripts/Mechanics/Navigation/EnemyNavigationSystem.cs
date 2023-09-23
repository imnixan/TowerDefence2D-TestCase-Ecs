using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

public class EnemyNavigationSystem : IEcsRunSystem
{
    private EcsFilter<Navigated, Enemy, Attacker>.Exclude<Movable> navigatedFilter;
    private StaticData staticData;

    public void Run()
    {
        foreach (int i in navigatedFilter)
        {
            ref EcsEntity entity = ref navigatedFilter.GetEntity(i);
            entity.AddMovable(staticData);
            ref Movable movable = ref entity.Get<Movable>();
            ref Navigated navigated = ref navigatedFilter.Get1(i);

            navigated.PathPointIndex++;

            if (navigated.PathPointIndex < navigated.Path.Count)
            {
                navigated.LastPointIndex = navigated.PathPointIndex;
                movable.Destination = navigated.Path[navigated.PathPointIndex].ConvertToWorld(
                    staticData.FieldSize
                );
            }
            else
            {
                entity.Del<Navigated>();
            }
        }
    }
}
