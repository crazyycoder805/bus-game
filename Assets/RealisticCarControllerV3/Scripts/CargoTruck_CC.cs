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

///<summary>
/// API for instantiating, registering new RCC vehicles, and changes at runtime.
///</summary>
public class CargoTruck_CC
{

    ///<summary>
    /// Spawn a RCC vehicle prefab with given position, rotation, sets its controllable, and engine state.
    ///</summary>
    public static CargoTruck_CC_CarControllerV3 SpawnRCC(CargoTruck_CC_CarControllerV3 vehiclePrefab, Vector3 position, Quaternion rotation, bool registerAsPlayerVehicle, bool isControllable, bool isEngineRunning) {

        CargoTruck_CC_CarControllerV3 spawnedRCC = (CargoTruck_CC_CarControllerV3)GameObject.Instantiate(vehiclePrefab, position, rotation);
        spawnedRCC.gameObject.SetActive(true);
        spawnedRCC.SetCanControl(isControllable);

        if (registerAsPlayerVehicle)
            CargoTruck_CC_SceneManager.Instance.RegisterPlayer(spawnedRCC);

        if (isEngineRunning)
            spawnedRCC.StartEngine(true);
        else
            spawnedRCC.KillEngine();

        return spawnedRCC;

    }

    ///<summary>
    /// Registers the vehicle as player vehicle. 
    ///</summary>
    public static void RegisterPlayerVehicle(CargoTruck_CC_CarControllerV3 vehicle) {

        CargoTruck_CC_SceneManager.Instance.RegisterPlayer(vehicle);

    }

    ///<summary>
    /// Registers the vehicle as player vehicle with controllable state. 
    ///</summary>
    public static void RegisterPlayerVehicle(CargoTruck_CC_CarControllerV3 vehicle, bool isControllable) {

        CargoTruck_CC_SceneManager.Instance.RegisterPlayer(vehicle, isControllable);

    }

    ///<summary>
    /// Registers the vehicle as player vehicle with controllable and engine state. 
    ///</summary>
    public static void RegisterPlayerVehicle(CargoTruck_CC_CarControllerV3 vehicle, bool isControllable, bool engineState) {

        CargoTruck_CC_SceneManager.Instance.RegisterPlayer(vehicle, isControllable, engineState);

    }

    ///<summary>
    /// De-Registers the player vehicle. 
    ///</summary>
    public static void DeRegisterPlayerVehicle() {

        CargoTruck_CC_SceneManager.Instance.DeRegisterPlayer();

    }

    ///<summary>
    /// Sets controllable state of the vehicle.
    ///</summary>
    public static void SetControl(CargoTruck_CC_CarControllerV3 vehicle, bool isControllable) {

        vehicle.SetCanControl(isControllable);

    }

    ///<summary>
    /// Sets engine state of the vehicle.
    ///</summary>
    public static void SetEngine(CargoTruck_CC_CarControllerV3 vehicle, bool engineState) {

        if (engineState)
            vehicle.StartEngine();
        else
            vehicle.KillEngine();

    }

    ///<summary>
    /// Sets the mobile controller type.
    ///</summary>
    public static void SetMobileController(CargoTruck_CC_Settings.MobileController mobileController) {

        CargoTruck_CC_Settings.Instance.mobileController = mobileController;
        Debug.Log("Mobile Controller has been changed to " + mobileController.ToString());

    }

    ///<summary>
    /// Sets the units.
    ///</summary>
    public static void SetUnits() { }

    ///<summary>
    /// Sets the Automatic Gear.
    ///</summary>
    public static void SetAutomaticGear() { }

    ///<summary>
    /// Starts / stops to record the player vehicle.
    ///</summary>
    public static void StartStopRecord() {

        CargoTruck_CC_SceneManager.Instance.Record();

    }

    ///<summary>
    /// Start / stops replay of the last record.
    ///</summary>
    public static void StartStopReplay() {

        CargoTruck_CC_SceneManager.Instance.Play();

    }

    ///<summary>
    /// Stops record / replay of the last record.
    ///</summary>
    public static void StopRecordReplay() {

        CargoTruck_CC_SceneManager.Instance.Stop();

    }

    ///<summary>
    /// Sets new behavior.
    ///</summary>
    public static void SetBehavior(int behaviorIndex) {

        CargoTruck_CC_SceneManager.SetBehavior(behaviorIndex);
        Debug.Log("Behavior has been changed to " + behaviorIndex.ToString());

    }

    /// <summary>
    /// Changes the camera mode.
    /// </summary>
    public static void ChangeCamera() {

        CargoTruck_CC_SceneManager.Instance.ChangeCamera();

    }

    /// <summary>
    /// Transport player vehicle the specified position and rotation.
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="rotation">Rotation.</param>
    public static void Transport(Vector3 position, Quaternion rotation) {

        CargoTruck_CC_SceneManager.Instance.Transport(position, rotation);

    }

    /// <summary>
    /// Transport the target vehicle to specified position and rotation.
    /// </summary>
    /// <param name="vehicle"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    public static void Transport(CargoTruck_CC_CarControllerV3 vehicle, Vector3 position, Quaternion rotation) {

        CargoTruck_CC_SceneManager.Instance.Transport(vehicle, position, rotation);

    }

    /// <summary>
    /// Cleans all skidmarks on the current scene.
    /// </summary>
    public static void CleanSkidmarks() {

        CargoTruck_CC_SkidmarksManager.Instance.CleanSkidmarks();

    }

    /// <summary>
    /// Cleans target skidmarks on the current scene.
    /// </summary>
    public static void CleanSkidmarks(int index) {

        CargoTruck_CC_SkidmarksManager.Instance.CleanSkidmarks(index);

    }

}
