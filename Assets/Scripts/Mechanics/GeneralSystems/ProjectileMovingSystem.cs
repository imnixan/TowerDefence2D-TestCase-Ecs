using Leopotam.Ecs;
using UnityEngine;

sealed class ProjectileMovingSystem : IEcsRunSystem
{
    EcsFilter<Projectile> projectiles;

    public void Run()
    {
        foreach (int i in projectiles)
        {
            ref Projectile projectile = ref projectiles.Get1(i);

            projectile.ObjTransform.position = Vector2.MoveTowards(
                projectile.ObjTransform.position,
                projectile.Destination,
                projectile.Speed
            );
        }
    }
}
