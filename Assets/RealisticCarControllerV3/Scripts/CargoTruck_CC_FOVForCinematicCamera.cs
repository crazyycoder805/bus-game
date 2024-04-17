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
/// Animation attached to "Animation Pivot" of the Cinematic Camera is feeding FOV float value.
/// </summary>
public class CargoTruck_CC_FOVForCinematicCamera : MonoBehaviour {

    private CargoTruck_CC_CinematicCamera cinematicCamera;
    public float FOV = 30f;

    void Awake() {

        cinematicCamera = GetComponentInParent<CargoTruck_CC_CinematicCamera>();

    }

    void Update() {

        cinematicCamera.targetFOV = FOV;

    }

}
