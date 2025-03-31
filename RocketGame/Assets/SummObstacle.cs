using UnityEngine;

public class SummObstacle : Obstacle
{
    [SerializeField] private int summValue;

    public override void ReleaseScoreMultiply()
    {
        _scoreMultiply = new SummScoreMultiply(summValue);
    }
}
