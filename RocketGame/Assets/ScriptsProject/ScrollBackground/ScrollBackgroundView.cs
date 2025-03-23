using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackgroundView : View
{
    [SerializeField] private RawImage rawImage;

    private Rect rect;

    public void Initialize()
    {
        rect = rawImage.uvRect;
    }

    public void Dispose()
    {

    }

    public void SetScrollValue(float scrollValue)
    {
        rect.y = scrollValue;
        rawImage.uvRect = rect;
    }
}
