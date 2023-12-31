﻿using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

public class CreateProjectileSystem : IEcsRunSystem
{
    private EcsWorld world;
    private StaticData staticData;
    private Pool pool;
    private EcsFilter<
        Attacker,
        RangeAttackUnit,
        HasTarget,
        CreateProjectileMarker,
        ObjectComponent
    > rangeAttackFilter;

    public void Run()
    {
        foreach (int i in rangeAttackFilter)
        {
            ref Attacker attacker = ref rangeAttackFilter.Get1(i);
            ref RangeAttackUnit rangeAttacker = ref rangeAttackFilter.Get2(i);
            ref HasTarget attackerTarget = ref rangeAttackFilter.Get3(i);
            ref ObjectComponent AttackerObjComp = ref rangeAttackFilter.Get5(i);
            EcsEntity projectileEntity = world.NewEntity();
            ref Projectile projectile = ref projectileEntity.Get<Projectile>();
            projectileEntity.AddObjectComp(
                staticData,
                pool.GetProjObj(),
                StaticData.UnitType.Projectile,
                AttackerObjComp.ObTransform.position
            );

            ref ObjectComponent ProjObjComp = ref projectileEntity.Get<ObjectComponent>();

            projectileEntity.AddMovable(staticData);

            ref Movable movable = ref projectileEntity.Get<Movable>();
            ref EcsEntity targtEntity = ref attackerTarget.Target;
            ref ObjectComponent targetObjComp = ref targtEntity.Get<ObjectComponent>();
            movable.Destination = targetObjComp.ObTransform.position;

            projectile.Speed = rangeAttacker.ProjecttileSpeed;
            projectile.Damage = attacker.Damage;

            ref HasTarget ptTarget = ref projectileEntity.Get<HasTarget>();
            ptTarget.Target = attackerTarget.Target;
        }
    }
}
