using UnityEngine;
using UnityEngine.UI;

public class SummObstacle : Obstacle
{
    [SerializeField] private int summValue;
    [SerializeField] private Image imageObstacle;
    [SerializeField] private string idEffect;

    public override void AddObstacleEffect()
    {

    }

    public override void AddScoreMultiply()
    {
        _scoreMultiply = new SummScoreMultiply(summValue);
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

                imageObstacle.enabled = false;
                colliderObstacle.enabled = false;
            }
        }
    }
}
