using UnityEngine;

public class DivideObstacle : Obstacle
{
    [SerializeField] private int divideValue;

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
            Debug.Log($"Obstacle - {_spawnPointData.CourseRoute} // Rocket - {rocket.CourseRoute}");

            if (rocket.CourseRoute == _spawnPointData.CourseRoute)
            {
                ApplyScoreMultiply();
            }
        }
    }
}
