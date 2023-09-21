using Leopotam.Ecs;

public class CleaningTargetsSystem : IEcsRunSystem
{
    private EcsFilter<HasTarget> targetsFilter;

    public void Run()
    {
        foreach (int i in targetsFilter)
        {
            ref HasTarget targets = ref targetsFilter.Get1(i);
            EcsEntity targetEntity = targets.KillList[0];
            ref Health targetHp = ref targetEntity.Get<Health>();
            if (targetHp.HP <= 0)
            {
                targets.KillList.RemoveAt(0);
                if (targets.KillList.Count == 0)
                {
                    EcsEntity entity = targetsFilter.GetEntity(i);
                    entity.Del<HasTarget>();
                    entity.Del<Movable>();
                }
            }
        }
    }
}
