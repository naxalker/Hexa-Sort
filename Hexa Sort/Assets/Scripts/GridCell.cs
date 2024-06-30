using NaughtyAttributes;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Hexagon _hexagonPrefab;

    [Header("Settings")]
    [OnValueChanged("GenerateInitialHexagon")]
    [SerializeField] private Color[] _hexagonColors;

    private HexStack _stack;

    public HexStack Stack => _stack;

    public bool IsOccupied
    {
        get => _stack != null;
        private set { }
    }

    private void Start()
    {
        if (_hexagonColors.Length > 0)
        {
            GenerateInitialHexagon();
        }

        //if (transform.childCount > 1)
        //{
        //    _stack = transform.GetChild(1).GetComponent<HexStack>();
        //    _stack.Initialize();
        //}
    }

    public void AssignStack(HexStack stack)
    {
        _stack = stack;
    }

    private void GenerateInitialHexagon()
    {
        while (transform.childCount > 1)
        {
            Transform t = transform.GetChild(1);
            t.SetParent(null);
            DestroyImmediate(t.gameObject);
        }

        _stack = new GameObject("Initial Stack").AddComponent<HexStack>();
        _stack.transform.SetParent(transform);
        _stack.transform.localPosition = Vector3.up * .2f;

        for (int i = 0; i < _hexagonColors.Length; i++)
        {
            Vector3 spawnPosition = _stack.transform.TransformPoint(Vector3.up * i * .2f);

            Hexagon hexagonInstance = Instantiate(_hexagonPrefab, spawnPosition, Quaternion.identity);
            hexagonInstance.Color = _hexagonColors[i];

            _stack.Add(hexagonInstance);
        }
    }
}
