using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Events;

public class EndGameSystem : IEcsRunSystem
{
    public static event UnityAction GameEnd;
    private EcsFilter<BaseTowerMarker> baseTowerFilter;

    public void Run()
    {
        if (baseTowerFilter.GetEntitiesCount() == 0)
        {
            GameEnd?.Invoke();
        }
    }
}
