using Leopotam.Ecs;
using UnityEngine;

public class EndGameSystem : IEcsRunSystem
{
    private EcsFilter<BaseTowerMarker> baseTowerFilter;

    public void Run()
    {
        if (baseTowerFilter.GetEntitiesCount() == 0)
        {
            Debug.Log("EndGame");
        }
    }
}
