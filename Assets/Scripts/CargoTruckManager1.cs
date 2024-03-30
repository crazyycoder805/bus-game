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

    public void OpenTruckTransportBack(GameObject _btn)
    {
        _truckTransportBackAnimator.SetBool("Open", true);
        _timelineOut.GetComponent<PlayableDirector>().Resume();
        _btn.SetActive(false);
    }


    public void ResumeTimeLine()
    {
        _timelineOut.GetComponent<PlayableDirector>().Pause();
    }
    public void PauseTimeLine(GameObject _timeline)
    {
        _timelineOut = _timeline;
        _timeline.GetComponent<PlayableDirector>().Pause();
    }
    public void MoveForwardUp()
    {
        _timelineOut.GetComponent<PlayableDirector>().Pause();
    }
    public void MoveForwardDown(GameObject _timelin)

    {
        _timelineOut = _timelin;
        _timelin.GetComponent<PlayableDirector>().Resume();
    }
}
