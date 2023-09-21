using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EcsStartup))]
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
        Application.targetFrameRate = 300;
        ecsStartup = GetComponent<EcsStartup>();
        gameMapGenerator.GenerateMap(staticData);
        ecsStartup.StartGame(staticData, poolSystem);
    }
}
