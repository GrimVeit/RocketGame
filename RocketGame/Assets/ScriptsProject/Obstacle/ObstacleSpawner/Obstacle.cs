using System;
using DG.Tweening;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour, IScoreMultiplyProvider
{
    [SerializeField] private protected Collider2D colliderObstacle;
    [SerializeField] private protected Transform transformObstacle;

    private protected VisualSpawnPointData _spawnPointData;
    private protected Tween _tweenMove;
    private protected IScoreMultiply _scoreMultiply;

    public abstract void ReleaseScoreMultiply();

    public void SetData(VisualSpawnPointData data)
    {
        _spawnPointData = data;
    }

    public void MoveToEnd()
    {
        _tweenMove = transformObstacle.DOMove(_spawnPointData.EndPoint.position, 4f).SetEase(Ease.Linear).OnComplete(() => OnEndMove?.Invoke(this));
    }

    public void MoveToClear(Vector3 target, Action OnComplete = null)
    {
        _tweenMove?.Kill();

        _tweenMove = transformObstacle.DOMove(target, 1f).OnComplete(() => OnComplete?.Invoke());
    }

    public void Stop()
    {
        _tweenMove?.Kill();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Rocket rocket))
        {
            Debug.Log("BONK!");
            Debug.Log($"Obstacle - {_spawnPointData.CourseRoute} // Rocket - {rocket.CourseRoute}");

            if (rocket.CourseRoute == _spawnPointData.CourseRoute)
            {
                OnApplyScoreMultiply?.Invoke(_scoreMultiply);
                colliderObstacle.enabled = false;
            }
        }
    }

    #region Input

    public event Action<Obstacle> OnEndMove;
    public event Action<IScoreMultiply> OnApplyScoreMultiply;

    #endregion
}
