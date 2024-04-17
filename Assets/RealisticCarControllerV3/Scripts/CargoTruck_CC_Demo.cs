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
using UnityEngine.SceneManagement;

/// <summary>
/// A simple manager script for all demo scenes. It has an array of spawnable player vehicles, public methods, setting new behavior modes, restart, and quit application.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC Demo Manager")]
public class CargoTruck_CC_Demo : MonoBehaviour {

    internal int selectedVehicleIndex = 0;      // An integer index value used for spawning a new vehicle.
    internal int selectedBehaviorIndex = 0;     // An integer index value used for setting behavior mode.

    // An integer index value used for spawning a new vehicle.
    public void SelectVehicle(int index) {

        selectedVehicleIndex = index;

    }

    public void Spawn() {

        // Last known position and rotation of last active vehicle.
        Vector3 lastKnownPos = new Vector3();
        Quaternion lastKnownRot = new Quaternion();

        // Checking if there is a player vehicle on the scene.
        if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle) {

            lastKnownPos = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.transform.position;
            lastKnownRot = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.transform.rotation;

        }

        // If last known position and rotation is not assigned, camera's position and rotation will be used.
        if (lastKnownPos == Vector3.zero) {

            if (CargoTruck_CC_SceneManager.Instance.activePlayerCamera) {

                lastKnownPos = CargoTruck_CC_SceneManager.Instance.activePlayerCamera.transform.position;
                lastKnownRot = CargoTruck_CC_SceneManager.Instance.activePlayerCamera.transform.rotation;

            }

        }

        // We don't need X and Z rotation angle. Just Y.
        lastKnownRot.x = 0f;
        lastKnownRot.z = 0f;

        CargoTruck_CC_CarControllerV3 lastVehicle = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle;

#if BCG_ENTEREXIT

		BCG_EnterExitVehicle lastEnterExitVehicle;
		bool enterExitVehicleFound = false;

		if (lastVehicle) {

			lastEnterExitVehicle = lastVehicle.GetComponentInChildren<BCG_EnterExitVehicle> ();

			if(lastEnterExitVehicle && lastEnterExitVehicle.driver){

				enterExitVehicleFound = true;
				BCG_EnterExitManager.Instance.waitTime = 10f;
				lastEnterExitVehicle.driver.GetOut();

			}

		}

#endif

        // If we have controllable vehicle by player on scene, destroy it.
        if (lastVehicle)
            Destroy(lastVehicle.gameObject);

        // Here we are creating our new vehicle.
        CargoTruck_CC.SpawnRCC(CargoTruck_CC_DemoVehicles.Instance.vehicles[selectedVehicleIndex], lastKnownPos, lastKnownRot, true, true, true);

#if BCG_ENTEREXIT

		if(enterExitVehicleFound){

			lastEnterExitVehicle = null;

			lastEnterExitVehicle = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.GetComponentInChildren<BCG_EnterExitVehicle> ();

			if(!lastEnterExitVehicle){
				
				lastEnterExitVehicle = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.gameObject.AddComponent<BCG_EnterExitVehicle> ();

			}

			if(BCG_EnterExitManager.Instance.activePlayer && lastEnterExitVehicle && lastEnterExitVehicle.driver == null){
				
				BCG_EnterExitManager.Instance.waitTime = 10f;
				BCG_EnterExitManager.Instance.activePlayer.GetIn(lastEnterExitVehicle);

			}

		}
		
#endif

    }

    // An integer index value used for setting behavior mode.
    public void SetBehavior(int index) {

        selectedBehaviorIndex = index;

    }

    // Here we are setting new selected behavior to corresponding one.
    public void InitBehavior() {

        CargoTruck_CC.SetBehavior(selectedBehaviorIndex);

    }

    // Sets the mobile controller type.
    public void SetMobileController(int index) {

        switch (index) {

            case 0:
                CargoTruck_CC.SetMobileController(CargoTruck_CC_Settings.MobileController.TouchScreen);
                break;
            case 1:
                CargoTruck_CC.SetMobileController(CargoTruck_CC_Settings.MobileController.Gyro);
                break;
            case 2:
                CargoTruck_CC.SetMobileController(CargoTruck_CC_Settings.MobileController.SteeringWheel);
                break;
            case 3:
                CargoTruck_CC.SetMobileController(CargoTruck_CC_Settings.MobileController.Joystick);
                break;

        }

    }

    /// <summary>
    /// Sets the quality.
    /// </summary>
    /// <param name="index">Index.</param>
    public void SetQuality(int index) {

        QualitySettings.SetQualityLevel(index);

    }

    // Simply restarting the current scene.
    public void RestartScene() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    // Simply quit application. Not working on Editor.
    public void Quit() {

        Application.Quit();

    }

}
