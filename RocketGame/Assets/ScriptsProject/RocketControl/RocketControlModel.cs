using System;
using UnityEngine;

public class RocketControlModel
{
    public event Action<int> OnMoveToLeft;
    public event Action<int> OnMoveToRight;

    private const int minRouteNumber = 0;
    private const int maxRouteNumber = 8;
    private int currentRouteNumber = 4;

    public void MoveLeft()
    {
        if (currentRouteNumber <= minRouteNumber)
        {
            Debug.Log("FAIL: LEFT ROUTE OUT");
            return;
        }
        
        currentRouteNumber -= 1;
        OnMoveToLeft?.Invoke(currentRouteNumber);
    }

    public void MoveRight()
    {

        if (currentRouteNumber >= maxRouteNumber)
        {
            Debug.Log("FAIL: RIGHT ROUTE OUT");
            return;
        }
        
        currentRouteNumber += 1;
        OnMoveToRight?.Invoke(currentRouteNumber);
    }
}
