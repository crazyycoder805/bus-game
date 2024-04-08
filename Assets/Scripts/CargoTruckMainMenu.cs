using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CargoTruckMainMenu : MonoBehaviour
{
    public GameObject _gameExitPanel, _modeSelectionPanel, _mainMenuPanel;
    public Text _coinsText;
    void Awake()
    {
        _coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

    }
    public void PlayBtn()
    {
        _mainMenuPanel.SetActive(false);
        _modeSelectionPanel.SetActive(true);
    }

    public void GameExit()
    {
        _gameExitPanel.SetActive(true);
        _mainMenuPanel.SetActive(false);
    }
}
