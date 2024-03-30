using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTruckTruck : MonoBehaviour
{
    int _check;
    public static CargoTruckTruck _link;
    private void Awake()
    {
        _link = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Finish"))
        {
            if (PlayerPrefs.GetInt("Level") == 0)
            {
                if (_check == 0)
                {
                    CargoTruckManager1._link._carCamera.SetActive(false);
                    CargoTruckManager1._link._levelsAnimation[0].SetActive(true);
                }
            }
            
            
        }
    }
}
