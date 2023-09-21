using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

public class AttackSystem : IEcsRunSystem
{
    private EcsFilter<Attacking, Attacker, ObjectComponent, HasTargets> attackingFilter;
    private StaticData staticData;

    public void Run()
    {
        foreach (int i in attackingFilter)
        {
            ref Attacking attacking = ref attackingFilter.Get1(i);
            ref Attacker attacker = ref attackingFilter.Get2(i);
            ref ObjectComponent objComp = ref attackingFilter.Get3(i);
            ref HasTargets hasTargets = ref attackingFilter.Get4(i);

            ref ObjectComponent targetObjComp = ref hasTargets.KillList[0].Get<ObjectComponent>();

            if (
                Vector2.Distance(objComp.ObTransform.position, targetObjComp.ObTransform.position)
                <= attacker.AttackRange
            )
            {
                if (attacking.LastAttack + attacking.AttackRecharge <= Time.time)
                {
                    attacking.LastAttack = Time.time;
                }
            }
            else
            {
                EcsEntity entity = attackingFilter.GetEntity(i);
                if (entity.Has<Enemy>())
                {
                    ref Enemy enemy = ref entity.Get<Enemy>();
                    entity.Del<Attacking>();
                    ref Movable entityMovable = ref entity.Get<Movable>();
                    entityMovable.ObjectTransform = objComp.ObGo.transform;
                    entityMovable.Speed = staticData.EnemiesSpeed[(int)enemy.EnemyType];
                    entityMovable.Rb = objComp.ObGo.GetComponent<Rigidbody2D>();
                }
            }
        }
    }
}
