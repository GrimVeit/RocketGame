using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierObstacle : Obstacle
{
    [SerializeField] private int multiplierValue;

    public override void AddObstacleEffect()
    {

    }

    public override void AddScoreMultiply()
    {
        _scoreMultiply = new MultiplyScoreMultiply(multiplierValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rocket rocket))
        {
            Debug.Log("BONK!");
            Debug.Log($"Obstacle - {_spawnPointData.CourseRoute} // Rocket - {rocket.CourseRoute}");

            if (rocket.CourseRoute == _spawnPointData.CourseRoute)
            {
                ApplyScoreMultiply();
            }
        }
    }
}
