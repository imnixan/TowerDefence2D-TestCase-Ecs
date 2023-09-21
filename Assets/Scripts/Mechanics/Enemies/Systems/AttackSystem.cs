using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

public class AttackSystem : IEcsRunSystem
{
    private EcsFilter<Attacking> attackingFilter;

    public void Run() { }
}
