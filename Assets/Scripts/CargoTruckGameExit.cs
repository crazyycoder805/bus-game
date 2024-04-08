using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTruckGameExit : MonoBehaviour
{
    public GameObject _gameExitPanel, _mainMenuPanel;


    public void Yes()
    {
        Application.Quit();
    }

    public void No()
    {
        _gameExitPanel.SetActive(false);
        _mainMenuPanel.SetActive(true);
    }
}
