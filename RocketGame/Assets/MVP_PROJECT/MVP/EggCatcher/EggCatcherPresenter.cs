using System;
using UnityEngine;

public class EggCatcherPresenter
{
    private EggCatcherModel eggCatcherModel;
    private EggCatcherView eggCatcherView;

    public EggCatcherPresenter(EggCatcherModel eggCatcherModel, EggCatcherView eggCatcherView)
    {
        this.eggCatcherModel = eggCatcherModel;
        this.eggCatcherView = eggCatcherView;
    }

    public void Initialize()
    {
        ActivateActions();

        eggCatcherView.Initialize();
        eggCatcherModel.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        eggCatcherView.Dispose();
        eggCatcherModel.Dispose();
    }

    private void ActivateActions()
    {
        eggCatcherView.OnEggWin += eggCatcherModel.EggWin;
        eggCatcherView.OnEggDown += eggCatcherModel.EggDown;
        eggCatcherView.OnEggJump += eggCatcherModel.EggJump;

        eggCatcherModel.OnSpawnEgg += eggCatcherView.Spawn;
        eggCatcherModel.OnChangeMoveTime += eggCatcherView.SetMoveTime;
    }

    private void DeactivateEvents()
    {
        eggCatcherView.OnEggWin -= eggCatcherModel.EggWin;
        eggCatcherView.OnEggDown -= eggCatcherModel.EggDown;
        eggCatcherView.OnEggJump -= eggCatcherModel.EggJump;

        eggCatcherModel.OnSpawnEgg -= eggCatcherView.Spawn;
        eggCatcherModel.OnChangeMoveTime -= eggCatcherView.SetMoveTime;
    }

    #region Input

    public event Action OnEggDown
    {
        add { eggCatcherModel.OnEggDown += value; }
        remove { eggCatcherModel.OnEggDown -= value; }
    }

    public event Action OnEggWin
    {
        add { eggCatcherModel.OnEggWin += value; }
        remove { eggCatcherModel.OnEggWin -= value; }
    }

    public event Action<Vector3> OnEggDown_Position
    {
        add { eggCatcherModel.OnEggDown_Position += value; }
        remove { eggCatcherModel.OnEggDown_Position -= value; }
    }

    public void SetTimerSpawnerData(float initialDelay, float minDelay, float decreaseAmount, float moveTime)
    {
        eggCatcherModel.SetTimerSpawnerData(initialDelay , minDelay , decreaseAmount, moveTime);
    }

    public void StartSpawner()
    {
        eggCatcherModel.ActivateSpawner();
    }

    public void DeactivateSpawner()
    {
        eggCatcherModel.DeactivateSpawner();
    }

    public void PauseSpawner()
    {
        eggCatcherModel.PauseSpawner();
    }

    public void ResumeSpawner()
    {
        eggCatcherModel.ResumeSpawner();
    }

    #endregion
}
