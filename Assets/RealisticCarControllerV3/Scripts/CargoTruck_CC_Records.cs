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

public class CargoTruck_CC_Records : ScriptableObject {

    #region singleton
    private static CargoTruck_CC_Records instance;
    public static CargoTruck_CC_Records Instance { get { if (instance == null) instance = Resources.Load("RCC Assets/CargoTruck_CC_Records") as CargoTruck_CC_Records; return instance; } }
    #endregion

    public List<CargoTruck_CC_Recorder.Recorded> records = new List<CargoTruck_CC_Recorder.Recorded>();

}
