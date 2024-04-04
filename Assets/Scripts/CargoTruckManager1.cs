using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Playables;
using UnityEngine.SceneManagement;



public class CargoTruckManager1 : MonoBehaviour
{
    public Animator _truckTransportBackAnimator;
    public static CargoTruckManager1 _link;
    public GameObject
        _car,
        _carCanvas,
        _carCamera,
        _completePanel,
        _finalCamera,
        _failPanel,
        _levelsPanel,
        _timelineOut;

    public GameObject[] _levels, _levelsStartPoint, _levelsPoints, _levelsAnimation;

    public GameObject _pausePanel;


    private void Awake()
    {
        _link = this;
        _levels[PlayerPrefs.GetInt("Level")].SetActive(true);
        _car.transform.SetPositionAndRotation(_levelsStartPoint[PlayerPrefs.GetInt("Level")].transform.position, _levelsStartPoint[PlayerPrefs.GetInt("Level")].transform.rotation);

        if (PlayerPrefs.GetInt("Level") == 0)
        {
            _levelsPoints[0].SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }
    public void LevelComplete(GameObject _point)
    {
        _point.SetActive(false);
        _carCamera.SetActive(false);
        _car.SetActive(false);
        _carCanvas.SetActive(false);
        _completePanel.SetActive(true);
    }
    public void SkipAnimation(int _currentPoint)
    {
        _levelsPoints[_currentPoint].SetActive(false);
        _levelsPoints[_currentPoint + 1].SetActive(true);
        _carCamera.SetActive(true);
        _car.SetActive(true);
        _carCanvas.SetActive(true);
    }

}
