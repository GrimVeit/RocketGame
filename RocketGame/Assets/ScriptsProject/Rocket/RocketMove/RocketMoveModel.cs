using System;
using UnityEngine;

public class RocketMoveModel
{
    public event Action<int> OnMoveToLeft;
    public event Action<int> OnMoveToRight;
    public event Action OnMoveToWinLeft;
    public event Action OnMoveToWinRight;

    public event Action OnMoveToBase;
    public event Action OnMoveToPlay;

    private const int minRouteNumber = 0;
    private const int maxRouteNumber = 8;
    private int currentRouteNumber = 4;

    public void MoveLeft()
    {
        if (currentRouteNumber <= minRouteNumber)
        {
            Debug.Log("FAIL: LEFT ROUTE OUT");
            OnMoveToWinLeft?.Invoke();
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
            OnMoveToWinRight?.Invoke();
            return;
        }

        currentRouteNumber += 1;
        OnMoveToRight?.Invoke(currentRouteNumber);
    }

    public void MoveToBase()
    {

    }
}
