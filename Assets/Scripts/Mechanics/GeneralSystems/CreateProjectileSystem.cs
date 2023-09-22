using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

public class CreateProjectileSystem : IEcsRunSystem
{
    private EcsWorld world;
    private ObjectsPool objectsPool;
    private EcsFilter<
        Attacker,
        RangeAttackUnit,
        HasTarget,
        CreateProjectileMarker
    > rangeAttackFilter;

    public void Run()
    {
        foreach (int i in rangeAttackFilter)
        {
            ref Attacker attacker = ref rangeAttackFilter.Get1(i);
            ref RangeAttackUnit rangeAttacker = ref rangeAttackFilter.Get2(i);
            ref HasTarget hasTarget = ref rangeAttackFilter.Get3(i);

            EcsEntity projectileEntity = world.NewEntity();
            ref Projectile projectile = ref projectileEntity.Get<Projectile>();
            projectile.ObjGo = objectsPool.GetProjObj();
            projectile.ObjTransform = projectile.ObjGo.transform;
            projectile.Sr = projectile.ObjGo.GetComponent<SpriteRenderer>();
            projectile.Sr.sprite = rangeAttacker.projectileSprite;
            projectile.Destination = hasTarget.target.Get<ObjectComponent>().ObTransform.position;
            projectile.Speed = rangeAttacker.ProjecttileSpeed;
            projectile.target = hasTarget.target;
        }
    }
}
