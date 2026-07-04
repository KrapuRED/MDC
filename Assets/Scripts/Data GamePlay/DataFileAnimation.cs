using UnityEngine;
using DG.Tweening;
using System;

public class DataFileAnimation : MonoBehaviour
{
    [Header("Animation Configuration")]
    [SerializeField] private float fadeCrashDuration;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private Tween _fadeTween;

    private void OnDisable()
    {
        _fadeTween?.Kill(); // avoid tween callbacks firing on a disabled/destroyed object
    }

    public void PlayCrashDataFileAnimation(float duration, Action onComplete = null)
    {
        _fadeTween?.Kill(); // stop any fade already in progress before starting a new one

        Color startColor = spriteRenderer.color;
        _fadeTween = spriteRenderer
            .DOFade(0f, duration)
            .SetEase(Ease.InQuad)
            .OnComplete(() => onComplete?.Invoke());
    }
}
