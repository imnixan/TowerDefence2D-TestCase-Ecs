using Leopotam.Ecs;
using UnityEngine;

sealed class DeleteHealthBarSystem : IEcsRunSystem
{
    private EcsFilter<HealthBar, DeadMarker> healthFilter;
    private ObjectsPool poolSystem;

    public void Run()
    {
        foreach (int i in healthFilter)
        {
            ref HealthBar healthbar = ref healthFilter.Get1(i);

            Object.Destroy(healthbar.hpBarTransform.gameObject);
        }
    }
}
