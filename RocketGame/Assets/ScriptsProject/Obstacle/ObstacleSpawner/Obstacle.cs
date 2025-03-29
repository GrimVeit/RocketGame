using System;
using DG.Tweening;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public VisualSpawnPointData SpawnPointData { get; private set; }

    [SerializeField] private Transform transformObstacle;

    private Tween tweenMove;

    public void SetData(VisualSpawnPointData data)
    {
        SpawnPointData = data;
    }

    public void MoveToEnd()
    {
        tweenMove = transformObstacle.DOMove(SpawnPointData.EndPoint.position, 4f).SetEase(Ease.Linear).OnComplete(() => OnEndMove?.Invoke(this));
    }

    public void MoveToClear(Vector3 target, Action OnComplete = null)
    {
        tweenMove?.Kill();

        tweenMove = transformObstacle.DOMove(target, 0.3f).OnComplete(() => OnComplete?.Invoke());
    }

    public void Stop()
    {
        tweenMove?.Kill();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #region Input

    public event Action<Obstacle> OnEndMove;
    #endregion
}
