using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoldierSpawner : MonoBehaviour, IPointerClickHandler
{
    void Awake()
    {
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }

    public void SetBarracks(Barracks b)
    {
        barracks = b;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2Int spawnGridIndex = barracks.GetSpawnGridIndex();
        if (spawnGridIndex == new Vector2Int(-1, -1))
        {
            Debug.Log("No place to spawn the soldier!!!");
        }
        else
        {
            Vector3 spawnPosition = gridManager.GetGridPosition(spawnGridIndex);
            GameObject soldier = Instantiate(soldierPrefab, spawnPosition, Quaternion.identity);
            soldier.GetComponent<SoldierController>().currentGridIndex = spawnGridIndex;

            gridManager.SetSelectedSoldier(soldier);

            gridManager.grid[spawnGridIndex.x, spawnGridIndex.y].isObstructed = true;
        }
    }

    public GameObject soldierPrefab;
    private GridManager gridManager;
    private Barracks barracks;
}
