using Leopotam.Ecs;
using UnityEngine;

public class DamageSystem : IEcsRunSystem
{
    private EcsFilter<Health, DamageRecieveMarker> damageFilter;
    private EcsFilter<HealthBar, Health> hpBarFilter;

    public void Run()
    {
        foreach (int i in damageFilter)
        {
            ref Health health = ref damageFilter.Get1(i);
            ref DamageRecieveMarker damage = ref damageFilter.Get2(i);
            health.HP -= damage.Damage;
            if (health.HP <= 0)
            {
                EcsEntity entity = damageFilter.GetEntity(i);
                entity.Get<DeadMarker>();
            }
        }

        foreach (int i in hpBarFilter)
        {
            ref HealthBar healthBar = ref hpBarFilter.Get1(i);
            ref Health health = ref hpBarFilter.Get2(i);
            float hpPercent = health.HP / health.MaxHp;
            healthBar.healthBarFill.fillAmount = hpPercent;
            healthBar.healthBarFill.color = new Color(1 - hpPercent, hpPercent, 0, 1);
        }
    }
}
