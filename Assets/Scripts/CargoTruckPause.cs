using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class CargoTruckPause : MonoBehaviour
{
    public GameObject _loadingPanel;
    public Slider _loadingBar;

    public GameObject _pausePanel;

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
        AsyncOperation _loadOperation = SceneManager.LoadSceneAsync(0);
        while (!_loadOperation.isDone)
        {
            float _progress = Mathf.Clamp01(_loadOperation.progress / 0.9f);
            _loadingBar.value = _progress;
            yield return null;
        }

    }




    public void ResumeLevel()
    {
        _pausePanel.SetActive(false);
        CargoTruckManager1._link._car.SetActive(true);
        Time.timeScale = 1f;
    }
}
