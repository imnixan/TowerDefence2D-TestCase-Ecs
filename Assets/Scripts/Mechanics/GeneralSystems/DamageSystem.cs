using Leopotam.Ecs;
using UnityEngine;

public class DamageSystem : IEcsRunSystem
{
    private EcsFilter<Health, DamageRecieveMarker> damageFilter;

    public void Run()
    {
        foreach (int i in damageFilter)
        {
            ref Health health = ref damageFilter.Get1(i);
            ref DamageRecieveMarker damage = ref damageFilter.Get2(i);
            health.HP -= damage.Damage;
            if (health.HP <= 0)
            {
                ref EcsEntity entity = ref damageFilter.GetEntity(i);
                entity.Get<DeadMarker>();
                Debug.Log("text");
            }
        }
    }
}
