using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuiManager : MonoBehaviour
{
    [SerializeField]
    private float AnimLength;

    [SerializeField]
    private RectTransform startButton,
        killCount,
        exitBtn,
        restartBtn;

    [SerializeField]
    private Vector2 startButtonShow,
        startButtonHide,
        killCountShow,
        killCountHide,
        exitShow,
        exitHide,
        restartShow,
        restartHide;

    private Sequence startAnim;
    private Sequence endGameAnim;
    private Sequence gameStart;
    private bool GameGoin;

    private void Start()
    {
        InitAnims();
        startAnim.Restart();
    }

    private void InitAnims()
    {
        startAnim = DOTween.Sequence();
        startAnim
            .Append(startButton.DOAnchorPos(startButtonShow, AnimLength))
            .Join(killCount.DOAnchorPos(killCountShow, AnimLength))
            .Join(exitBtn.DOAnchorPos(exitShow, AnimLength))
            .Join(restartBtn.DOAnchorPos(restartShow, AnimLength));

        endGameAnim = DOTween.Sequence();
        endGameAnim
            .AppendCallback(() =>
            {
                restartBtn.anchorMax = startButton.anchorMax;
                restartBtn.anchorMin = startButton.anchorMin;
                restartBtn.pivot = new Vector2(0.5f, 0.5f);
            })
            .Append(restartBtn.DOAnchorPos(startButtonShow, AnimLength));

        gameStart = DOTween.Sequence();
        gameStart.Append(startButton.DOAnchorPos(startButtonHide, AnimLength));
    }

    public void EndGame()
    {
        if (GameGoin)
        {
            GameGoin = false;
            endGameAnim.Restart();
        }
    }

    public void StartGame()
    {
        if (!GameGoin)
        {
            GameGoin = true;
            gameStart.Restart();
        }
    }
}
