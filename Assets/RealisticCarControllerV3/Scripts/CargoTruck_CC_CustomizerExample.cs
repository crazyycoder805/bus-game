//----------------------------------------------
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
using UnityEngine.EventSystems;

/// <summary>
/// A simple customizer example script used for receiving methods from UI elements and send them to CargoTruck_CC_Customization script. Also updates all UI elements for new spawned vehicles too.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC Customizer Example")]
public class CargoTruck_CC_CustomizerExample : CargoTruck_CC_Singleton<CargoTruck_CC_CustomizerExample> {

    [Header("UI Menus")]
    public GameObject wheelsMenu;
    public GameObject configurationMenu;
    public GameObject steeringAssistancesMenu;
    public GameObject colorsMenu;

    [Header("UI Sliders")]
    public Slider frontCamber;
    public Slider rearCamber;
    public Slider frontSuspensionDistances;
    public Slider rearSuspensionDistances;
    public Slider frontSuspensionDampers;
    public Slider rearSuspensionDampers;
    public Slider frontSuspensionSprings;
    public Slider rearSuspensionSprings;
    public Slider gearShiftingThreshold;
    public Slider clutchThreshold;

    [Header("UI Toggles")]
    public Toggle TCS;
    public Toggle ABS;
    public Toggle ESP;
    public Toggle SH;
    public Toggle counterSteering;
    public Toggle steeringSensitivity;
    public Toggle NOS;
    public Toggle turbo;
    public Toggle exhaustFlame;
    public Toggle revLimiter;
    public Toggle transmission;

    [Header("UI InputFields")]
    public InputField maxSpeed;
    public InputField maxBrake;
    public InputField maxTorque;

    [Header("UI Dropdown Menus")]
    public Dropdown drivetrainMode;

    void Start() {

        CheckUIs();

    }

