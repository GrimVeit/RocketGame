using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    private readonly RocketMovePresenter _rocketMovePresenter;
    private readonly PlatformPresenter _platformPresenter;
    private readonly UIGameRoot _sceneRoot;
    private readonly ObstaclePresenter _obstaclePresenter;
    private readonly AltitudeRocketPresenter _altitudePresenter;
    private readonly CourseDisplacementRocketPresenter _courseDisplacementPresenter;
    private readonly ScoreMultiplierPresenter _scoreMultiplierPresenter;
    private readonly ObstacleEffectPresenter _obstacleEffectPresenter;
    private readonly ObstacleRocketMovePresenter _obstacleRocketMovePresenter;

    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundGameStart;

    public ArrivalState_Game(
        IGlobalStateMachineProvider stateProvider, 
        RocketMovePresenter rocketMovePresenter, 
        PlatformPresenter platformPresenter, 
        UIGameRoot sceneRoot, 
        ObstaclePresenter obstaclePresenter, 
        AltitudeRocketPresenter altitudePresenter, 
        CourseDisplacementRocketPresenter courseDisplacementPresenter, 
        ScoreMultiplierPresenter scoreMultiplierPresenter, 
        ObstacleEffectPresenter obstacleEffectPresenter,
        ObstacleRocketMovePresenter obstacleRocketMovePresenter,
        ISoundProvider soundProvider)
    {
        _stateProvider = stateProvider;
        _rocketMovePresenter = rocketMovePresenter;
        _platformPresenter = platformPresenter;
        _sceneRoot = sceneRoot;
        _obstaclePresenter = obstaclePresenter;
        _altitudePresenter = altitudePresenter;
        _courseDisplacementPresenter = courseDisplacementPresenter;
        _scoreMultiplierPresenter = scoreMultiplierPresenter;
        _obstacleEffectPresenter = obstacleEffectPresenter;
        _obstacleRocketMovePresenter = obstacleRocketMovePresenter;
        _soundProvider = soundProvider;
        _soundGameStart = _soundProvider.GetSound("Background_GameStart");
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - ARRIVAL(1)");

        _rocketMovePresenter.OnPauseMoveToBase += _platformPresenter.ActivatePlatform;
        _rocketMovePresenter.OnEndMoveToBase += ChangeStateToPrepare;

        _rocketMovePresenter.MoveToBase();
        _obstaclePresenter.ClearObstacles();
        _altitudePresenter.Clear();
        _courseDisplacementPresenter.Clear();
        _scoreMultiplierPresenter.Clear();
        _obstacleEffectPresenter.Clear();
        _obstacleRocketMovePresenter.Clear();

        _soundGameStart.Play();
        _soundGameStart.SetVolume(0, 1, 0.1f);
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - ARRIVAL(1)");

        _rocketMovePresenter.OnPauseMoveToBase -= _platformPresenter.ActivatePlatform;
        _rocketMovePresenter.OnEndMoveToBase -= ChangeStateToPrepare;
    }

    private void ChangeStateToPrepare()
    {
        _stateProvider.SetState(_stateProvider.GetState<PrepareState_Game>());
    }
}
