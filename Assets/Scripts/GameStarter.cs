using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    private StaticData staticData;

    [SerializeField]
    private GameMapGenerator gameMapGenerator;

    [SerializeField]
    private EcsStartup ecsStartup;

    [SerializeField]
    private PoolSystem poolSystem;

    private void Start()
    {
        gameMapGenerator.GenerateMap(staticData);
        ecsStartup.StartGame(staticData, poolSystem);
    }
}
