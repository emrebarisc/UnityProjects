using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelManager : MonoBehaviour
{
    void Awake()
    {
        barracksInfoPanel = GameObject.FindGameObjectWithTag("BarracksInfoPanel");
        powerPlantInfoPanel = GameObject.FindGameObjectWithTag("PowerPlantInfoPanel");

        barracksInfoPanel.SetActive(false);
        powerPlantInfoPanel.SetActive(false);
    }

    public void ShowBarracksInfoPanel(Barracks barracks)
    {
        if(!barracks)
        {
            barracksInfoPanel.SetActive(false);
            return;
        }

        barracksInfoPanel.SetActive(true);

        GameObject.FindGameObjectWithTag("SoldierSpawner").GetComponent<SoldierSpawner>().SetBarracks(barracks);

        powerPlantInfoPanel.SetActive(false);
    }

    public void ShowPowerPlantInfoPanel()
    {
        barracksInfoPanel.SetActive(false);
        powerPlantInfoPanel.SetActive(true);
    }

    public void HideAllPanels()
    {
        barracksInfoPanel.SetActive(false);
        powerPlantInfoPanel.SetActive(false);
    }

    private GameObject barracksInfoPanel;
    private GameObject powerPlantInfoPanel;
}
