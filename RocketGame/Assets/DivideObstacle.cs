using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DivideObstacle : Obstacle
{
    [SerializeField] private int divideValue;
    [SerializeField] private Transform transformSprite;
    [SerializeField] private Transform leftKnock;
    [SerializeField] private Transform rightKnock;
    [SerializeField] private float knockbackDistance = 0.2f;
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private string idEffect;

    public override void AddScoreMultiply()
    {
        _scoreMultiply = new DivideScoreMultiply(divideValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rocket rocket))
        {
            Debug.Log("BONK!");
            Debug.Log($"Obstacle - {_pathRouteData.CourseRoute} // Rocket - {rocket.CourseRoute}");

            if (rocket.CourseRoute == _pathRouteData.CourseRoute)
            {
                ApplyScoreMultiply();
                ApplyObstacleEffect(idEffect, transform);
                ApplyRocketMove(this);

                colliderObstacle.enabled = false;
            }
        }
    }

    public override void KnockLeft()
    {
        transformSprite.DOMove(leftKnock.position, knockbackDuration).SetEase(Ease.OutQuad);
    }

    public override void KnockRight()
    {
        transformSprite.DOMove(rightKnock.position, knockbackDuration).SetEase(Ease.OutQuad);
    }
}
