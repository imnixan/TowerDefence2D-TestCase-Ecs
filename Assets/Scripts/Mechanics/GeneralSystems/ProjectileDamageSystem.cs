using Leopotam.Ecs;
using UnityEngine;

public class ProjectileDamageSystem : IEcsRunSystem
{
    private EcsFilter<Projectile, ObjectComponent>.Exclude<Movable> projectiles;
    private Pool objectsPool;

    public void Run()
    {
        foreach (int i in projectiles)
        {
            ref EcsEntity ptEntity = ref projectiles.GetEntity(i);
            ref Projectile projectile = ref projectiles.Get1(i);
            ref ObjectComponent objComp = ref projectiles.Get2(i);

            if (ptEntity.Has<HasTarget>())
            {
                ref HasTarget ptTarget = ref ptEntity.Get<HasTarget>();
                ref EcsEntity targetEntity = ref ptTarget.Target;

                ref ObjectComponent targetObj = ref targetEntity.Get<ObjectComponent>();
                if (
                    Vector2.Distance(objComp.ObTransform.position, targetObj.ObTransform.position)
                    <= 0.5f
                )
                {
                    ref DamageRecieveMarker damageMarker =
                        ref targetEntity.Get<DamageRecieveMarker>();
                    damageMarker.Damage += projectile.Damage;
                    DestroyProjectile(ptEntity);
                }
            }
            else
            {
                DestroyProjectile(ptEntity);
            }
        }
    }

    private void DestroyProjectile(EcsEntity projectile)
    {
        ref ObjectComponent proj = ref projectile.Get<ObjectComponent>();

        objectsPool.ReturnProjObjectInPool(proj.ObGo);

        projectile.Destroy();
    }
}
