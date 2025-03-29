using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawnerView : View
{
    [SerializeField] private Obstacle obstaclePrefab;
    [SerializeField] private Transform transformParent;
    [SerializeField] private List<VisualSpawnPointData> spawnPoints = new List<VisualSpawnPointData>();

    public void SpawnObstacle(int id)
    {
        var data = spawnPoints.FirstOrDefault(d => d.Id == id);

        var obstacle = Instantiate(obstaclePrefab, transformParent);
        obstacle.transform.SetPositionAndRotation(data.StartPoint.position, obstaclePrefab.transform.rotation);
        obstacle.SetData(data);

        OnSpawnObstacle?.Invoke(obstacle);
    }

    #region Output

    public event Action<Obstacle> OnSpawnObstacle;

    #endregion
}

[System.Serializable]
public class VisualSpawnPointData
{
    [SerializeField] private int id;
    [SerializeField] private Transform transformStartPoint;
    [SerializeField] private Transform transformEndPoint;

    public int Id => id;
    public Transform StartPoint => transformStartPoint;
    public Transform EndPoint => transformEndPoint;
}
