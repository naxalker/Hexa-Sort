using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public HexStack HexStack { get; private set; }

    [Header("Elements")]
    [SerializeField] private Renderer _renderer;

    public Color Color
    {
        get => _renderer.material.color;
        set => _renderer.material.color = value;
    }

    public void Configure(HexStack hexStack)
    {
        HexStack = hexStack;
    }
}
