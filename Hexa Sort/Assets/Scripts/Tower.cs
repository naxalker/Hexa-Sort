using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Tower : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator _animator;
    private Renderer _renderer;

    [Header("Settings")]
    [SerializeField] private float _fillIncrement;
    [SerializeField] private float _maxFillPrecent;
    private float _fillPercent;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        UpdateMaterials();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fill();
        }
    }

    private void Fill()
    {
        if (_fillPercent >= 1f)
            return;

        _fillPercent += _fillIncrement;
        UpdateMaterials();

        _animator.Play("Bump");
    }

    private void UpdateMaterials()
    {
        foreach(Material material in _renderer.materials)
        {
            material.SetFloat("_Fill_Percent", _fillPercent * _maxFillPrecent);
        }
    }
}
