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
using System.Collections.Generic;
using UnityEngine.EventSystems;

///<summary>
/// Main Customization Class For RCC.
///</summary>
public class CargoTruck_CC_Customization : MonoBehaviour {

    /// <summary>
    /// Set Customization Mode. This will enable / disable controlling the vehicle, and enable / disable orbit camera mode.
    /// </summary>
    public static void SetCustomizationMode(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!vehicle) {

            Debug.LogError("Player vehicle is not selected for customization! Use CargoTruck_CC_Customization.SetCustomizationMode(playerVehicle, true/false); for enabling / disabling customization mode for player vehicle.");
            return;

        }

        CargoTruck_CC_Camera cam = CargoTruck_CC_SceneManager.Instance.activePlayerCamera;
        CargoTruck_CC_UIDashboardDisplay UI = CargoTruck_CC_SceneManager.Instance.activePlayerCanvas;

        if (state) {

            vehicle.SetCanControl(false);

            if (cam)
                cam.ChangeCamera(CargoTruck_CC_Camera.CameraMode.TPS);

            if (UI)
                UI.SetDisplayType(CargoTruck_CC_UIDashboardDisplay.DisplayType.Customization);

        } else {

            SetSmokeParticle(vehicle, false);
            SetExhaustFlame(vehicle, false);
            vehicle.SetCanControl(true);

            if (cam)
                cam.ChangeCamera(CargoTruck_CC_Camera.CameraMode.TPS);

            if (UI)
                UI.SetDisplayType(CargoTruck_CC_UIDashboardDisplay.DisplayType.Full);

        }

    }

    /// <summary>
    ///	 Updates RCC while vehicle is inactive.
    /// </summary>
    public static void OverrideRCC(CargoTruck_CC_CarControllerV3 vehicle) {

        if (!CheckVehicle(vehicle))
            return;

    }

    /// <summary>
    ///	 Enable / Disable Smoke Particles. You can use it for previewing current wheel smokes.
    /// </summary>
    public static void SetSmokeParticle(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.PreviewSmokeParticle(state);

    }

    /// <summary>
    /// Set Smoke Color.
    /// </summary>
    public static void SetSmokeColor(CargoTruck_CC_CarControllerV3 vehicle, int indexOfGroundMaterial, Color color) {

        if (!CheckVehicle(vehicle))
            return;

        CargoTruck_CC_WheelCollider[] wheels = vehicle.GetComponentsInChildren<CargoTruck_CC_WheelCollider>();

        foreach (CargoTruck_CC_WheelCollider wheel in wheels) {

            for (int i = 0; i < wheel.allWheelParticles.Count; i++) {

                ParticleSystem ps = wheel.allWheelParticles[i];
                ParticleSystem.MainModule psmain = ps.main;
                color.a = psmain.startColor.color.a;
                psmain.startColor = color;

            }

        }

    }

    /// <summary>
    /// Set Headlights Color.
    /// </summary>
    public static void SetHeadlightsColor(CargoTruck_CC_CarControllerV3 vehicle, Color color) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.lowBeamHeadLightsOn = true;
        CargoTruck_CC_Light[] lights = vehicle.GetComponentsInChildren<CargoTruck_CC_Light>();

        foreach (CargoTruck_CC_Light l in lights) {

            if (l.lightType == CargoTruck_CC_Light.LightType.HeadLight || l.lightType == CargoTruck_CC_Light.LightType.HighBeamHeadLight || l.lightType == CargoTruck_CC_Light.LightType.ParkLight)
                l.GetComponent<Light>().color = color;

        }

    }

    /// <summary>
    /// Enable / Disable Exhaust Flame Particles.
    /// </summary>
    public static void SetExhaustFlame(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        CargoTruck_CC_Exhaust[] exhausts = vehicle.GetComponentsInChildren<CargoTruck_CC_Exhaust>();

        foreach (CargoTruck_CC_Exhaust exhaust in exhausts)
            exhaust.previewFlames = state;

    }

    /// <summary>
    /// Set Front Wheel Cambers.
    /// </summary>
    public static void SetFrontCambers(CargoTruck_CC_CarControllerV3 vehicle, float camberAngle) {

        if (!CheckVehicle(vehicle))
            return;

        CargoTruck_CC_WheelCollider[] wc = vehicle.GetComponentsInChildren<CargoTruck_CC_WheelCollider>();

        foreach (CargoTruck_CC_WheelCollider w in wc) {

            if (w == vehicle.FrontLeftWheelCollider || w == vehicle.FrontRightWheelCollider)
                w.camber = camberAngle;

        }

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Rear Wheel Cambers.
    /// </summary>
    public static void SetRearCambers(CargoTruck_CC_CarControllerV3 vehicle, float camberAngle) {

        if (!CheckVehicle(vehicle))
            return;

        CargoTruck_CC_WheelCollider[] wc = vehicle.GetComponentsInChildren<CargoTruck_CC_WheelCollider>();

        foreach (CargoTruck_CC_WheelCollider w in wc) {

            if (w != vehicle.FrontLeftWheelCollider && w != vehicle.FrontRightWheelCollider)
                w.camber = camberAngle;

        }

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Change Wheel Models. You can find your wheel models array in Tools --> BCG --> RCC --> Configure Changable Wheels.
    /// </summary>
    public static void ChangeWheels(CargoTruck_CC_CarControllerV3 vehicle, GameObject wheel, bool applyRadius) {

        if (!CheckVehicle(vehicle))
            return;

        for (int i = 0; i < vehicle.allWheelColliders.Length; i++) {

            if (vehicle.allWheelColliders[i].wheelModel.GetComponent<MeshRenderer>())
                vehicle.allWheelColliders[i].wheelModel.GetComponent<MeshRenderer>().enabled = false;

            foreach (Transform t in vehicle.allWheelColliders[i].wheelModel.GetComponentInChildren<Transform>())
                t.gameObject.SetActive(false);

            GameObject newWheel = (GameObject)Instantiate(wheel, vehicle.allWheelColliders[i].wheelModel.position, vehicle.allWheelColliders[i].wheelModel.rotation, vehicle.allWheelColliders[i].wheelModel);

            if (vehicle.allWheelColliders[i].wheelModel.localPosition.x > 0f)
                newWheel.transform.localScale = new Vector3(newWheel.transform.localScale.x * -1f, newWheel.transform.localScale.y, newWheel.transform.localScale.z);

            if (applyRadius)
                vehicle.allWheelColliders[i].wheelCollider.radius = CargoTruck_CC_GetBounds.MaxBoundsExtent(wheel.transform);

        }

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Front Suspension targetPositions. It changes targetPosition of the front WheelColliders.
    /// </summary>
    public static void SetFrontSuspensionsTargetPos(CargoTruck_CC_CarControllerV3 vehicle, float targetPosition) {

        if (!CheckVehicle(vehicle))
            return;

        targetPosition = Mathf.Clamp01(targetPosition);

        JointSpring spring1 = vehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring;
        spring1.targetPosition = 1f - targetPosition;

        vehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring = spring1;

        JointSpring spring2 = vehicle.FrontRightWheelCollider.wheelCollider.suspensionSpring;
        spring2.targetPosition = 1f - targetPosition;

        vehicle.FrontRightWheelCollider.wheelCollider.suspensionSpring = spring2;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Rear Suspension targetPositions. It changes targetPosition of the rear WheelColliders.
    /// </summary>
    public static void SetRearSuspensionsTargetPos(CargoTruck_CC_CarControllerV3 vehicle, float targetPosition) {

        if (!CheckVehicle(vehicle))
            return;

        targetPosition = Mathf.Clamp01(targetPosition);

        JointSpring spring1 = vehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring;
        spring1.targetPosition = 1f - targetPosition;

        vehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring = spring1;

        JointSpring spring2 = vehicle.RearRightWheelCollider.wheelCollider.suspensionSpring;
        spring2.targetPosition = 1f - targetPosition;

        vehicle.RearRightWheelCollider.wheelCollider.suspensionSpring = spring2;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set All Suspension targetPositions. It changes targetPosition of the all WheelColliders.
    /// </summary>
    public static void SetAllSuspensionsTargetPos(CargoTruck_CC_CarControllerV3 vehicle, float targetPosition) {

        if (!CheckVehicle(vehicle))
            return;

        targetPosition = Mathf.Clamp01(targetPosition);

        JointSpring spring1 = vehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring;
        spring1.targetPosition = 1f - targetPosition;

        vehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring = spring1;

        JointSpring spring2 = vehicle.RearRightWheelCollider.wheelCollider.suspensionSpring;
        spring2.targetPosition = 1f - targetPosition;

        vehicle.RearRightWheelCollider.wheelCollider.suspensionSpring = spring2;

        JointSpring spring3 = vehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring;
        spring3.targetPosition = 1f - targetPosition;

        vehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring = spring3;

        JointSpring spring4 = vehicle.FrontRightWheelCollider.wheelCollider.suspensionSpring;
        spring4.targetPosition = 1f - targetPosition;

        vehicle.FrontRightWheelCollider.wheelCollider.suspensionSpring = spring4;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Front Suspension Distances.
    /// </summary>
    public static void SetFrontSuspensionsDistances(CargoTruck_CC_CarControllerV3 vehicle, float distance) {

        if (!CheckVehicle(vehicle))
            return;

        if (distance <= 0)
            distance = .05f;

        vehicle.FrontLeftWheelCollider.wheelCollider.suspensionDistance = distance;
        vehicle.FrontRightWheelCollider.wheelCollider.suspensionDistance = distance;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Rear Suspension Distances.
    /// </summary>
    public static void SetRearSuspensionsDistances(CargoTruck_CC_CarControllerV3 vehicle, float distance) {

        if (!CheckVehicle(vehicle))
            return;

        if (distance <= 0)
            distance = .05f;

        vehicle.RearLeftWheelCollider.wheelCollider.suspensionDistance = distance;
        vehicle.RearRightWheelCollider.wheelCollider.suspensionDistance = distance;

        if (vehicle.ExtraRearWheelsCollider != null && vehicle.ExtraRearWheelsCollider.Length > 0) {

            foreach (CargoTruck_CC_WheelCollider wc in vehicle.ExtraRearWheelsCollider)
                wc.wheelCollider.suspensionDistance = distance;

        }

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Drivetrain Mode.
    /// </summary>
    public static void SetDrivetrainMode(CargoTruck_CC_CarControllerV3 vehicle, CargoTruck_CC_CarControllerV3.WheelType mode) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.wheelTypeChoise = mode;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Gear Shifting Threshold. Automatic gear will shift up at earlier rpm on lower values. Automatic gear will shift up at later rpm on higher values. 
    /// </summary>
    public static void SetGearShiftingThreshold(CargoTruck_CC_CarControllerV3 vehicle, float targetValue) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.gearShiftingThreshold = targetValue;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Clutch Threshold. Automatic gear will shift up at earlier rpm on lower values. Automatic gear will shift up at later rpm on higher values. 
    /// </summary>
    public static void SetClutchThreshold(CargoTruck_CC_CarControllerV3 vehicle, float targetValue) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.clutchInertia = targetValue;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Enable / Disable Counter Steering while vehicle is drifting. Useful for avoid spinning.
    /// </summary>
    public static void SetCounterSteering(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.useCounterSteering = state;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Enable / Disable Steering Limiter while vehicle is drifting. Useful for avoid spinning.
    /// </summary>
    public static void SetSteeringLimit(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.useSteeringLimiter = state;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Enable / Disable NOS.
    /// </summary>
    public static void SetNOS(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.useNOS = state;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Enable / Disable Turbo.
    /// </summary>
    public static void SetTurbo(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.useTurbo = state;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Enable / Disable Exhaust Flames.
    /// </summary>
    public static void SetUseExhaustFlame(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.useExhaustFlame = state;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Enable / Disable Rev Limiter.
    /// </summary>
    public static void SetRevLimiter(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.useRevLimiter = state;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Front Suspension Spring Force.
    /// </summary>
    public static void SetFrontSuspensionsSpringForce(CargoTruck_CC_CarControllerV3 vehicle, float targetValue) {

        if (!CheckVehicle(vehicle))
            return;

        JointSpring spring = vehicle.FrontLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring;
        spring.spring = targetValue;
        vehicle.FrontLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring = spring;
        vehicle.FrontRightWheelCollider.GetComponent<WheelCollider>().suspensionSpring = spring;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Rear Suspension Spring Force.
    /// </summary>
    public static void SetRearSuspensionsSpringForce(CargoTruck_CC_CarControllerV3 vehicle, float targetValue) {

        if (!CheckVehicle(vehicle))
            return;

        JointSpring spring = vehicle.RearLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring;
        spring.spring = targetValue;
        vehicle.RearLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring = spring;
        vehicle.RearRightWheelCollider.GetComponent<WheelCollider>().suspensionSpring = spring;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Front Suspension Spring Damper.
    /// </summary>
    public static void SetFrontSuspensionsSpringDamper(CargoTruck_CC_CarControllerV3 vehicle, float targetValue) {

        if (!CheckVehicle(vehicle))
            return;

        JointSpring spring = vehicle.FrontLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring;
        spring.damper = targetValue;
        vehicle.FrontLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring = spring;
        vehicle.FrontRightWheelCollider.GetComponent<WheelCollider>().suspensionSpring = spring;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Rear Suspension Spring Damper.
    /// </summary>
    public static void SetRearSuspensionsSpringDamper(CargoTruck_CC_CarControllerV3 vehicle, float targetValue) {

        if (!CheckVehicle(vehicle))
            return;

        JointSpring spring = vehicle.RearLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring;
        spring.damper = targetValue;
        vehicle.RearLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring = spring;
        vehicle.RearRightWheelCollider.GetComponent<WheelCollider>().suspensionSpring = spring;

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Maximum Speed of the vehicle.
    /// </summary>
    public static void SetMaximumSpeed(CargoTruck_CC_CarControllerV3 vehicle, float targetValue) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.maxspeed = Mathf.Clamp(targetValue, 10f, 400f);

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Maximum Engine Torque of the vehicle.
    /// </summary>
    public static void SetMaximumTorque(CargoTruck_CC_CarControllerV3 vehicle, float targetValue) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.maxEngineTorque = Mathf.Clamp(targetValue, 50f, 50000f);

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Set Maximum Brake of the vehicle.
    /// </summary>
    public static void SetMaximumBrake(CargoTruck_CC_CarControllerV3 vehicle, float targetValue) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.brakeTorque = Mathf.Clamp(targetValue, 0f, 50000f);

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Repair vehicle.
    /// </summary>
    public static void Repair(CargoTruck_CC_CarControllerV3 vehicle) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.Repair();

    }

    /// <summary>
    /// Enable / Disable ESP.
    /// </summary>
    public static void SetESP(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.ESP = state;

    }

    /// <summary>
    /// Enable / Disable ABS.
    /// </summary>
    public static void SetABS(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.ABS = state;

    }

    /// <summary>
    /// Enable / Disable TCS.
    /// </summary>
    public static void SetTCS(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.TCS = state;

    }

    /// <summary>
    /// Enable / Disable Steering Helper.
    /// </summary>
    public static void SetSH(CargoTruck_CC_CarControllerV3 vehicle, bool state) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.steeringHelper = state;

    }

    /// <summary>
    /// Set Steering Helper strength.
    /// </summary>
    public static void SetSHStrength(CargoTruck_CC_CarControllerV3 vehicle, float value) {

        if (!CheckVehicle(vehicle))
            return;

        vehicle.steeringHelper = true;
        vehicle.steerHelperLinearVelStrength = value;
        vehicle.steerHelperAngularVelStrength = value;

    }

    /// <summary>
    /// Set Transmission of the vehicle.
    /// </summary>
    public static void SetTransmission(bool automatic) {

        CargoTruck_CC_Settings.Instance.useAutomaticGear = automatic;

    }

    /// <summary>
    /// Save all stats with PlayerPrefs.
    /// </summary>
    public static void SaveStats(CargoTruck_CC_CarControllerV3 vehicle) {

        if (!CheckVehicle(vehicle))
            return;

        PlayerPrefs.SetFloat(vehicle.transform.name + "_FrontCamber", vehicle.FrontLeftWheelCollider.camber);
        PlayerPrefs.SetFloat(vehicle.transform.name + "_RearCamber", vehicle.RearLeftWheelCollider.camber);

        PlayerPrefs.SetFloat(vehicle.transform.name + "_FrontSuspensionsDistance", vehicle.FrontLeftWheelCollider.wheelCollider.suspensionDistance);
        PlayerPrefs.SetFloat(vehicle.transform.name + "_RearSuspensionsDistance", vehicle.RearLeftWheelCollider.wheelCollider.suspensionDistance);

        PlayerPrefs.SetFloat(vehicle.transform.name + "_FrontSuspensionsSpring", vehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring.spring);
        PlayerPrefs.SetFloat(vehicle.transform.name + "_RearSuspensionsSpring", vehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring.spring);

        PlayerPrefs.SetFloat(vehicle.transform.name + "_FrontSuspensionsDamper", vehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring.damper);
        PlayerPrefs.SetFloat(vehicle.transform.name + "_RearSuspensionsDamper", vehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring.damper);

        PlayerPrefs.SetFloat(vehicle.transform.name + "_MaximumSpeed", vehicle.maxspeed);
        PlayerPrefs.SetFloat(vehicle.transform.name + "_MaximumBrake", vehicle.brakeTorque);
        PlayerPrefs.SetFloat(vehicle.transform.name + "_MaximumTorque", vehicle.maxEngineTorque);

        PlayerPrefs.SetString(vehicle.transform.name + "_DrivetrainMode", vehicle.wheelTypeChoise.ToString());

        PlayerPrefs.SetFloat(vehicle.transform.name + "_GearShiftingThreshold", vehicle.gearShiftingThreshold);
        PlayerPrefs.SetFloat(vehicle.transform.name + "_ClutchingThreshold", vehicle.clutchInertia);

        CargoTruck_CC_PlayerPrefsX.SetBool(vehicle.transform.name + "_CounterSteering", vehicle.useCounterSteering);

        foreach (CargoTruck_CC_Light _light in vehicle.GetComponentsInChildren<CargoTruck_CC_Light>()) {

            if (_light.lightType == CargoTruck_CC_Light.LightType.HeadLight) {

                CargoTruck_CC_PlayerPrefsX.SetColor(vehicle.transform.name + "_HeadlightsColor", _light.GetComponentInChildren<Light>().color);
                break;

            }

        }

        ParticleSystem ps = vehicle.RearLeftWheelCollider.allWheelParticles[0];
        ParticleSystem.MainModule psmain = ps.main;

        CargoTruck_CC_PlayerPrefsX.SetColor(vehicle.transform.name + "_WheelsSmokeColor", psmain.startColor.color);

        CargoTruck_CC_PlayerPrefsX.SetBool(vehicle.transform.name + "_ABS", vehicle.ABS);
        CargoTruck_CC_PlayerPrefsX.SetBool(vehicle.transform.name + "_ESP", vehicle.ESP);
        CargoTruck_CC_PlayerPrefsX.SetBool(vehicle.transform.name + "_TCS", vehicle.TCS);
        CargoTruck_CC_PlayerPrefsX.SetBool(vehicle.transform.name + "_SH", vehicle.steeringHelper);

        CargoTruck_CC_PlayerPrefsX.SetBool(vehicle.transform.name + "NOS", vehicle.useNOS);
        CargoTruck_CC_PlayerPrefsX.SetBool(vehicle.transform.name + "Turbo", vehicle.useTurbo);
        CargoTruck_CC_PlayerPrefsX.SetBool(vehicle.transform.name + "ExhaustFlame", vehicle.useExhaustFlame);
        CargoTruck_CC_PlayerPrefsX.SetBool(vehicle.transform.name + "RevLimiter", vehicle.useRevLimiter);

    }

    /// <summary>
    /// Load all stats with PlayerPrefs.
    /// </summary>
    public static void LoadStats(CargoTruck_CC_CarControllerV3 vehicle) {

        if (!CheckVehicle(vehicle))
            return;

        SetFrontCambers(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_FrontCamber", vehicle.FrontLeftWheelCollider.camber));
        SetRearCambers(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_RearCamber", vehicle.RearLeftWheelCollider.camber));

        SetFrontSuspensionsDistances(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_FrontSuspensionsDistance", vehicle.FrontLeftWheelCollider.wheelCollider.suspensionDistance));
        SetRearSuspensionsDistances(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_RearSuspensionsDistance", vehicle.RearLeftWheelCollider.wheelCollider.suspensionDistance));

        SetFrontSuspensionsSpringForce(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_FrontSuspensionsSpring", vehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring.spring));
        SetRearSuspensionsSpringForce(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_RearSuspensionsSpring", vehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring.spring));

        SetFrontSuspensionsSpringDamper(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_FrontSuspensionsDamper", vehicle.FrontLeftWheelCollider.wheelCollider.suspensionSpring.damper));
        SetRearSuspensionsSpringDamper(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_RearSuspensionsDamper", vehicle.RearLeftWheelCollider.wheelCollider.suspensionSpring.damper));

        SetMaximumSpeed(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_MaximumSpeed", vehicle.maxspeed));
        SetMaximumBrake(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_MaximumBrake", vehicle.brakeTorque));
        SetMaximumTorque(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_MaximumTorque", vehicle.maxEngineTorque));

        string drvtrn = PlayerPrefs.GetString(vehicle.transform.name + "_DrivetrainMode", vehicle.wheelTypeChoise.ToString());

        switch (drvtrn) {

            case "FWD":
                vehicle.wheelTypeChoise = CargoTruck_CC_CarControllerV3.WheelType.FWD;
                break;

            case "RWD":
                vehicle.wheelTypeChoise = CargoTruck_CC_CarControllerV3.WheelType.RWD;
                break;

            case "AWD":
                vehicle.wheelTypeChoise = CargoTruck_CC_CarControllerV3.WheelType.AWD;
                break;

        }

        SetGearShiftingThreshold(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_GearShiftingThreshold", vehicle.gearShiftingThreshold));
        SetClutchThreshold(vehicle, PlayerPrefs.GetFloat(vehicle.transform.name + "_ClutchingThreshold", vehicle.clutchInertia));

        SetCounterSteering(vehicle, CargoTruck_CC_PlayerPrefsX.GetBool(vehicle.transform.name + "_CounterSteering", vehicle.useCounterSteering));

        SetABS(vehicle, CargoTruck_CC_PlayerPrefsX.GetBool(vehicle.transform.name + "_ABS", vehicle.ABS));
        SetESP(vehicle, CargoTruck_CC_PlayerPrefsX.GetBool(vehicle.transform.name + "_ESP", vehicle.ESP));
        SetTCS(vehicle, CargoTruck_CC_PlayerPrefsX.GetBool(vehicle.transform.name + "_TCS", vehicle.TCS));
        SetSH(vehicle, CargoTruck_CC_PlayerPrefsX.GetBool(vehicle.transform.name + "_SH", vehicle.steeringHelper));

        SetNOS(vehicle, CargoTruck_CC_PlayerPrefsX.GetBool(vehicle.transform.name + "NOS", vehicle.useNOS));
        SetTurbo(vehicle, CargoTruck_CC_PlayerPrefsX.GetBool(vehicle.transform.name + "Turbo", vehicle.useTurbo));
        SetUseExhaustFlame(vehicle, CargoTruck_CC_PlayerPrefsX.GetBool(vehicle.transform.name + "ExhaustFlame", vehicle.useExhaustFlame));
        SetRevLimiter(vehicle, CargoTruck_CC_PlayerPrefsX.GetBool(vehicle.transform.name + "RevLimiter", vehicle.useRevLimiter));

        if (PlayerPrefs.HasKey(vehicle.transform.name + "_WheelsSmokeColor"))
            SetSmokeColor(vehicle, 0, CargoTruck_CC_PlayerPrefsX.GetColor(vehicle.transform.name + "_WheelsSmokeColor"));

        if (PlayerPrefs.HasKey(vehicle.transform.name + "_HeadlightsColor"))
            SetHeadlightsColor(vehicle, CargoTruck_CC_PlayerPrefsX.GetColor(vehicle.transform.name + "_HeadlightsColor"));

        OverrideRCC(vehicle);

    }

    /// <summary>
    /// Resets all stats and saves default values with PlayerPrefs.
    /// </summary>
    public static void ResetStats(CargoTruck_CC_CarControllerV3 vehicle, CargoTruck_CC_CarControllerV3 defaultCar) {

        if (!CheckVehicle(vehicle))
            return;

        if (!CheckVehicle(defaultCar))
            return;

        SetFrontCambers(vehicle, defaultCar.FrontLeftWheelCollider.camber);
        SetRearCambers(vehicle, defaultCar.RearLeftWheelCollider.camber);

        SetFrontSuspensionsDistances(vehicle, defaultCar.FrontLeftWheelCollider.wheelCollider.suspensionDistance);
        SetRearSuspensionsDistances(vehicle, defaultCar.RearLeftWheelCollider.wheelCollider.suspensionDistance);

        SetFrontSuspensionsSpringForce(vehicle, defaultCar.FrontLeftWheelCollider.wheelCollider.suspensionSpring.spring);
        SetRearSuspensionsSpringForce(vehicle, defaultCar.RearLeftWheelCollider.wheelCollider.suspensionSpring.spring);

        SetFrontSuspensionsSpringDamper(vehicle, defaultCar.FrontLeftWheelCollider.wheelCollider.suspensionSpring.damper);
        SetRearSuspensionsSpringDamper(vehicle, defaultCar.RearLeftWheelCollider.wheelCollider.suspensionSpring.damper);

        SetMaximumSpeed(vehicle, defaultCar.maxspeed);
        SetMaximumBrake(vehicle, defaultCar.brakeTorque);
        SetMaximumTorque(vehicle, defaultCar.maxEngineTorque);

        string drvtrn = defaultCar.wheelTypeChoise.ToString();

        switch (drvtrn) {

            case "FWD":
                vehicle.wheelTypeChoise = CargoTruck_CC_CarControllerV3.WheelType.FWD;
                break;

            case "RWD":
                vehicle.wheelTypeChoise = CargoTruck_CC_CarControllerV3.WheelType.RWD;
                break;

            case "AWD":
                vehicle.wheelTypeChoise = CargoTruck_CC_CarControllerV3.WheelType.AWD;
                break;

        }

        SetGearShiftingThreshold(vehicle, defaultCar.gearShiftingThreshold);
        SetClutchThreshold(vehicle, defaultCar.clutchInertia);

        SetCounterSteering(vehicle, defaultCar.useCounterSteering);

        SetABS(vehicle, defaultCar.ABS);
        SetESP(vehicle, defaultCar.ESP);
        SetTCS(vehicle, defaultCar.TCS);
        SetSH(vehicle, defaultCar.steeringHelper);

        SetNOS(vehicle, defaultCar.useNOS);
        SetTurbo(vehicle, defaultCar.useTurbo);
        SetUseExhaustFlame(vehicle, defaultCar.useExhaustFlame);
        SetRevLimiter(vehicle, defaultCar.useRevLimiter);

        SetSmokeColor(vehicle, 0, Color.white);
        SetHeadlightsColor(vehicle, Color.white);

        SaveStats(vehicle);

        OverrideRCC(vehicle);

    }

    public static bool CheckVehicle(CargoTruck_CC_CarControllerV3 vehicle) {

        if (!vehicle) {

            Debug.LogError("Vehicle is missing!");
            return false;

        }

        return true;

    }

}
