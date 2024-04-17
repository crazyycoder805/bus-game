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

public class CargoTruck_CC_DemoVehicles : ScriptableObject {

    public CargoTruck_CC_CarControllerV3[] vehicles;

    #region singleton
    private static CargoTruck_CC_DemoVehicles instance;
    public static CargoTruck_CC_DemoVehicles Instance { get { if (instance == null) instance = Resources.Load("RCC Assets/CargoTruck_CC_DemoVehicles") as CargoTruck_CC_DemoVehicles; return instance; } }
    #endregion

}
