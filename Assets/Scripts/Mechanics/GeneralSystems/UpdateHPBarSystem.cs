using Leopotam.Ecs;
using UnityEngine;

sealed class UpdateHPBarSystem : IEcsRunSystem
{
    private EcsFilter<HealthBar, Health> hpBarFilter;
    private Pool poolSystem;

    public void Run()
    {
        foreach (int i in hpBarFilter)
        {
            EcsEntity ent = hpBarFilter.GetEntity(i);
            if (ent.Has<DeadMarker>())
            {
                ref HealthBar healthbar = ref hpBarFilter.Get1(i);
                Object.Destroy(healthbar.hpBarTransform.gameObject);
                ent.Destroy();
            }
            else
            {
                ref HealthBar healthBar = ref hpBarFilter.Get1(i);
                ref Health health = ref hpBarFilter.Get2(i);
                float hpPercent = health.HP / health.MaxHp;
                healthBar.healthBarFill.fillAmount = hpPercent;
                healthBar.healthBarFill.color = new Color(1 - hpPercent, hpPercent, 0, 1);
            }
        }
    }
}
