using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CargoTruckLevelComplete : MonoBehaviour
{
    public GameObject _loadingPanel;
    public Slider _loadingBar;

    public void Home()
    {
        _loadingPanel.SetActive(true);
        StartCoroutine(LoadScreen());

    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(1);


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
    public void NextLevel()
    {

        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        PlayerPrefs.SetInt("offroadLevelSelection", PlayerPrefs.GetInt("offroadLevelSelection") + 1);

        SceneManager.LoadScene(1);
    }
}
