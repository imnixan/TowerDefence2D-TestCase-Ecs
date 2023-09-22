using Leopotam.Ecs;
using UnityEngine;

sealed class HealthBarMovingSystem : IEcsRunSystem
{
    private EcsWorld world;
    private StaticData staticData;
    private EcsFilter<Movable, HealthBar> movableWithHPFilter;

    public void Run()
    {
        if (movableWithHPFilter.GetEntitiesCount() > 0)
        {
            foreach (int i in movableWithHPFilter)
            {
                ref Movable movable = ref movableWithHPFilter.Get1(i);
                ref HealthBar healthBar = ref movableWithHPFilter.Get2(i);

                healthBar.hpBarTransform.position =
                    (Vector2)movable.ObjectTransform.position + Vector2.up * 3;
            }
        }
    }
}
