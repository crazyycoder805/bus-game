﻿//----------------------------------------------
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

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Skidmarks Manager")]
public class CargoTruck_CC_SkidmarksManager : CargoTruck_CC_Singleton<CargoTruck_CC_SkidmarksManager> {

    private CargoTruck_CC_Skidmarks[] skidmarks;
    private int[] skidmarksIndexes;
    private int _lastGroundIndex = 0;

    void Awake() {

        skidmarks = new CargoTruck_CC_Skidmarks[CargoTruck_CC_GroundMaterials.Instance.frictions.Length];
        skidmarksIndexes = new int[skidmarks.Length];

        for (int i = 0; i < skidmarks.Length; i++) {

            skidmarks[i] = Instantiate(CargoTruck_CC_GroundMaterials.Instance.frictions[i].skidmark, Vector3.zero, Quaternion.identity);
            skidmarks[i].transform.name = skidmarks[i].transform.name + "_" + CargoTruck_CC_GroundMaterials.Instance.frictions[i].groundMaterial.name;
            skidmarks[i].transform.SetParent(transform, true);

        }

    }

    // Function called by the wheels that is skidding. Gathers all the information needed to
    // create the mesh later. Sets the intensity of the skidmark section b setting the alpha
    // of the vertex color.
    public int AddSkidMark(Vector3 pos, Vector3 normal, float intensity, float width, int lastIndex, int groundIndex) {

        if (_lastGroundIndex != groundIndex) {

            _lastGroundIndex = groundIndex;
            return -1;

        }

        skidmarksIndexes[groundIndex] = skidmarks[groundIndex].AddSkidMark(pos, normal, intensity, width, lastIndex);

        return skidmarksIndexes[groundIndex];

    }

    public void CleanSkidmarks() {

        for (int i = 0; i < skidmarks.Length; i++)
            skidmarks[i].Clean();

    }

    public void CleanSkidmarks(int index) {

        skidmarks[index].Clean();

    }

}
