using System;
using System.Collections;
using UnityEngine;

public class EggCatcherModel
{
    public event Action OnEggDown;
    public event Action OnEggWin;
    public event Action<Vector3> OnEggDown_Position;
    public event Action<Vector3> OnEggDown_EggValue;

    public event Action OnSpawnEgg;

    public event Action<float> OnChangeMoveTime;

    private float initialDelay = 2f;
    private float minDelay = 0.4f;
    private float decreaseAmount = 0.02f;
    private float currentDelay;

    private IEnumerator spawnEggs_ienumerator;
    private bool isPaused = false;

    private ISoundProvider soundProvider;
    private IParticleEffectProvider particleEffectProvider;

    private bool isActive;

    public EggCatcherModel(float initialDelay, float minDelay, float decreaseAmount, ISoundProvider soundProvider, IParticleEffectProvider particleEffectProvider)
    {
        this.initialDelay = initialDelay;
        this.minDelay = minDelay;
        this.decreaseAmount = decreaseAmount;

        this.soundProvider = soundProvider;
        this.particleEffectProvider = particleEffectProvider;
    }

    public void SetTimerSpawnerData(float initialDelay, float minDelay, float decreaseAmount, float moveTime)
    {
        this.initialDelay = initialDelay;
        this.minDelay= minDelay;
        this.decreaseAmount = decreaseAmount;


        OnChangeMoveTime?.Invoke(moveTime);
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        DeactivateSpawner();
    }

    public void EggWin(EggValues eggValues)
    {
        if (!isActive) return;

        soundProvider.PlayOneShot("EggWin");

        OnEggWin?.Invoke();
    }

    public void EggDown(EggValues eggValues, Vector3 posDown)
    {
        if (!isActive) return;

        //soundProvider.PlayOneShot("FallEgg");

        OnEggDown?.Invoke();
        OnEggDown_Position?.Invoke(posDown);
    }

    public void EggJump()
    {
        //soundProvider.PlayOneShot("Jump");
    }

    #region Spawner

    public void ActivateSpawner()
    {
        isActive = true;

        if (spawnEggs_ienumerator != null)
            Coroutines.Stop(spawnEggs_ienumerator);

        spawnEggs_ienumerator = SpawnEggs_Coroutine();
        Coroutines.Start(spawnEggs_ienumerator);

        Debug.Log("����� ��������");
    }

    public void DeactivateSpawner()
    {
        isActive = false;

        if (spawnEggs_ienumerator != null)
            Coroutines.Stop(spawnEggs_ienumerator);

        Debug.Log("����� ��������");
    }

    public void PauseSpawner()
    {
        isPaused = true;

        Debug.Log("����� ��������");
    }

    public void ResumeSpawner()
    {
        if (spawnEggs_ienumerator == null)
            ActivateSpawner();

            isPaused = false;

        Debug.Log("����������� ��������");
    }

    private IEnumerator SpawnEggs_Coroutine()
    {
        currentDelay = initialDelay;

        while (true)
        {
            yield return new WaitUntil(() => !isPaused);

            if(!isPaused)
               OnSpawnEgg?.Invoke();

            currentDelay = Mathf.Max(currentDelay - decreaseAmount, minDelay);
            Debug.Log(currentDelay);

            yield return new WaitForSeconds(currentDelay);

        }
    }

    #endregion
}
