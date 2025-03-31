using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFrameModel
{
    public event Action<string, Vector3, int> OnActivateAnimation;

    public void ActivateAnimation(string id, Vector3 target, int cycles)
    {
        OnActivateAnimation?.Invoke(id, target, cycles);
    }
}
