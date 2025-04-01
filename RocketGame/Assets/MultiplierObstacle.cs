using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierObstacle : Obstacle
{
    [SerializeField] private int multiplierValue;
    [SerializeField] private Image imageObstacle;
    [SerializeField] private string idEffect;
    public override void AddScoreMultiply()
    {
        _scoreMultiply = new MultiplyScoreMultiply(multiplierValue);
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

                imageObstacle.enabled = false;
                colliderObstacle.enabled = false;
            }
        }
    }
}
