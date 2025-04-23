using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPresenter : ISoundProvider
{
    private readonly SoundModel _model;
    private readonly SoundView _view;

    public SoundPresenter(SoundModel model, SoundView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnClickSoundButton += _model.MuteUnmute;
    }

    private void DeactivateEvents()
    {
        _view.OnClickSoundButton -= _model.MuteUnmute;
    }

    #region Input

    public void Play(string id)
    {
        _model.Play(id);
    }

    public void PlayOneShot(string id)
    {
        _model.PlayOneShot(id);
    }

    public ISound GetSound(string id)
    {
        return _model.GetSound(id);
    }

    #endregion
}

public interface ISoundProvider
{
    void Play(string id);
    void PlayOneShot(string id);

    ISound GetSound(string id);
}
