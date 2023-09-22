using Leopotam.Ecs;
using UnityEngine;

public class ProjectileDamageSystem : IEcsRunSystem
{
    private EcsFilter<Projectile> projectiles;
    private ObjectsPool objectsPool;

    public void Run()
    {
        foreach (int i in projectiles)
        {
            ref Projectile projectile = ref projectiles.Get1(i);
            if ((Vector2)projectile.ObjTransform.position == projectile.Destination)
            {
                EcsEntity target = projectile.target;
                ref DamageRecieveMarker damageMarker = ref target.Get<DamageRecieveMarker>();
                damageMarker.Damage += projectile.Damage;
                DestroyProjectile(projectiles.GetEntity(i));
            }
        }
    }

    private void DestroyProjectile(EcsEntity projectile)
    {
        ref Projectile proj = ref projectile.Get<Projectile>();

        objectsPool.ReturnProjObjectInPool(proj.ObjGo);

        projectile.Destroy();
    }
}
