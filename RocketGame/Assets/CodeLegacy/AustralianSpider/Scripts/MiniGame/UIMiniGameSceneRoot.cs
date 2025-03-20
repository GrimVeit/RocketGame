using System;
using UnityEngine;

public class UIMiniGameSceneRoot : MonoBehaviour
{

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    private Panel currentPanel;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Base

    public void Activate()
    {

    }

    public void Deactivate()
    {
        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }


    private void OpenPanel(Panel panel)
    {
        if (currentPanel != null)
            currentPanel.DeactivatePanel();

        currentPanel = panel;
        currentPanel.ActivatePanel();

    }

    private void OpenOtherPanel(Panel panel)
    {
        panel.ActivatePanel();
    }

    private void CloseOtherPanel(Panel panel)
    {
        panel.DeactivatePanel();
    }

    #endregion

    #region Input


    #endregion
}
