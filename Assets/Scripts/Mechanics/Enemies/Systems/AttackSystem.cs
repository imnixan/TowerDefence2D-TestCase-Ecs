using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

public class AttackSystem : IEcsRunSystem
{
    private EcsFilter<InBattleMarker, Attacker, ObjectComponent, HasTarget> attackingFilter;
    private StaticData staticData;

    public void Run()
    {
        foreach (int i in attackingFilter)
        {
            ref InBattleMarker attacking = ref attackingFilter.Get1(i);
            ref Attacker attacker = ref attackingFilter.Get2(i);
            ref ObjectComponent objComp = ref attackingFilter.Get3(i);
            ref HasTarget hasTargets = ref attackingFilter.Get4(i);

            EcsEntity targetEntity = hasTargets.KillList[0];
            ref ObjectComponent targetObjComp = ref targetEntity.Get<ObjectComponent>();

            if (
                Vector2.Distance(objComp.ObTransform.position, targetObjComp.ObTransform.position)
                <= attacker.AttackRange
            )
            {
                if (attacking.LastAttack + attacker.RechargeTime <= Time.time)
                {
                    attacking.LastAttack = Time.time;

                    ref DamageRecieveMarker damageReciever =
                        ref targetEntity.Get<DamageRecieveMarker>();
                    damageReciever.Damage += attacker.Damage;
                }
            }
            else
            {
                EcsEntity entity = attackingFilter.GetEntity(i);
                if (entity.Has<Enemy>())
                {
                    ref Enemy enemy = ref entity.Get<Enemy>();
                    entity.Del<InBattleMarker>();

                    entity.AddMovable(staticData);
                }
            }
        }
    }
}
