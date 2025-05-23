using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    private readonly UIGameRoot _sceneRoot;
    private readonly RocketMovePresenter _rocketMovePresenter;
    private readonly ObstaclePresenter _obstaclePresenter;
    private readonly AltitudeRocketPresenter _altitudePresenter;
    private readonly ScorePresenter _scorePresenter;
    private readonly IParticleProvider _particleEffectProvider;
    private readonly ISoundProvider _soundProvider;

    private IEnumerator coroutineTimer;

    public WinState_Game(IGlobalStateMachineProvider stateProvider, UIGameRoot sceneRoot, RocketMovePresenter rocketMovePresenter, ObstaclePresenter obstaclePresenter, AltitudeRocketPresenter altitudePresenter, ScorePresenter scorePresenter, IParticleProvider particleEffectProvider, ISoundProvider soundProvider)
    {
        _stateProvider = stateProvider;
        _sceneRoot = sceneRoot;
        _rocketMovePresenter = rocketMovePresenter;
        _obstaclePresenter = obstaclePresenter;
        _altitudePresenter = altitudePresenter;
        _scorePresenter = scorePresenter;
        _particleEffectProvider = particleEffectProvider;
        _soundProvider = soundProvider;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - WIN(5)");

        _sceneRoot.CloseFooterPanel();
        _sceneRoot.CloseExitPanel();
        _sceneRoot.CloseBetPanel();
        _sceneRoot.OpenWinPanel();

        _rocketMovePresenter.Restart();
        _obstaclePresenter.StopObstacles();
        _altitudePresenter.DeactivateAltitude();
        _scorePresenter.Win();
        _particleEffectProvider.Play("Win");
        _soundProvider.PlayOneShot("WinGame");

        if(coroutineTimer != null ) Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer();
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - WIN(5)");

        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);

        _sceneRoot.CloseWinPanel();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);

        ChangeStateToPreparePlay();
    }

    private void ChangeStateToPreparePlay()
    {
        _stateProvider.SetState(_stateProvider.GetState<ArrivalState_Game>());
    }
}
