﻿//----------------------------------------------
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
/// Locks rotation of the shadow projector to avoid stretching.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Shadow")]
public class CargoTruck_CC_ShadowRotConst : MonoBehaviour {

    private Transform root;

    void Start() {

        root = GetComponentInParent<CargoTruck_CC_CarControllerV3>().transform;

    }

    void Update() {

        transform.rotation = Quaternion.Euler(90f, root.eulerAngles.y, 0f);

    }

}