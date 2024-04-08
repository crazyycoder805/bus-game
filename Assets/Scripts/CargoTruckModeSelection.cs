using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoTruckModeSelection : MonoBehaviour
{
    public GameObject _modeSelectionPanel, _mainMenuPanel, _offroadLevelSelectionPanel;
    public Text _coinsText;
    void Awake()
    {
        _coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

    }
    public void Back()
    {
        _mainMenuPanel.SetActive(true);
        _modeSelectionPanel.SetActive(false);
    }

    public void ModeSelect()
    {
        _offroadLevelSelectionPanel.SetActive(true);
        _modeSelectionPanel.SetActive(false);
    }
}
