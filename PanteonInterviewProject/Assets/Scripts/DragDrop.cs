using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform objectToCreate;
    private GameObject instantiatedObject;

    public void Awake()
    {
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }
    
    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        instantiatedObject = Instantiate(objectToCreate, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity).gameObject;
        if (instantiatedObject)
        {
            instantiatedObject.GetComponent<Placeable>().SetIsBeingPlaced(true);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (instantiatedObject)
        {
            instantiatedObject.GetComponent<Placeable>().SetIsBeingPlaced(false);

            // If instantiatedObject is in a condition which blocks it to be placed then it is being deleted.
            // If it is deleted then do not assign it as a selected placeable of the grid manager
            if (instantiatedObject.activeInHierarchy)
            {
                gridManager.SetSelectedPlaceable(instantiatedObject);
            }
        }
    }

    private GridManager gridManager;
}
