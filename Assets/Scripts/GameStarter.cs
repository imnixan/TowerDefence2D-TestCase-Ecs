using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    private StaticData staticData;

    [SerializeField]
    private GameMapGenerator gameMapGenerator;

    private EcsStartup ecsStartup;

    [SerializeField]
    private PoolSystem poolSystem;

    private void Start()
    {
        ecsStartup = gameObject.AddComponent<EcsStartup>();
        gameMapGenerator.GenerateMap(staticData);
        staticData.InitData();
        ecsStartup.StartGame(staticData, poolSystem);
    }
}
