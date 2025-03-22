using System;
using UnityEngine;

public class UIMiniGameSceneRoot : UIRoot
{
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private FooterPanel_Game footerPanel;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        footerPanel.Initialize();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        footerPanel.Dispose();
    }

    public void Activate()
    {
        mainPanel.OnClickToExit += HandleClickToExit;
    }

    public void Deactivate()
    {
        mainPanel.OnClickToExit -= HandleClickToExit;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    #region Input

    public event Action OnClickToExit;

    private void HandleClickToExit()
    {
        OnClickToExit?.Invoke();
    }

    #endregion
}
