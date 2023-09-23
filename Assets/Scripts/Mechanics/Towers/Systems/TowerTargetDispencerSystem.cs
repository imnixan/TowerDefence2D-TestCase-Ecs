using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

public class TowerTargetDispencerSystem : IEcsRunSystem
{
    private EcsFilter<Attacker, ObjectComponent, Tower, RangeAttackUnit>.Exclude<
        HasTarget,
        InBattleMarker
    > towerFilter;
    private Collider2D enemy;
    private EcsFilter<Enemy, Health, ObjectComponent> enemyFilter;

    public void Run()
    {
        foreach (int t in towerFilter)
        {
            ref Attacker attacker = ref towerFilter.Get1(t);
            ref ObjectComponent objComp = ref towerFilter.Get2(t);
            enemy = Physics2D.OverlapCircle(objComp.ObTransform.position, attacker.AttackRange);
            if (enemy)
            {
                foreach (int e in enemyFilter)
                {
                    ref ObjectComponent enemyObj = ref enemyFilter.Get3(e);
                    if (enemy.gameObject == enemyObj.ObGo)
                    {
                        ref EcsEntity towerEntiy = ref towerFilter.GetEntity(t);
                        ref EcsEntity enemyEntity = ref enemyFilter.GetEntity(e);
                        ref HasTarget towerTarget = ref towerEntiy.Get<HasTarget>();
                        towerTarget.Target = enemyEntity;
                        towerEntiy.Get<InBattleMarker>();
                    }
                }
            }
        }
    }
}
