using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Placeable : MonoBehaviour, IPointerClickHandler
{
    virtual protected void Awake()
    {
        infoPanelManager = GameObject.FindGameObjectWithTag("InfoPanelManager").GetComponent<InfoPanelManager>();
        gridManager = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
    }

    public Vector2Int GetSize()
    {
        return size;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gridManager.SetSelectedPlaceable(gameObject);

        OnSelection();
    }

    virtual public void OnSelection()
    {

    }

    virtual public void OnDeselection()
    {
        infoPanelManager.HideAllPanels();
    }

    // Called by DragDrop script of corresponding UI thumbnail when dropped.
    public void SetIsBeingPlaced(bool value)
    {
        isBeingPlaced = value;

        // If the object if dragged but not dropped yet.
        if (isBeingPlaced)
        {
            StartCoroutine("FollowTheMouse");
        }
        else
        {
            if (isCollidingWithAnotherPlaceable)
            {
                Destroy(gameObject);
                gameObject.SetActive(false);
            }
            else if (isFullyInsideOfTheGrid)
            {
                placedGridIndex = gridManager.PutPlaceableToTheGrid(this);
            }
            else
            {
                Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator FollowTheMouse()
    {
        while(isBeingPlaced)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            if(!isFullyInsideOfTheGrid || isCollidingWithAnotherPlaceable) this.GetComponent<SpriteRenderer>().color = unplaceableColor;
            else this.GetComponent<SpriteRenderer>().color = Color.white;

            yield return null;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Placeable")
        {
            isCollidingWithAnotherPlaceable = true;
        }
        else if (other.tag == "Grid")
        {
            isFullyInsideOfTheGrid = IsInside(other.GetComponent<BoxCollider2D>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Placeable")
        {
            isCollidingWithAnotherPlaceable = false;
        }
        else if (other.tag == "Grid")
        {
            isFullyInsideOfTheGrid = false;
        }
    }

    // Used for understanding if the object is fully inside of the grid or not.
    bool IsInside(BoxCollider2D other)
    {
        Bounds owningColliderBounds = GetComponent<BoxCollider2D>().bounds;

        Vector2 center = owningColliderBounds.center;
        Vector2 extents = owningColliderBounds.extents;
        
        Vector2[] owningCorners = { new Vector2(center.x - extents.x, center.y + extents.y),
                                    new Vector2(center.x + extents.x, center.y + extents.y),
                                    new Vector2(center.x - extents.x, center.y - extents.y),
                                    new Vector2(center.x + extents.x, center.y - extents.y) };

        foreach (Vector2 corner in owningCorners)
        {
            if (!other.bounds.Contains(corner))
            {
                return false;
            }
        }
        return true;
    }
    
    public Color unplaceableColor;

    protected InfoPanelManager infoPanelManager;
    protected GridManager gridManager;

    // Placeable size information. Not necessarily to be local variables. But it is nice to be able to reach them if necessary.
    protected Vector2Int size;
    protected Vector3 worldSize;

    // To store where the placeable object is placed in the grid in order to calculate where the spawn location should be at.
    protected Vector2Int placedGridIndex;

    protected bool isFullyInsideOfTheGrid = false;
    protected bool isBeingPlaced;

    private bool isCollidingWithAnotherPlaceable = false;
}
