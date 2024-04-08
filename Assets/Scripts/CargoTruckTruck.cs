using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTruckTruck : MonoBehaviour
{
    int _check;
    public static CargoTruckTruck _link;
    public GameObject _emptyObject;
    private void Awake()
    {
        _check = 0;
        _link = this;
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.transform.CompareTag("Finish"))
        {
            if (PlayerPrefs.GetInt("Level") == 0)
            {
                if (_check == 0)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[0].SetActive(true);
                    _check = 1;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                } else if (_check == 1)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[1].SetActive(true);
                    _check = 2;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else if (PlayerPrefs.GetInt("Level") == 1)
            {
                if (_check == 0)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[2].SetActive(true);
                    _check = 1;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                else if (_check == 1)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[3].SetActive(true);
                    _emptyObject = _other.gameObject;
                    CargoTruckManager1._link._car.transform.SetPositionAndRotation(_emptyObject.transform.position, _emptyObject.transform.rotation);
                    _check = 2;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                else if (_check == 2)
                {

                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[4].SetActive(true);
                    _check = 3;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else if (PlayerPrefs.GetInt("Level") == 2)
            {
                if (_check == 0)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[5].SetActive(true);
                    _check = 1;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                else if (_check == 1)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[6].SetActive(true);
                    _emptyObject = _other.gameObject;
                    CargoTruckManager1._link._car.transform.SetPositionAndRotation(_emptyObject.transform.position, _emptyObject.transform.rotation);
                    _check = 2;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                else if (_check == 2)
                {
                    CargoTruckManager1._link.LevelComplete(CargoTruckManager1._link._levelsPoints[7]);
                    
                    _check = 3;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                    
                }


                
            }
            else if (PlayerPrefs.GetInt("Level") == 3)
            {
                if (_check == 0)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[7].SetActive(true);
                    _check = 1;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;

                }
                
            }
            else if (PlayerPrefs.GetInt("Level") == 4)
            {
                if (_check == 0)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[8].SetActive(true);
                    _check = 1;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                else if (_check == 1)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[9].SetActive(true);
                    _check = 2;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else if (PlayerPrefs.GetInt("Level") == 5)
            {
                if (_check == 0)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[10].SetActive(true);
                    _check = 1;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                
            }
            else if (PlayerPrefs.GetInt("Level") == 6)
            {
                if (_check == 0)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[11].SetActive(true);
                    _check = 1;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                else if (_check == 1)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[12].SetActive(true);
                    _check = 2;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }

            }
            else if (PlayerPrefs.GetInt("Level") == 7)
            {
                if (_check == 0)
                {

                    CargoTruckManager1._link.LevelComplete(CargoTruckManager1._link._levelsPoints[14]);
                    _check = 1;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }

            }
            else if (PlayerPrefs.GetInt("Level") == 8)
            {
                if (_check == 0)
                {

                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[13].SetActive(true);
                    _check = 1;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                else if (_check == 1)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._car.SetActive(false);
                    CargoTruckManager1._link._carCanvas.SetActive(false);

                    CargoTruckManager1._link._levelsAnimation[14].SetActive(true);
                    _check = 2;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else if (PlayerPrefs.GetInt("Level") == 9)
            {
                if (_check == 0)
                {

                    CargoTruckManager1._link.LevelComplete(CargoTruckManager1._link._levelsPoints[17]);
                    _check = 1;
                    _other.gameObject.GetComponent<BoxCollider>().enabled = false;
                }

            }

        } else if (_other.CompareTag("Fail"))
        {
            CargoTruckManager1._link.LevelFail();
        }
    }
}
