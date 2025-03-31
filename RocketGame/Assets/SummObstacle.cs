using UnityEngine;

public class SummObstacle : Obstacle
{
    [SerializeField] private int summValue;

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
            Debug.Log($"Obstacle - {_spawnPointData.CourseRoute} // Rocket - {rocket.CourseRoute}");

            if (rocket.CourseRoute == _spawnPointData.CourseRoute)
            {
                ApplyScoreMultiply();
            }
        }
    }
}
