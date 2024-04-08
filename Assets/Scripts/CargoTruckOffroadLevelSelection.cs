using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class CargoTruckOffroadLevelSelection : MonoBehaviour
{
    public GameObject _modeSelectionPanel, _offroadLevelSelectionPanel, _loadingPanel;
	[SerializeField] List<Button> _levels = new List<Button>();
	public string _pref;
	public Slider _loadingBar;
	public Text _coinsText;

	public void Back()
    {
        _modeSelectionPanel.SetActive(true);
        _offroadLevelSelectionPanel.SetActive(false);
    }


	void Awake()
	{
		_coinsText.text = PlayerPrefs.GetInt("Coins").ToString();

	}
	
	void Start()
	{
		if (PlayerPrefs.GetInt(_pref) == 0)
		{
			PlayerPrefs.SetInt(_pref, 1);
		}

		if (_levels.Count > 0)
		{
			foreach (Button btn in _levels)
			{
				btn.interactable = false;
			}


			for (int i = 0; i < (PlayerPrefs.GetInt(_pref)); i++)
			{
				_levels[i].interactable = true;
				_levels[i].transform.GetChild(0).gameObject.SetActive(false);


				if (i == 9)
				{
					break;
				}
			}
		}

	}

	public void LevelSelect(int _index)
	{
		_loadingPanel.SetActive(true);
		PlayerPrefs.SetInt("Level", _index);
		StartCoroutine(LoadScreen());

	}


	public IEnumerator LoadScreen()
	{
		AsyncOperation _loadOperation = SceneManager.LoadSceneAsync(1);
		while (!_loadOperation.isDone)
		{
			float _progress = Mathf.Clamp01(_loadOperation.progress / 0.9f);
			_loadingBar.value = _progress;
			yield return null;
		}
	}
}
