using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    public PathFinder()
    {
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }
    
    public bool FindPath(Vector2Int sourceIndex, Vector2Int destinationIndex, out List<Vector2Int> path)
    {
        path = new List<Vector2Int>();

        GridNode startNode = gridManager.grid[sourceIndex.x, sourceIndex.y];
        GridNode destinationNode = gridManager.grid[destinationIndex.x, destinationIndex.y];

        List<GridNode> openList = new List<GridNode>();
        HashSet<GridNode> closedList = new HashSet<GridNode>();
        openList.Add(startNode);

        while(openList.Count > 0)
        {
            GridNode currentNode = openList[0];

            for (int openListIndex = 0; openListIndex < openList.Count; openListIndex++)
            {
                if(openList[openListIndex].fCost < currentNode.fCost || (openList[openListIndex].fCost == currentNode.fCost && openList[openListIndex].hCost < currentNode.hCost))
                {
                    currentNode = openList[openListIndex];
                }
            }

            openList.Remove(currentNode);

            if (currentNode == destinationNode)
            {
                path = RetracePath(startNode, destinationNode);
                return true;
            }

            foreach (Vector2Int index in gridManager.GetNeighbouringGrids(currentNode))
            {
                GridNode neighbour = gridManager.grid[index.x, index.y];

                if(neighbour.isObstructed || closedList.Contains(neighbour))
                {
                    continue;
                }

                float costToTheNeighbour = currentNode.gCost + gridManager.GetDistanceBtwTwoGrids(currentNode, neighbour);

                if(costToTheNeighbour < neighbour.gCost || !openList.Contains(neighbour))
                {
                    neighbour.gCost = costToTheNeighbour;
                    neighbour.hCost = gridManager.GetDistanceBtwTwoGrids(neighbour, destinationNode);
                    neighbour.parent = currentNode;

                    if(!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }

            // All neighbours are checked. Add currentNode to the closedList.
            closedList.Add(currentNode);
        }

        return false;
    }

    List<Vector2Int> RetracePath(GridNode start, GridNode end)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        GridNode currentNode = end;

        while(currentNode != start)
        {
            path.Add(currentNode.gridIndex);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    private GridManager gridManager;
}
