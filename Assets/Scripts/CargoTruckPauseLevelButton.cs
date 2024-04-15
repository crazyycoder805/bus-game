using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CargoTruckPauseLevelButton : MonoBehaviour
{
    public GameObject _pausePanel;

    public void PauseLevel()
    {
        _pausePanel.SetActive(true);
        CargoTruckManager1._link._car.SetActive(false);
        Time.timeScale = 0f;
    }

}
