using Leopotam.Ecs;
using UnityEngine;

sealed class ProjectileMovingSystem : IEcsRunSystem
{
    EcsFilter<Projectile, ObjectComponent> projectiles;

    public void Run()
    {
        foreach (int i in projectiles)
        {
            ref Projectile projectile = ref projectiles.Get1(i);
            ref ObjectComponent objComp = ref projectiles.Get2(i);
            objComp.ObTransform.position = Vector2.MoveTowards(
                objComp.ObTransform.position,
                projectile.Destination,
                projectile.Speed
            );
        }
    }
}
