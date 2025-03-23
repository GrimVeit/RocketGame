using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Transform transformSpriteRocket;

    private Tween moveTween;
    private Tween shakeTween;
    private Sequence rotateSequence;

    public void MoveTo(Vector3 target, float time)
    {
        moveTween?.Kill();

        moveTween = transform.DOMove(target, time);
    }

    public void RotateTo(Vector3 vectorRotate, float time)
    {
        rotateSequence?.Kill();

        rotateSequence = DOTween.Sequence();
        rotateSequence.Append(transform.DOLocalRotate(vectorRotate, time / 4));
        rotateSequence.AppendInterval(time/2);
        rotateSequence.Append(transform.DOLocalRotate(Vector3.zero, time / 4));
        rotateSequence.Play();
    }

    public void Shake(float duration, float strength, int vibrato, float randomness)
    {
        shakeTween?.Kill();
        transformSpriteRocket.localRotation = Quaternion.Euler(Vector3.zero);

        shakeTween = transformSpriteRocket.DOShakeRotation(duration, strength, vibrato, randomness);
    }
}
