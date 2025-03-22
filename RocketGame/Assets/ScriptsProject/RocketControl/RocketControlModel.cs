using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControlModel
{
    public event Action OnMoveToLeft;
    public event Action OnMoveToRight;

    public void MoveLeft()
    {
        OnMoveToLeft?.Invoke();
    }

    public void MoveRight()
    {
        OnMoveToRight?.Invoke();
    }
}
