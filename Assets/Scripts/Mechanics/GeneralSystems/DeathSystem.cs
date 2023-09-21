using Leopotam.Ecs;

sealed class DeathSystem : IEcsRunSystem
{
    private EcsFilter<Health> healthFilter;
    private PoolSystem poolSystem;

    public void Run()
    {
        foreach (int i in healthFilter)
        {
            ref Health health = ref healthFilter.Get1(i);
            if (health.HP <= 0)
            {
                EcsEntity deadEntity = healthFilter.GetEntity(i);
                ref ObjectComponent objComp = ref deadEntity.Get<ObjectComponent>();
                poolSystem.ReturnObjectInPool(objComp.ObGo);
                deadEntity.Destroy();
            }
        }
    }
}
