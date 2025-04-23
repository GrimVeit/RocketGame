using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParticleVisual : IParticle
{
    public string ID;

    public List<Particle> particles = new();

    private IEnumerator coroutineParticles;

    public void Initialize()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Dispose();
        }
    }

    public void Play()
    {
        if (coroutineParticles != null)
            Coroutines.Stop(coroutineParticles);

        coroutineParticles = PlayParticles_Coroutine();
        Coroutines.Start(coroutineParticles);
    }

    public void Stop()
    {
        if (coroutineParticles != null)
            Coroutines.Stop(coroutineParticles);

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }


    private IEnumerator PlayParticles_Coroutine()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
            yield return new WaitForSeconds(particles[i].TimeToInterval);
        }
    }
}

public interface IParticle 
{ 
    void Play();
    void Stop();
}


[Serializable]
public class Particle
{
    public float TimeToInterval => timeToInterval;

    private ParticleSystem particleSystem;
    [SerializeField] private float timeToInterval;
    [SerializeField] private ParticleSystem particleSystemPrefab;
    [SerializeField] private Transform particleTransform;

    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private bool isPlayAwake;
    public void Initialize()
    {
        particleSystem = UnityEngine.Object.Instantiate(particleSystemPrefab, particleTransform);
        particleSystem.transform.SetLocalPositionAndRotation(Vector3.zero, particleSystemPrefab.transform.rotation);

        float randomSize = UnityEngine.Random.Range(minSize, maxSize);

        particleSystem.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

        if (isPlayAwake)
            Play();
    }

    public void Dispose()
    {
        //Destroy();
        Stop();
    }

    private void Destroy()
    {
        if (particleSystem != null)
            Coroutines.Destroy(particleSystem.gameObject);
    }

    public void Play()
    {
        if(particleSystem != null)
           particleSystem.Play();
    }

    public void Stop()
    {
        if(particleSystem != null)
           particleSystem.Stop();
    }
}
