﻿//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2022 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

/// <summary>
/// Receiving inputs from UI buttons, and feeds active vehicles on your scene.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Mobile/RCC UI Mobile Buttons")]
public class CargoTruck_CC_MobileButtons : CargoTruck_CC_Core {

    public CargoTruck_CC_UIController gasButton;
    public CargoTruck_CC_UIController gradualGasButton;
    public CargoTruck_CC_UIController brakeButton;
    public CargoTruck_CC_UIController leftButton;
    public CargoTruck_CC_UIController rightButton;
    public CargoTruck_CC_UISteeringWheelController steeringWheel;
    public CargoTruck_CC_UIController handbrakeButton;
    public CargoTruck_CC_UIController NOSButton;
    public CargoTruck_CC_UIController NOSButtonSteeringWheel;
    public GameObject gearButton;

    public CargoTruck_CC_UIJoystick joystick;

    public static CargoTruck_CC_Inputs mobileInputs = new CargoTruck_CC_Inputs();

    private float throttleInput = 0f;
    private float brakeInput = 0f;
    private float leftInput = 0f;
    private float rightInput = 0f;
    private float steeringWheelInput = 0f;
    private float handbrakeInput = 0f;
    private float boostInput = 1f;
    private float gyroInput = 0f;
    private float joystickInput = 0f;
    private float horizontalInput;
    private float verticalInput;
    private bool canUseNos = false;

    private Vector3 orgBrakeButtonPos;

    void Start() {

        if (brakeButton)
            orgBrakeButtonPos = brakeButton.transform.position;

        CheckController();

    }

    void OnEnable() {

        CargoTruck_CC_SceneManager.OnVehicleChanged += CheckController;

    }

