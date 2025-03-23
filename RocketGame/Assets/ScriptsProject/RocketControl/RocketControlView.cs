using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RocketControlView : View
{
    #region Test

    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;

    public void Initialize()
    {
        buttonLeft.onClick.AddListener(HandleClickToLeft);
        buttonRight.onClick.AddListener(HandleClickToRight);
    }

    public void Dispose()
    {
        buttonLeft.onClick.RemoveListener(HandleClickToLeft);
        buttonRight.onClick.RemoveListener(HandleClickToRight);
    }

    #region Input

    public event Action OnClickToLeft;
    public event Action OnClickToRight;

    private void HandleClickToLeft()
    {
        OnClickToLeft?.Invoke();
    }

    private void HandleClickToRight()
    {
        OnClickToRight?.Invoke();
    }

    #endregion

    #endregion

    #region Rocket

    [SerializeField] private List<Transform> transforms = new List<Transform>();
    [SerializeField] private Vector3 vectorRotateToLeft;
    [SerializeField] private Vector3 vectorRotateToRight;
    [SerializeField] private Rocket rocket;

    [Header("Shake")]
    [SerializeField] private float durationShake;
    [SerializeField] private float strengthShake;
    [SerializeField] private int vibratoShake;
    [SerializeField] private float randomnessShake;

    public void MoveLeft(int routeNumber)
    {
        rocket.MoveTo(transforms[routeNumber].position + new Vector3(0, UnityEngine.Random.Range(-0.2f, 0.2f)), 0.3f);
        rocket.RotateTo(vectorRotateToLeft, 0.3f);
        rocket.Shake(durationShake, strengthShake, vibratoShake, randomnessShake);
    }

    public void MoveRight(int routeNumber)
    {
        rocket.MoveTo(transforms[routeNumber].position + new Vector3(0, UnityEngine.Random.Range(-0.2f, 0.2f)), 0.3f);
        rocket.RotateTo(vectorRotateToRight, 0.5f);
        rocket.Shake(durationShake, strengthShake, vibratoShake, randomnessShake);
    }

    #endregion
}
