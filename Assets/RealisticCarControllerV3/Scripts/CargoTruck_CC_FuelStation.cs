//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2022 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Fuel Station")]
public class CargoTruck_CC_FuelStation : MonoBehaviour {

    private CargoTruck_CC_CarControllerV3 targetVehicle;
    public float refillSpeed = 1f;

    void OnTriggerStay(Collider col) {

        if (targetVehicle == null) {

            if (col.gameObject.GetComponentInParent<CargoTruck_CC_CarControllerV3>())
                targetVehicle = col.gameObject.GetComponentInParent<CargoTruck_CC_CarControllerV3>();

        }

        if (targetVehicle)
            targetVehicle.fuelTank += refillSpeed * Time.deltaTime;

    }

    void OnTriggerExit(Collider col) {

        if (col.gameObject.GetComponentInParent<CargoTruck_CC_CarControllerV3>())
            targetVehicle = null;

    }

}
