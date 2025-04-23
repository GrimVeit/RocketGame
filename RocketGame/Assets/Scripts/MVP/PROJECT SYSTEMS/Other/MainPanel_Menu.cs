using System;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Menu : Panel_Move
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonLeaderboard;

    public override void Initialize()
    {
        base.Initialize();

        buttonPlay.onClick.AddListener(HandleClickToPlay);
        buttonLeaderboard.onClick.AddListener(HandleClickToLeaderboard);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonPlay.onClick.RemoveListener(HandleClickToPlay);
        buttonLeaderboard.onClick.RemoveListener(HandleClickToLeaderboard);
    }

    #region Input

    public event Action OnClickToPlay;
    public event Action OnClickToLeaderboard;

    private void HandleClickToPlay()
    {
        OnClickToPlay?.Invoke();
    }

    private void HandleClickToLeaderboard()
    {
        OnClickToLeaderboard?.Invoke();
    }

    #endregion
}
