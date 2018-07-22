using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoldierController : MonoBehaviour, IPointerClickHandler
{
    public void Awake()
    {
        pathFinder = new PathFinder();
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gridManager.SetSelectedSoldier(gameObject);
    }

    void FixedUpdate()
    {
        if (isMovingToDestionation)
        {
            timer += Time.deltaTime;

            if (timer >= 0.5f - soldierSpeed / 200)
            {
                MoveToNextGrid();
                timer = 0;
            }
        }
    }

    public void MoveTo(Vector2Int destination)
    {
        path.Clear();

        bool pathFound = pathFinder.FindPath(currentGridIndex, destination, out path);

        if (pathFound)
        {
            isMovingToDestionation = true;
        }
        else
        {
            Debug.Log("Soldier cannot walk there.");
        }
    }

    void MoveToNextGrid()
    {
        gameObject.transform.position = gridManager.GetGridPosition(path[0]);

        // Set old grid index not obstructed
        gridManager.grid[currentGridIndex.x, currentGridIndex.y].isObstructed = false;
        currentGridIndex = path[0];

        // Set new grid index obstructed
        gridManager.grid[currentGridIndex.x, currentGridIndex.y].isObstructed = true;
        path.RemoveAt(0);

        if(path.Count == 0) isMovingToDestionation = false;
    }

    public Vector2Int currentGridIndex;

    [Range(0.0f, 100.0f)]
    public float soldierSpeed = 50.0f;

    List<Vector2Int> path = new List<Vector2Int>();

    private GridManager gridManager;
    private PathFinder pathFinder;
    private float timer = 0;
    private bool isMovingToDestionation = false;
    private bool isSelected;
}
