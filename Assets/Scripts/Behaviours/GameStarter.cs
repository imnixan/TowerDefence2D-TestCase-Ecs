using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    private StaticData staticData;

    [SerializeField]
    private GuiManager guiManager;

    [SerializeField]
    private GameMapGenerator gameMapGenerator;

    private EcsStartup ecsStartup;

    [SerializeField]
    private Pool poolSystem;

    private void Start() { }

    public void StartGame()
    {
        Application.targetFrameRate = 300;
        ecsStartup = GetComponentInChildren<EcsStartup>();
        gameMapGenerator.GenerateMap(staticData);
        ecsStartup.StartGame(staticData, poolSystem);
        guiManager.StartGame();
    }

    private void OnGameEnd()
    {
        ecsStartup.gameObject.SetActive(false);
        guiManager.EndGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        EndGameSystem.GameEnd += OnGameEnd;
    }

    private void OnDisable()
    {
        EndGameSystem.GameEnd -= OnGameEnd;
    }
}
