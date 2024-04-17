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
/// An example script to show how the RCC API works.
///</summary>
public class CargoTruck_CC_APIExample : MonoBehaviour {

    public CargoTruck_CC_CarControllerV3 spawnVehiclePrefab;          // Vehicle prefab we gonna spawn.
    private CargoTruck_CC_CarControllerV3 currentVehiclePrefab;       // Spawned vehicle.
    public Transform spawnTransform;                                // Spawn transform.

    public bool playerVehicle;          // Spawn as a player vehicle?
    public bool controllable;           // Spawn as controllable vehicle?
    public bool engineRunning;      // Spawn with running engine?

    public void Spawn() {

        // Spawning the vehicle with given settings.
        currentVehiclePrefab = CargoTruck_CC.SpawnRCC(spawnVehiclePrefab, spawnTransform.position, spawnTransform.rotation, playerVehicle, controllable, engineRunning);

    }

    public void SetPlayer() {

        // Registers the vehicle as player vehicle.
        CargoTruck_CC.RegisterPlayerVehicle(currentVehiclePrefab);

    }

    public void SetControl(bool control) {

        // Enables / disables controllable state of the vehicle.
        CargoTruck_CC.SetControl(currentVehiclePrefab, control);

    }

    public void SetEngine(bool engine) {

        // Starts / kills engine of the vehicle.
        CargoTruck_CC.SetEngine(currentVehiclePrefab, engine);

    }

    public void DeRegisterPlayer() {

        // Deregisters the vehicle from as player vehicle.
        CargoTruck_CC.DeRegisterPlayerVehicle();

    }

}
