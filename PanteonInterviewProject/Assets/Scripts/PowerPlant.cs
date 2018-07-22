using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Placeable
{
    protected override void Awake()
    {
        base.Awake();

        this.size = new Vector2Int(3, 2);
        this.worldSize = new Vector3((float)size.x * transform.localScale.x, (float)size.y * transform.localScale.y);
    }

    override public void OnSelection()
    {
        infoPanelManager.ShowPowerPlantInfoPanel();
    }
}
