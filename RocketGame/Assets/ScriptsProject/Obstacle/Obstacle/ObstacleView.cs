using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleView : View
{
    [SerializeField] private List<Transform> transformClears = new List<Transform>();

    [SerializeField] private List<Obstacle> obstacles = new List<Obstacle>();

    public void ClearObstacles()
    {
        obstacles.ForEach(o =>
        {
            o.OnEndMove -= RemoveObstacle;

            o.MoveToClear(transformClears[Random.Range(0, transformClears.Count)].position, o.Destroy);
        });

        obstacles.Clear();
    }

    public void StopObstacles()
    {
        obstacles.ForEach(o =>
        {
            o.Stop();
        });
    }

    public void AddObstacle(Obstacle obstacle)
    {
        obstacles.Add(obstacle);

        obstacle.OnEndMove += RemoveObstacle;

        obstacle.MoveToEnd();
    }

    private void RemoveObstacle(Obstacle obstacle)
    {
        obstacles.Remove(obstacle);

        obstacle.OnEndMove -= RemoveObstacle;

        obstacle.Destroy();
    }
}
