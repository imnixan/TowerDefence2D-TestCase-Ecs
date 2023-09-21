using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

sealed class StopToAttackSystem : IEcsRunSystem
{
    private EcsWorld world;
    private EcsFilter<Movable, Attacker, HasTarget> attackFilter;
    private float distanceToTarget;

    public void Run()
    {
        foreach (int i in attackFilter)
        {
            ref EcsEntity attackerEntity = ref attackFilter.GetEntity(i);
            ref Attacker attacker = ref attackFilter.Get2(i);
            ref ObjectComponent objComp = ref attackerEntity.Get<ObjectComponent>();
            ref HasTarget wantKillTarget = ref attackFilter.Get3(i);
            ref ObjectComponent targetObjComp = ref wantKillTarget.KillList[
                0
            ].Get<ObjectComponent>();
            distanceToTarget = Vector2.Distance(
                (Vector2)objComp.ObTransform.position,
                (Vector2)targetObjComp.ObTransform.position
            );
            if (distanceToTarget <= attacker.AttackRange)
            {
                attackerEntity.Del<Movable>();
                attackerEntity.Del<Navigated>();
                attackerEntity.Get<InBattleMarker>();
            }
        }
    }
}
