using UnityEngine;

public class GridCell : MonoBehaviour
{
    private HexStack _stack;

    public HexStack Stack => _stack;

    public bool IsOccupied 
    { 
        get => _stack != null;
        private set { }
    }

    public void AssignStack(HexStack stack)
    {
        _stack = stack;
    }
}
