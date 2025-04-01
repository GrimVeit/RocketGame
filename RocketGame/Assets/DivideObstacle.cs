using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DivideObstacle : Obstacle
{
    [SerializeField] private int divideValue;
    [SerializeField] private Transform transformSprite;
    [SerializeField] private float knockbackDistance = 0.2f;
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private string idEffect;
    public override void AddObstacleEffect()
    {

    }

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
                ApplyRocketMove();

                colliderObstacle.enabled = false;

                Vector2 knockbackDirection = (transform.position - (Vector3)collision.ClosestPoint(transform.position)).normalized;
                transformSprite.DOMove((Vector2)transform.position + knockbackDirection * knockbackDistance, knockbackDuration).SetEase(Ease.OutQuad);
            }
        }
    }
}
