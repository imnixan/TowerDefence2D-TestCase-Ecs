using System.Collections;
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
            ref HasTarget hasTarget = ref rangeAttackFilter.Get3(i);
            ref ObjectComponent AttackerObjComp = ref rangeAttackFilter.Get5(i);
            EcsEntity projectileEntity = world.NewEntity();
            ref Projectile projectile = ref projectileEntity.Get<Projectile>();
            projectileEntity.AddObjectComp(
                staticData,
                pool.GetProjObj(AttackerObjComp.ObTransform.position),
                StaticData.UnitType.Projectile
            );
            ref ObjectComponent ProjObjComp = ref projectileEntity.Get<ObjectComponent>();

            projectileEntity.AddMovable(staticData);

            ref Movable movable = ref projectileEntity.Get<Movable>();
            movable.Destination = hasTarget.target.Get<ObjectComponent>().ObTransform.position;

            projectile.Speed = rangeAttacker.ProjecttileSpeed;
            projectile.target = hasTarget.target;
            projectile.Damage = attacker.Damage;
        }
    }
}
