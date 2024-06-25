using UnityEngine;

public class StackController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask _hexagonLayerMask;
    [SerializeField] private LayerMask _gridHexagonLayerMask;
    [SerializeField] private LayerMask _groundLayerMask;

    private HexStack _currentStack;
    private Vector3 _currentStackInitialPos;

    private void Update()
    {
        ManageControl();
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ManageMouseDown();
        }
        else if (Input.GetMouseButton(0) && _currentStack != null)
        {
            ManageMouseDrag();
        }
        else if (Input.GetMouseButtonUp(0) && _currentStack != null)
        {
            ManageMouseUp();
        }
    }

    private void ManageMouseDown()
    {
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit, 500f, _hexagonLayerMask);

        if (hit.collider == null)
        {
            Debug.Log("We have not detected any hexagons");
            return;
        }

        _currentStack = hit.collider.GetComponent<Hexagon>().HexStack;
        _currentStackInitialPos = _currentStack.transform.position;
    }

    private void ManageMouseDrag()
    {
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit, 500f, _gridHexagonLayerMask);

        if (hit.collider == null)
        {
            DraggingAboveGround();
        }
        else
        {
            DraggingAboveGridCell(hit);
        }
    }

    private void DraggingAboveGround()
    {
        RaycastHit hit;
        Physics.Raycast(GetClickedRay(), out hit, 500f, _groundLayerMask);

        if (hit.collider == null)
        {
            Debug.LogError("No ground detected!");
            return;
        }

        Vector3 currentStackTargetPos = hit.point.With(y: 2);

        _currentStack.transform.position = Vector3.MoveTowards(
            _currentStack.transform.position, 
            currentStackTargetPos, 
            Time.deltaTime * 30f);
    }

    private void DraggingAboveGridCell(RaycastHit hit)
    {
        GridCell gridCell = hit.collider.GetComponent<GridCell>();

        if (gridCell.IsOccupied)
        {
            DraggingAboveGround();
        }
        else
        {
            DraggingAboveNonOccupiedGridCell(gridCell);
        }
    }

    private void DraggingAboveNonOccupiedGridCell(GridCell gridCell)
    {
        Vector3 currentStackTargetPos = gridCell.transform.position.With(y: 2);

        _currentStack.transform.position = Vector3.MoveTowards(
            _currentStack.transform.position,
            currentStackTargetPos,
            Time.deltaTime * 30f);
    }

    private void ManageMouseUp()
    {
        
    }

    private Ray GetClickedRay() => Camera.main.ScreenPointToRay(Input.mousePosition);
}
