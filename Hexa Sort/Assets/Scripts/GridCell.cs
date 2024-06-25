using UnityEngine;

public class GridCell : MonoBehaviour
{
    private HexStack stack;

    public bool IsOccupied 
    { 
        get => stack != null;
        private set { }
    }
}
