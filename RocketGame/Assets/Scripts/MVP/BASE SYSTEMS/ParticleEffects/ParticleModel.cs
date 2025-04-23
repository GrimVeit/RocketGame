using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticleModel
{
    public event Action OnActivateEffect;
    
    public Dictionary<string, ParticleVisual> particleEffects = new Dictionary<string, ParticleVisual>();

    public void Initialize(ParticleVisual[] effects)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            particleEffects.Add(effects[i].ID, effects[i]);
            effects[i].Initialize();
        }
    }

    public void Dispose()
    {
        foreach (var item in particleEffects.Values)
        {
            item.Dispose();
        }
    }

    public IParticle GetParticleEffectById(string id)
    {
        if (particleEffects.ContainsKey(id))
        {
            return particleEffects[id];
        }

        Debug.Log("Ёффект с идентификатором " + id + "не был найден");
        return null;
    }

    public void Play(string ID)
    {
        if (particleEffects.ContainsKey(ID))
        {
            OnActivateEffect?.Invoke();
            particleEffects[ID].Play();
        }
    }
}
