using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollbarManager : MonoBehaviour
{
	void Start ()
    {
        scrollbar.onValueChanged.AddListener( delegate { OnSliderValueChange(); } );

    }

    void FixedUpdate()
    {
        if(!Input.GetMouseButton(0) && isScrollbarValueChanged)
        {
            scrollbar.value = 0.5f;
            isScrollbarValueChanged = false;
        }
        else if (isScrollbarValueChanged && Mathf.Abs(scrollbarWeightedDirection) > 0.05f)
        {
            Debug.Log(scrollbarWeightedDirection);

            thumbnailHolder1.transform.position += new Vector3(0, scrollbarWeightedDirection, 0) * scrollingSpeed;
            thumbnailHolder2.transform.position += new Vector3(0, scrollbarWeightedDirection, 0) * scrollingSpeed;

            if (scrollbarWeightedDirection < 0)
            {
                if(thumbnailHolder1.transform.position.y < 250)
                {
                    RectTransform th1RT = thumbnailHolder1.transform as RectTransform;
                    thumbnailHolder2.transform.position = thumbnailHolder1.transform.position + new Vector3(0, th1RT.rect.height, 0);
                }
                if(thumbnailHolder2.transform.position.y < 250)
                {
                    RectTransform th2RT = thumbnailHolder2.transform as RectTransform;
                    thumbnailHolder1.transform.position = thumbnailHolder2.transform.position + new Vector3(0, th2RT.rect.height, 0);
                }
            }

            if (0 < scrollbarWeightedDirection)
            {
                if(500 < thumbnailHolder1.transform.position.y)
                {
                    RectTransform th1RT = thumbnailHolder1.transform as RectTransform;
                    thumbnailHolder2.transform.position = thumbnailHolder1.transform.position - new Vector3(0, th1RT.rect.height, 0);
                }
                if (500 < thumbnailHolder2.transform.position.y)
                {
                    RectTransform th2RT = thumbnailHolder2.transform as RectTransform;
                    thumbnailHolder1.transform.position = thumbnailHolder2.transform.position - new Vector3(0, th2RT.rect.height, 0);
                }
            }
        }
    }

    void OnSliderValueChange()
    {
        isScrollbarValueChanged = true;
        scrollbarWeightedDirection = (scrollbar.value - 0.5f) * 2.0f;
    }
    
    public GameObject placeablesPanel;
    public GameObject barracksThumbnail;
    public GameObject powerPlantThumbnail;

    public GameObject thumbnailHolder1;
    public GameObject thumbnailHolder2;

    public Scrollbar scrollbar;

    private float scrollingSpeed = 100;
    private float scrollbarWeightedDirection = 0;

    private bool isScrollbarValueChanged = false;
}