    private void CheckController() {

        if (!CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
            return;

        if (CargoTruck_CC_Settings.Instance.mobileControllerEnabled) {

            EnableButtons();
            return;

        } else {

            DisableButtons();
            return;

        }

    }

    void DisableButtons() {

        if (gasButton)
            gasButton.gameObject.SetActive(false);
        if (gradualGasButton)
            gradualGasButton.gameObject.SetActive(false);
        if (leftButton)
            leftButton.gameObject.SetActive(false);
        if (rightButton)
            rightButton.gameObject.SetActive(false);
        if (brakeButton)
            brakeButton.gameObject.SetActive(false);
        if (steeringWheel)
            steeringWheel.gameObject.SetActive(false);
        if (handbrakeButton)
            handbrakeButton.gameObject.SetActive(false);
        if (NOSButton)
            NOSButton.gameObject.SetActive(false);
        if (NOSButtonSteeringWheel)
            NOSButtonSteeringWheel.gameObject.SetActive(false);
        if (gearButton)
            gearButton.gameObject.SetActive(false);
        if (joystick)
            joystick.gameObject.SetActive(false);

    }

    void EnableButtons() {

        if (gasButton)
            gasButton.gameObject.SetActive(true);
        //			if (gradualGasButton)
        //				gradualGasButton.gameObject.SetActive (true);
        if (leftButton)
            leftButton.gameObject.SetActive(true);
        if (rightButton)
            rightButton.gameObject.SetActive(true);
        if (brakeButton)
            brakeButton.gameObject.SetActive(true);
        if (steeringWheel)
            steeringWheel.gameObject.SetActive(true);
        if (handbrakeButton)
            handbrakeButton.gameObject.SetActive(true);

        if (canUseNos) {

            if (NOSButton)
                NOSButton.gameObject.SetActive(true);
            if (NOSButtonSteeringWheel)
                NOSButtonSteeringWheel.gameObject.SetActive(true);

        }

        if (joystick)
            joystick.gameObject.SetActive(true);

    }

    void Update() {

        if (!CargoTruck_CC_Settings.Instance.mobileControllerEnabled)
            return;

        switch (CargoTruck_CC_Settings.Instance.mobileController) {

            case CargoTruck_CC_Settings.MobileController.TouchScreen:

                if (CargoTruck_CC_InputManager.gyroUsed) {

                    CargoTruck_CC_InputManager.gyroUsed = false;
                    InputSystem.DisableDevice(UnityEngine.InputSystem.Accelerometer.current);

                }

                gyroInput = 0f;

                if (steeringWheel && steeringWheel.gameObject.activeInHierarchy)
                    steeringWheel.gameObject.SetActive(false);

                if (NOSButton && NOSButton.gameObject.activeInHierarchy != canUseNos)
                    NOSButton.gameObject.SetActive(canUseNos);

                if (joystick && joystick.gameObject.activeInHierarchy)
                    joystick.gameObject.SetActive(false);

                if (!leftButton.gameObject.activeInHierarchy) {

                    brakeButton.transform.position = orgBrakeButtonPos;
                    leftButton.gameObject.SetActive(true);

                }

                if (!rightButton.gameObject.activeInHierarchy)
                    rightButton.gameObject.SetActive(true);

                break;

            case CargoTruck_CC_Settings.MobileController.Gyro:

                if (!CargoTruck_CC_InputManager.gyroUsed) {

                    CargoTruck_CC_InputManager.gyroUsed = true;
                    InputSystem.EnableDevice(UnityEngine.InputSystem.Accelerometer.current);

                }

                if (UnityEngine.InputSystem.Accelerometer.current != null)
                    gyroInput = Mathf.Lerp(gyroInput, UnityEngine.InputSystem.Accelerometer.current.acceleration.ReadValue().x * CargoTruck_CC_Settings.Instance.gyroSensitivity, Time.deltaTime * 5f);

                brakeButton.transform.position = leftButton.transform.position;

                if (steeringWheel && steeringWheel.gameObject.activeInHierarchy)
                    steeringWheel.gameObject.SetActive(false);

                if (NOSButton && NOSButton.gameObject.activeInHierarchy != canUseNos)
                    NOSButton.gameObject.SetActive(canUseNos);

                if (joystick && joystick.gameObject.activeInHierarchy)
                    joystick.gameObject.SetActive(false);

                if (leftButton.gameObject.activeInHierarchy)
                    leftButton.gameObject.SetActive(false);

                if (rightButton.gameObject.activeInHierarchy)
                    rightButton.gameObject.SetActive(false);

                break;

            case CargoTruck_CC_Settings.MobileController.SteeringWheel:

                if (CargoTruck_CC_InputManager.gyroUsed) {

                    CargoTruck_CC_InputManager.gyroUsed = false;
                    InputSystem.DisableDevice(UnityEngine.InputSystem.Accelerometer.current);

                }

                gyroInput = 0f;

                if (steeringWheel && !steeringWheel.gameObject.activeInHierarchy) {
                    steeringWheel.gameObject.SetActive(true);
                    brakeButton.transform.position = orgBrakeButtonPos;
                }

                if (NOSButton && NOSButton.gameObject.activeInHierarchy)
                    NOSButton.gameObject.SetActive(false);

                if (NOSButtonSteeringWheel && NOSButtonSteeringWheel.gameObject.activeInHierarchy != canUseNos)
                    NOSButtonSteeringWheel.gameObject.SetActive(canUseNos);

                if (joystick && joystick.gameObject.activeInHierarchy)
                    joystick.gameObject.SetActive(false);

                if (leftButton.gameObject.activeInHierarchy)
                    leftButton.gameObject.SetActive(false);
                if (rightButton.gameObject.activeInHierarchy)
                    rightButton.gameObject.SetActive(false);

                break;

            case CargoTruck_CC_Settings.MobileController.Joystick:

                if (CargoTruck_CC_InputManager.gyroUsed) {

                    CargoTruck_CC_InputManager.gyroUsed = false;
                    InputSystem.DisableDevice(UnityEngine.InputSystem.Accelerometer.current);

                }

                gyroInput = 0f;

                if (steeringWheel && steeringWheel.gameObject.activeInHierarchy)
                    steeringWheel.gameObject.SetActive(false);

                if (NOSButton && NOSButton.gameObject.activeInHierarchy != canUseNos)
                    NOSButton.gameObject.SetActive(canUseNos);

                if (joystick && !joystick.gameObject.activeInHierarchy) {
                    joystick.gameObject.SetActive(true);
                    brakeButton.transform.position = orgBrakeButtonPos;
                }

                if (leftButton.gameObject.activeInHierarchy)
                    leftButton.gameObject.SetActive(false);

                if (rightButton.gameObject.activeInHierarchy)
                    rightButton.gameObject.SetActive(false);

                break;

        }

        throttleInput = GetInput(gasButton) + GetInput(gradualGasButton);
        brakeInput = GetInput(brakeButton);
        leftInput = GetInput(leftButton);
        rightInput = GetInput(rightButton);
        handbrakeInput = GetInput(handbrakeButton);
        boostInput = Mathf.Clamp((GetInput(NOSButton) + GetInput(NOSButtonSteeringWheel)), 0f, 1f);

        if (steeringWheel)
            steeringWheelInput = steeringWheel.input;

        if (joystick)
            joystickInput = joystick.inputHorizontal;

        FeedRCC();

    }

    private void FeedRCC() {

        if (!CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
            return;

        canUseNos = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.useNOS;

        mobileInputs.throttleInput = throttleInput;
        mobileInputs.brakeInput = brakeInput;
        mobileInputs.steerInput = -leftInput + rightInput + steeringWheelInput + gyroInput + joystickInput;
        mobileInputs.handbrakeInput = handbrakeInput;
        mobileInputs.boostInput = boostInput;

    }

    // Gets input from button.
    float GetInput(CargoTruck_CC_UIController button) {

        if (button == null)
            return 0f;

        return (button.input);

    }

    void OnDisable() {

        CargoTruck_CC_SceneManager.OnVehicleChanged -= CheckController;

    }

}