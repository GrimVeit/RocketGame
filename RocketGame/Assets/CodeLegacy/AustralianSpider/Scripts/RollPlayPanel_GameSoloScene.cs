using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPlayPanel_GameSoloScene : MovePanel
{
    public bool IsActivePanel => isActivePanel;

    private bool isActivePanel;

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        isActivePanel = true;
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        isActivePanel = false;
    }
}
