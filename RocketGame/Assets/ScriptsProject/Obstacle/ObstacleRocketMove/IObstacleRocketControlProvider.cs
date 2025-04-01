using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacleRocketControlProvider
{
    public event Action<ObstacleType, PathZone> OnApplyObstacleRocketControl;
}
