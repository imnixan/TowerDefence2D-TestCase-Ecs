using System.Collections;
using UnityEngine;
using Leopotam.Ecs;
using TMPro;

sealed class UISystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsFilter<GuiComp> guiFilter;
    private EcsFilter<Enemy, DeadMarker> deadEnemies;
    private StaticData staticData;
    private EcsWorld world;

    public void Init()
    {
        EcsEntity uiEntity = world.NewEntity();
        ref GuiComp guiComp = ref uiEntity.Get<GuiComp>();
        guiComp.KillCounter = staticData.KillCounter;
    }

    public void Run()
    {
        foreach (int i in guiFilter)
        {
            ref GuiComp guiComp = ref guiFilter.Get1(i);
            guiComp.EnemiesKilled += deadEnemies.GetEntitiesCount();
            guiComp.KillCounter.text = guiComp.EnemiesKilled.ToString();
        }
    }
}
