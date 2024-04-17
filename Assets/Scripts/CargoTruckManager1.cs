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
        _truckDump,
        _toeTruck,
        _beansBag,
        _tyres,
        _turckRain;

    public GameObject[] _levels, _levelsStartPoint, _levelsPoints, _levelsAnimation;
    public Material _rainyDayMaterial;

    private void Awake()
    {
        Time.timeScale = 1f;

        _link = this;
        _levels[PlayerPrefs.GetInt("Level")].SetActive(true);
        _car.transform.SetPositionAndRotation(_levelsStartPoint[PlayerPrefs.GetInt("Level")].transform.position, _levelsStartPoint[PlayerPrefs.GetInt("Level")].transform.rotation);

        if (PlayerPrefs.GetInt("Level") == 0)
        {
            _levelsPoints[0].SetActive(true);
        } else if (PlayerPrefs.GetInt("Level") == 1)
        {
            _levelsPoints[2].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Level") == 2)
        {
            RenderSettings.skybox = _rainyDayMaterial;
            _turckRain.SetActive(true);
            _levelsPoints[5].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Level") == 3)
        {
            _levelsPoints[8].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Level") == 4)
        {
            _levelsPoints[9].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Level") == 5)
        {
            _truckDump.SetActive(false);
            _toeTruck.SetActive(true);

            _levelsPoints[11].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Level") == 6)
        {
            _levelsPoints[12].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Level") == 7)
        {
            _beansBag.SetActive(true);

            _levelsPoints[14].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Level") == 8)
        {
            _levelsPoints[15].SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Level") == 9)
        {
            _tyres.SetActive(true);

            _levelsPoints[17].SetActive(true);
        }
    }

    public void LevelComplete(GameObject _point)
    {
        _point.SetActive(false);
        _carCamera.SetActive(false);
        _car.SetActive(false);
        _carCanvas.SetActive(false);
        _finalCamera.SetActive(true);
        _completePanel.SetActive(true);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 500);
    }

    public void LevelFail()
    {
        _carCamera.SetActive(false);
        _car.SetActive(false);
        _carCanvas.SetActive(false);
        _finalCamera.SetActive(true);
        _failPanel.SetActive(true);
    }
    public void SkipAnimation(int _currentPoint)
    {
        _levelsPoints[_currentPoint].SetActive(false);
        _levelsPoints[_currentPoint + 1].SetActive(true);
        _carCamera.SetActive(true);
        _car.SetActive(true);
        _carCanvas.SetActive(true);
    }

    public void ObjectOn(GameObject _object)
    {
        _object.SetActive(true);
    }
    public void ObjectOff(GameObject _object)
    {
        _object.SetActive(false);

    }
    
}
