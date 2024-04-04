using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTruckTruck : MonoBehaviour
{
    int _check;
    public static CargoTruckTruck _link;
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


        }
    }
}
