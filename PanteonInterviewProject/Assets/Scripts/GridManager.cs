using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridNode
{
    public float hCost = 0;
    public float gCost = 0;
    public float fCost
    {
        get { return gCost + hCost; }
    }

    public Vector2Int gridIndex;
    public GridNode parent;
    public bool isObstructed;
}

public class GridManager : MonoBehaviour, IPointerClickHandler
{
    void Awake()
    {
        grid = new GridNode[gridExtents.x, gridExtents.y];

        Vector3 gridScale = transform.localScale;

        Vector2 gridWorldSize;
        gridWorldSize.x = totalGridSize.x * gridScale.x;
        gridWorldSize.y = totalGridSize.y * gridScale.y;

        singleGridWorldSize.x = singleGridSize.x * gridScale.x;
        singleGridWorldSize.y = singleGridSize.y * gridScale.y;

        firstGridPosition = new Vector3(transform.position.x - gridWorldSize.x * 0.5f, transform.position.y + gridWorldSize.y * 0.5f, 0);

        // Set every grid not occupied by a placeable.
        for (int y = 0; y < 14; y++)
        {
            for (int x = 0; x < 14; x++)
            {
                grid[x, y] = new GridNode();
                grid[x, y].isObstructed = false;
                grid[x, y].gridIndex = new Vector2Int(x, y);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (selectedSoldier)
            {
                selectedSoldier.GetComponent<SoldierController>().MoveTo(GetCorrespondingRoundedGridIndex(Camera.main.ScreenToWorldPoint(eventData.position), true));
            }
        }
        else
        {
            SetSelectedSoldier(null);
            SetSelectedPlaceable(null);
        }
    }
    
    public List<Vector2Int> GetNeighbouringGrids(GridNode grid)
    {
        Vector2Int sourceGridIndex = grid.gridIndex;
        List<Vector2Int> neighbours = new List<Vector2Int>();

        int startXIndex = sourceGridIndex.x == 0 ? 0 : -1;
        int endXIndex = sourceGridIndex.x == gridExtents.x - 1 ? 0 : 1;
        int startYIndex = sourceGridIndex.y == 0 ? 0 : -1;
        int endYIndex = sourceGridIndex.y == gridExtents.y - 1 ? 0 : 1;

        for (int y = startYIndex; y <= endYIndex; y++)
        {
            for (int x = startXIndex; x <= endXIndex; x++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                neighbours.Add(new Vector2Int(sourceGridIndex.x + x, sourceGridIndex.y + y));
            }
        }

        return neighbours;
    }

    // Called when drop operation is done on the grid
    public Vector2Int PutPlaceableToTheGrid(Placeable placeable)
    {
        Vector2Int correspondingGridIndex = GetCorrespondingRoundedGridIndex(placeable.transform.position);
		placeable.transform.position = GetGridPosition(correspondingGridIndex);

        Vector2Int placeableSize = placeable.GetSize();

        int endXIndex = correspondingGridIndex.x + placeableSize.x;
        int endYIndex = correspondingGridIndex.y + placeableSize.y;

        for (int y = correspondingGridIndex.y; y < endYIndex; y++)
        {
            for (int x = correspondingGridIndex.x; x < endXIndex; x++)
            {
                grid[x, y].isObstructed = true;
            }
        }
        return correspondingGridIndex;
    }

	// Return a Vector2 in which X component is grid's X index, Y component is its Y index
	public Vector2Int GetCorrespondingRoundedGridIndex(Vector3 position, bool returnFlooredIndex = false)
	{
		Vector3 positionWrtFirstGridPosition = position - firstGridPosition;
        Vector2Int roundedIndex;
        if (returnFlooredIndex)
        {
            roundedIndex = new Vector2Int(Mathf.FloorToInt(Mathf.Abs(positionWrtFirstGridPosition.x / singleGridWorldSize.x)),
                                          Mathf.FloorToInt(Mathf.Abs(positionWrtFirstGridPosition.y / singleGridWorldSize.y)));
        }
        else
        {
            roundedIndex = new Vector2Int(Mathf.RoundToInt(Mathf.Abs(positionWrtFirstGridPosition.x / singleGridWorldSize.x)),
                                          Mathf.RoundToInt(Mathf.Abs(positionWrtFirstGridPosition.y / singleGridWorldSize.y)));
        }

        if (roundedIndex.x >= gridExtents.x) roundedIndex.x = gridExtents.x - 1;
        if (roundedIndex.y >= gridExtents.y) roundedIndex.y = gridExtents.y - 1;
        
        return roundedIndex;
	}
    
    public float GetDistanceBtwTwoGrids(GridNode node1, GridNode node2)
    {
        return 10 * Vector2Int.Distance(node1.gridIndex, node2.gridIndex);
    }

	// Return the world position of the indexed grid.
	public Vector3 GetGridPosition(Vector2 index)
	{
		return firstGridPosition + new Vector3(index.x * singleGridWorldSize.x, index.y * -singleGridWorldSize.y, 0);
	}

    public void SetSelectedSoldier(GameObject soldier)
    {
        if (selectedSoldier)
        {
            selectedSoldier.GetComponent<SpriteRenderer>().color = Color.white;
        }

        selectedSoldier = soldier;

        if (selectedSoldier)
        {
            selectedSoldier.GetComponent<SpriteRenderer>().color = selectionColor;
            SetSelectedPlaceable(null);
        }
    }

    public void SetSelectedPlaceable(GameObject placeable)
    {
        if (selectedPlaceable)
        {
            selectedPlaceable.GetComponent<SpriteRenderer>().color = Color.white;
        }

        selectedPlaceable = placeable;

        if (selectedPlaceable)
        {
            selectedPlaceable.GetComponent<SpriteRenderer>().color = selectionColor;
            selectedPlaceable.GetComponent<Placeable>().OnSelection();
            SetSelectedSoldier(null);
        }

    }

    readonly public Vector2Int gridExtents = new Vector2Int(14, 14);

    public Color selectionColor;
    
    public GridNode[,] grid;

	private Vector3 firstGridPosition;
    // Grid size information. Not necessarily to be local variables. But it is nice to be able to reach them if necessary.
	// 14 * 32 pixels = 448 pixels.
	private Vector2 totalGridSize = new Vector2(4.48f, 4.48f);
	private Vector2 singleGridSize = new Vector2(0.32f, 0.32f);

	private Vector2 singleGridWorldSize;
    
    private GameObject selectedSoldier;
    private GameObject selectedPlaceable;
}
