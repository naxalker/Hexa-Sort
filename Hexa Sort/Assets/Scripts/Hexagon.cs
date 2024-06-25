using System;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public HexStack HexStack { get; private set; }

    [Header("Elements")]
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Collider _collider;

    public Color Color
    {
        get => _renderer.material.color;
        set => _renderer.material.color = value;
    }

    public void Configure(HexStack hexStack)
    {
        HexStack = hexStack;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void DisableCollider() => _collider.enabled = false;

    public void MoveToLocal(Vector3 targetLocalPosition)
    {
        LeanTween.moveLocal(gameObject, targetLocalPosition, .2f)
            .setEase(LeanTweenType.easeInOutSine)
            .setDelay(transform.GetSiblingIndex() * .01f);
    }

    public void Vanish(float delay)
    {
        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, Vector3.zero, .2f)
            .setEase(LeanTweenType.easeInBack)
            .setDelay(delay)
            .setOnComplete(() => Destroy(gameObject));
    }
}
