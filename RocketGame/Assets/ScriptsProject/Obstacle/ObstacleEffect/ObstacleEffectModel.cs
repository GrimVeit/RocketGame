using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEffectModel
{
    private readonly IAnimationFrameProvider _animationFrameProvider;

    public ObstacleEffectModel(IAnimationFrameProvider animationFrameProvider)
    {
        _animationFrameProvider = animationFrameProvider;
    }
}
