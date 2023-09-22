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
            ref Projectile projectile = ref projectiles.Get1(i);
            ref ObjectComponent objComp = ref projectiles.Get2(i);
            ref EcsEntity attackerEntity = ref projectile.Attacker;

            if (attackerEntity.Has<HasTarget>())
            {
                ref Attacker attacker = ref attackerEntity.Get<Attacker>();
                ref EcsEntity target = ref attackerEntity.Get<HasTarget>().Target;
                ref ObjectComponent targetObj = ref target.Get<ObjectComponent>();
                if (
                    Vector2.Distance(objComp.ObTransform.position, targetObj.ObTransform.position)
                    <= attacker.AttackRange
                )
                {
                    ref DamageRecieveMarker damageMarker = ref target.Get<DamageRecieveMarker>();
                    damageMarker.Damage += projectile.Damage;
                }
            }

            DestroyProjectile(projectiles.GetEntity(i));
        }
    }

    private void DestroyProjectile(EcsEntity projectile)
    {
        ref ObjectComponent proj = ref projectile.Get<ObjectComponent>();

        objectsPool.ReturnProjObjectInPool(proj.ObGo);

        projectile.Destroy();
    }
}
