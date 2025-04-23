using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePresenter : IParticleProvider
{
    private readonly ParticleModel effectModel;
    private readonly ParticleView effectView;

    public ParticlePresenter(ParticleModel effectModel, ParticleView effectView)
    {
        this.effectModel = effectModel;
        this.effectView = effectView;
    }

    public void Initialize()
    {
        effectModel.Initialize(effectView.particleEffects.particleVisuals.ToArray());
    }

    public void Dispose()
    {
        effectModel.Dispose();
    }


    #region Input

    public void Play(string ID)
    {
        effectModel.Play(ID);
    }

    public IParticle GetParticleEffect(string ID)
    {
        return effectModel.GetParticleEffectById(ID);
    }

    #endregion
}

public interface IParticleProvider
{
    void Play(string ID);
    IParticle GetParticleEffect(string ID);
}
