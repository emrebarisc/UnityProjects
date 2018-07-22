using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Placeable
{
    protected override void Awake()
    {
        base.Awake();

        this.size = new Vector2Int(3, 3);
        this.worldSize = new Vector3((float)size.x * transform.localScale.x, (float)size.y * transform.localScale.y);
    }

    override public void OnSelection()
    {
        infoPanelManager.ShowBarracksInfoPanel(this);
    }

    public Vector2Int GetSpawnGridIndex()
    {
        // Start and End indices controls if the barracks is in an edge of the grid or not.
        int startXIndex = placedGridIndex.x == 0 ? 0 : -1;
        int endXIndex = placedGridIndex.x + size.x > gridManager.gridExtents.x - 1 ? size.x - 1 : size.x;

        int startYIndex = placedGridIndex.y == 0 ? 0 : -1;
        int endYIndex = placedGridIndex.y + size.y > gridManager.gridExtents.y - 1 ? size.y - 1 : size.y;

        bool isSpawnPointFound = false;

        for (int y = startYIndex; y <= endYIndex; y++)
        {
            for (int x = startXIndex; x <= endXIndex; x++)
            {
                // If x and y indices are inside the barracks, then continue
                if ((y != -1 && y != size.y) && -1 < x && x < size.x)
                {
                    continue;
                }

                if (gridManager.grid[placedGridIndex.x + x, placedGridIndex.y + y].isObstructed)
                {
                    continue;
                }
                else
                {
                    spawnGridIndex = new Vector2Int(placedGridIndex.x + x, placedGridIndex.y + y);
                    isSpawnPointFound = true;
                    break;
                }
            }
            if (isSpawnPointFound)
            {
                break;
            }
        }

        if(!isSpawnPointFound)
        {
            spawnGridIndex = new Vector2Int(-1, -1);
        }

        return spawnGridIndex;
    }
    
    private Vector2Int spawnGridIndex;
}
