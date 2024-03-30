using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Playables;


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
        _levelsPanel;

    public GameObject[] _levels, _levelsStartPoint, _levelsPoints, _levelsAnimation;

    public GameObject _pausePanel;

    private void Awake()
    {
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

    public void OpenTruckTransportBack()
    {
        _truckTransportBackAnimator.SetBool("Open", true);
    }


    public void ResumeTimeLine()
    {

    }
    public void PauseTimeLine(PlayableDirector _timeline)
    {
        _timeline.Pause();
    }

}