    public void CheckUIs() {

        if (!CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
            return;

        frontCamber.value = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.FrontLeftWheelCollider.camber;
        rearCamber.value = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.RearLeftWheelCollider.camber;
        frontSuspensionDistances.value = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.FrontLeftWheelCollider.wheelCollider.suspensionDistance;
        rearSuspensionDistances.value = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.RearLeftWheelCollider.wheelCollider.suspensionDistance;
        frontSuspensionDampers.value = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring.damper;
        rearSuspensionDampers.value = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring.damper;
        frontSuspensionSprings.value = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring.spring;
        rearSuspensionSprings.value = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring.spring;
        gearShiftingThreshold.value = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.gearShiftingThreshold;
        clutchThreshold.value = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.clutchInertia;

        TCS.isOn = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.TCS;
        ABS.isOn = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.ABS;
        ESP.isOn = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.ESP;
        SH.isOn = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.steeringHelper;
        counterSteering.isOn = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.useCounterSteering;
        NOS.isOn = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.useNOS;
        turbo.isOn = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.useTurbo;
        exhaustFlame.isOn = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.useExhaustFlame;
        revLimiter.isOn = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.useRevLimiter;
        transmission.isOn = CargoTruck_CC_Settings.Instance.useAutomaticGear;

        maxSpeed.text = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.maxspeed.ToString();
        maxBrake.text = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.brakeTorque.ToString();
        maxTorque.text = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.maxEngineTorque.ToString();

        switch (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.wheelTypeChoise) {

            case CargoTruck_CC_CarControllerV3.WheelType.FWD:
                drivetrainMode.value = 0;
                break;

            case CargoTruck_CC_CarControllerV3.WheelType.RWD:
                drivetrainMode.value = 1;
                break;

            case CargoTruck_CC_CarControllerV3.WheelType.AWD:
                drivetrainMode.value = 2;
                break;

            case CargoTruck_CC_CarControllerV3.WheelType.BIASED:
                drivetrainMode.value = 3;
                break;

        }

    }

    public void OpenMenu(GameObject activeMenu) {

        if (activeMenu.activeInHierarchy) {

            activeMenu.SetActive(false);
            return;

        }

        wheelsMenu.SetActive(false);
        configurationMenu.SetActive(false);
        steeringAssistancesMenu.SetActive(false);
        colorsMenu.SetActive(false);

        activeMenu.SetActive(true);

    }

    public void CloseAllMenus() {

        wheelsMenu.SetActive(false);
        configurationMenu.SetActive(false);
        steeringAssistancesMenu.SetActive(false);
        colorsMenu.SetActive(false);

    }

    public void SetCustomizationMode(bool state) {

        if (!CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
            return;

        CargoTruck_CC_Customization.SetCustomizationMode(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, state);

        if (state)
            CheckUIs();

    }

    public void SetFrontCambersBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetFrontCambers(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetRearCambersBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetRearCambers(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void TogglePreviewSmokeByToggle(Toggle toggle) {

        CargoTruck_CC_Customization.SetSmokeParticle(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void TogglePreviewExhaustFlameByToggle(Toggle toggle) {

        CargoTruck_CC_Customization.SetExhaustFlame(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetSmokeColorByColorPicker(CargoTruck_CC_ColorPickerBySliders color) {

        CargoTruck_CC_Customization.SetSmokeColor(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, 0, color.color);

    }

    public void SetHeadlightColorByColorPicker(CargoTruck_CC_ColorPickerBySliders color) {

        CargoTruck_CC_Customization.SetHeadlightsColor(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, color.color);

    }

    public void ChangeWheelsBySlider(Slider slider) {

        CargoTruck_CC_Customization.ChangeWheels(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, CargoTruck_CC_ChangableWheels.Instance.wheels[(int)slider.value].wheel, true);

    }

    public void SetFrontSuspensionTargetsBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetFrontSuspensionsTargetPos(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetRearSuspensionTargetsBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetRearSuspensionsTargetPos(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetAllSuspensionTargetsByButton(float strength) {

        CargoTruck_CC_Customization.SetAllSuspensionsTargetPos(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, strength);

    }

    public void SetFrontSuspensionDistancesBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetFrontSuspensionsDistances(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetRearSuspensionDistancesBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetRearSuspensionsDistances(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetGearShiftingThresholdBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetGearShiftingThreshold(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, .5f, .95f));

    }

    public void SetClutchThresholdBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetClutchThreshold(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, .1f, .9f));

    }

    public void SetDriveTrainModeByDropdown(Dropdown dropdown) {

        switch (dropdown.value) {

            case 0:
                CargoTruck_CC_Customization.SetDrivetrainMode(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, CargoTruck_CC_CarControllerV3.WheelType.FWD);
                break;

            case 1:
                CargoTruck_CC_Customization.SetDrivetrainMode(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, CargoTruck_CC_CarControllerV3.WheelType.RWD);
                break;

            case 2:
                CargoTruck_CC_Customization.SetDrivetrainMode(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, CargoTruck_CC_CarControllerV3.WheelType.AWD);
                break;

        }

    }

    public void SetCounterSteeringByToggle(Toggle toggle) {

        CargoTruck_CC_Customization.SetCounterSteering(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetSteeringLimitByToggle(Toggle toggle) {

        CargoTruck_CC_Customization.SetSteeringLimit(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetNOSByToggle(Toggle toggle) {

        CargoTruck_CC_Customization.SetNOS(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetTurboByToggle(Toggle toggle) {

        CargoTruck_CC_Customization.SetTurbo(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetExhaustFlameByToggle(Toggle toggle) {

        CargoTruck_CC_Customization.SetUseExhaustFlame(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetRevLimiterByToggle(Toggle toggle) {

        CargoTruck_CC_Customization.SetRevLimiter(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetFrontSuspensionsSpringForceBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetFrontSuspensionsSpringForce(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, 10000f, 100000f));

    }

    public void SetRearSuspensionsSpringForceBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetRearSuspensionsSpringForce(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, 10000f, 100000f));

    }

    public void SetFrontSuspensionsSpringDamperBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetFrontSuspensionsSpringDamper(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, 1000f, 10000f));

    }

    public void SetRearSuspensionsSpringDamperBySlider(Slider slider) {

        CargoTruck_CC_Customization.SetRearSuspensionsSpringDamper(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, Mathf.Clamp(slider.value, 1000f, 10000f));

    }

    public void SetMaximumSpeedByInputField(InputField inputField) {

        CargoTruck_CC_Customization.SetMaximumSpeed(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, StringToFloat(inputField.text, 200f));
        inputField.text = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.maxspeed.ToString();

    }

    public void SetMaximumTorqueByInputField(InputField inputField) {

        CargoTruck_CC_Customization.SetMaximumTorque(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, StringToFloat(inputField.text, 2000f));
        inputField.text = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.maxEngineTorque.ToString();

    }

    public void SetMaximumBrakeByInputField(InputField inputField) {

        CargoTruck_CC_Customization.SetMaximumBrake(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, StringToFloat(inputField.text, 2000f));
        inputField.text = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.brakeTorque.ToString();

    }

    public void RepairCar() {

        CargoTruck_CC_Customization.Repair(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle);

    }

    public void SetESP(Toggle toggle) {

        CargoTruck_CC_Customization.SetESP(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetABS(Toggle toggle) {

        CargoTruck_CC_Customization.SetABS(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetTCS(Toggle toggle) {

        CargoTruck_CC_Customization.SetTCS(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetSH(Toggle toggle) {

        CargoTruck_CC_Customization.SetSH(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, toggle.isOn);

    }

    public void SetSHStrength(Slider slider) {

        CargoTruck_CC_Customization.SetSHStrength(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, slider.value);

    }

    public void SetTransmission(Toggle toggle) {

        CargoTruck_CC_Customization.SetTransmission(toggle.isOn);

    }

    public void SaveStats() {

        CargoTruck_CC_Customization.SaveStats(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle);

    }

    public void LoadStats() {

        CargoTruck_CC_Customization.LoadStats(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle);
        CheckUIs();

    }

    public void ResetStats() {

        int selectedVehicleIndex = 0;

        if (GameObject.FindObjectOfType<CargoTruck_CC_Demo>())
            selectedVehicleIndex = GameObject.FindObjectOfType<CargoTruck_CC_Demo>().selectedVehicleIndex;

        CargoTruck_CC_Customization.ResetStats(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle, CargoTruck_CC_DemoVehicles.Instance.vehicles[selectedVehicleIndex]);

        CheckUIs();

    }

    private float StringToFloat(string stringValue, float defaultValue) {

        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;

    }

}
