//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2022 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// Changes wheels (visual only) at runtime. It holds changable wheels as prefab in an array.
/// </summary>
[System.Serializable]
public class CargoTruck_CC_ChangableWheels : ScriptableObject {

    #region singleton
    private static CargoTruck_CC_ChangableWheels instance;
    public static CargoTruck_CC_ChangableWheels Instance { get { if (instance == null) instance = Resources.Load("RCC Assets/CargoTruck_CC_ChangableWheels") as CargoTruck_CC_ChangableWheels; return instance; } }
    #endregion

    [System.Serializable]
    public class ChangableWheels {

        public GameObject wheel;

    }

    public ChangableWheels[] wheels;

}


