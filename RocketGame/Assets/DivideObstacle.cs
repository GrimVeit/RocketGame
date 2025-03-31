using UnityEngine;

public class DivideObstacle : Obstacle
{
    [SerializeField] private int divideValue;

    public override void ReleaseScoreMultiply()
    {
        _scoreMultiply = new DivideScoreMultiply(divideValue);
    }
}
