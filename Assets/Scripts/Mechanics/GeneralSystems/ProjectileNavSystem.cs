using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

public class ProjectileNavSystem : IEcsRunSystem
{
    private EcsFilter<Projectile, Movable, HasTarget> projectileFilter;

    public void Run()
    {
        foreach (int i in projectileFilter)
        {
            ref Projectile projectile = ref projectileFilter.Get1(i);
            ref Movable movable = ref projectileFilter.Get2(i);
            ref HasTarget ptTarget = ref projectileFilter.Get3(i);
            ref EcsEntity target = ref ptTarget.Target;
            ref ObjectComponent targetObjComp = ref target.Get<ObjectComponent>();
            movable.Destination = targetObjComp.ObTransform.position;
        }
    }
}
