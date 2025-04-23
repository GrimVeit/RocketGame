using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundView : View
{
    [SerializeField] private Button soundButton;

    public void Initialize()
    {
        if (soundButton == null) return;
        soundButton.onClick.AddListener(HandlerClickToSoundButton);
    }

    public void Dispose()
    {
        if (soundButton == null) return;
        soundButton.onClick.RemoveListener(HandlerClickToSoundButton);
    }

    public void MuteDisplay()
    {

    }

    public void UnmuteDisplay()
    {

    }

    private void HandlerClickToSoundButton()
    {
        OnClickSoundButton?.Invoke();
    }

    #region Output

    public event Action OnClickSoundButton;

    #endregion
}
