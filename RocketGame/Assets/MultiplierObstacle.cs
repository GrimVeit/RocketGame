using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierObstacle : Obstacle
{
    [SerializeField] private int multiplierValue;

    public override void ReleaseScoreMultiply()
    {
        _scoreMultiply = new MultiplyScoreMultiply(multiplierValue);
    }
}
