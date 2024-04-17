//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2022 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

#pragma warning disable 0414

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Mobile UI Drag used for orbiting RCC Camera.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Mobile/RCC UI Drag")]
public class CargoTruck_CC_MobileUIDrag : MonoBehaviour, IDragHandler, IEndDragHandler {

    private bool isPressing = false;

    void Awake() {

        if (!CargoTruck_CC_Settings.Instance.mobileControllerEnabled) {

            gameObject.SetActive(false);
            return;

        }

    }

    public void OnDrag(PointerEventData data) {

        if (!CargoTruck_CC_Settings.Instance.mobileControllerEnabled)
            return;

        isPressing = true;

        CargoTruck_CC_SceneManager.Instance.activePlayerCamera.OnDrag(data);

    }

    public void OnEndDrag(PointerEventData data) {

        if (!CargoTruck_CC_Settings.Instance.mobileControllerEnabled)
            return;

        isPressing = false;

    }

    void OnDisable() {

        isPressing = false;

    }

}
