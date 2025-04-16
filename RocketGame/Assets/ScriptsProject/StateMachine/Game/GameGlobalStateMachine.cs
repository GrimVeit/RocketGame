using System;
using System.Collections.Generic;

public class GameGlobalStateMachine : IGlobalStateMachineProvider
{
    private Dictionary<Type, IState> states = new();

    private IState currentState;

    public GameGlobalStateMachine(
        RocketMovePresenter rocketMovePresenter, 
        PlatformPresenter platformPresenter, 
        ScrollBackgroundPresenter scrollBackgroundPresenter,
        UIMiniGameSceneRoot sceneRoot, 
        ObstacleSpawnerPresenter obstacleSpawnerPresenter, 
        ObstaclePresenter obstaclePresenter,
        StoreBetPresenter storeBetPresenter,
        BetPreparePresenter betPreparePresenter,
        AltitudePresenter altitudePresenter,
        CourseDisplacementPresenter courseDisplacementPresenter,
        ScoreMultiplierPresenter scoreMultiplierPresenter,
        ObstacleEffectPresenter obstacleEffectPresenter,
        ObstacleRocketMovePresenter obstacleRocketMovePresenter,
        ScorePresenter scorePresenter,
        ISoundProvider soundProvider,
        IParticleEffectProvider particleEffectProvider)
    {
        states[typeof(ArrivalState_Game)] = new ArrivalState_Game(this, rocketMovePresenter, platformPresenter, sceneRoot, obstaclePresenter, altitudePresenter, courseDisplacementPresenter, scoreMultiplierPresenter, obstacleEffectPresenter, obstacleRocketMovePresenter, soundProvider);
        states[typeof(PrepareState_Game)] = new PrepareState_Game(this, sceneRoot, storeBetPresenter, betPreparePresenter, soundProvider);
        states[typeof(LaunchState_Game)] = new LaunchState_Game(this, rocketMovePresenter, sceneRoot, altitudePresenter, soundProvider);
        states[typeof(MainGameState_Game)] = new MainGameState_Game(this, platformPresenter, rocketMovePresenter, scrollBackgroundPresenter, sceneRoot, obstacleSpawnerPresenter, courseDisplacementPresenter, soundProvider);
        states[typeof(WinState_Game)] = new WinState_Game(this, sceneRoot, rocketMovePresenter, obstaclePresenter, altitudePresenter, scorePresenter, particleEffectProvider, soundProvider);

        states[typeof(HouseBedroomState_Game)] = new HouseBedroomState_Game(this, sceneRoot, soundProvider);
        states[typeof(HouseBioreactorState_Game)] = new HouseBioreactorState_Game(this, sceneRoot);
    }

    public void Initialize()
    {
        SetState(GetState<ArrivalState_Game>());
    }

    public void Dispose()
    {

    }

    public void SetState(IState state)
    {
        currentState?.ExitState();

        currentState = state;
        currentState.EnterState();
    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }
}
