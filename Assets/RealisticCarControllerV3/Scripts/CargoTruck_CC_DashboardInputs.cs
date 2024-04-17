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

/// <summary>
/// Receiving inputs from active vehicle on your scene, and feeds dashboard needles, texts, images.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC UI Dashboard Inputs")]
public class CargoTruck_CC_DashboardInputs : MonoBehaviour {

    public GameObject RPMNeedle;
    public GameObject KMHNeedle;
    public GameObject turboGauge;
    public GameObject turboNeedle;
    public GameObject NOSGauge;
    public GameObject NoSNeedle;
    public GameObject heatGauge;
    public GameObject heatNeedle;
    public GameObject fuelGauge;
    public GameObject fuelNeedle;

    private float RPMNeedleRotation = 0f;
    private float KMHNeedleRotation = 0f;
    private float BoostNeedleRotation = 0f;
    private float NoSNeedleRotation = 0f;
    private float heatNeedleRotation = 0f;
    private float fuelNeedleRotation = 0f;

    internal float RPM;
    internal float KMH;
    internal int direction = 1;
    internal float Gear;
    internal bool changingGear = false;
    internal bool NGear = false;

    internal bool ABS = false;
    internal bool ESP = false;
    internal bool Park = false;
    internal bool Headlights = false;

    internal CargoTruck_CC_CarControllerV3.IndicatorsOn indicators;

    void Update() {

        GetValues();

    }

    void GetValues() {

        if (!CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
            return;

        if (!CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.canControl || CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.externalController)
            return;

        if (NOSGauge) {

            if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.useNOS) {

                if (!NOSGauge.activeSelf)
                    NOSGauge.SetActive(true);

            } else {

                if (NOSGauge.activeSelf)
                    NOSGauge.SetActive(false);

            }

        }

        if (turboGauge) {

            if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.useTurbo) {

                if (!turboGauge.activeSelf)
                    turboGauge.SetActive(true);

            } else {

                if (turboGauge.activeSelf)
                    turboGauge.SetActive(false);

            }

        }

        if (heatGauge) {

            if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.useEngineHeat) {

                if (!heatGauge.activeSelf)
                    heatGauge.SetActive(true);

            } else {

                if (heatGauge.activeSelf)
                    heatGauge.SetActive(false);

            }

        }

        if (fuelGauge) {

            if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.useFuelConsumption) {

                if (!fuelGauge.activeSelf)
                    fuelGauge.SetActive(true);

            } else {

                if (fuelGauge.activeSelf)
                    fuelGauge.SetActive(false);

            }

        }

        RPM = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.engineRPM;
        KMH = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.speed;
        direction = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.direction;
        Gear = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.currentGear;
        changingGear = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.changingGear;
        NGear = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.NGear;

        ABS = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.ABSAct;
        ESP = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.ESPAct;
        Park = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.handbrakeInput > .1f ? true : false;
        Headlights = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn || CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn;
        indicators = CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.indicatorsOn;

        if (RPMNeedle) {

            RPMNeedleRotation = (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.engineRPM / 50f);
            RPMNeedleRotation = Mathf.Clamp(RPMNeedleRotation, 0f, 180f);
            RPMNeedle.transform.eulerAngles = new Vector3(RPMNeedle.transform.eulerAngles.x, RPMNeedle.transform.eulerAngles.y, -RPMNeedleRotation);

        }

        if (KMHNeedle) {

            if (CargoTruck_CC_Settings.Instance.units == CargoTruck_CC_Settings.Units.KMH)
                KMHNeedleRotation = (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.speed);
            else
                KMHNeedleRotation = (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.speed * 0.62f);

            KMHNeedle.transform.eulerAngles = new Vector3(KMHNeedle.transform.eulerAngles.x, KMHNeedle.transform.eulerAngles.y, -KMHNeedleRotation);

        }

        if (turboNeedle) {

            BoostNeedleRotation = (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.turboBoost / 30f) * 270f;
            turboNeedle.transform.eulerAngles = new Vector3(turboNeedle.transform.eulerAngles.x, turboNeedle.transform.eulerAngles.y, -BoostNeedleRotation);

        }

        if (NoSNeedle) {

            NoSNeedleRotation = (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.NoS / 100f) * 270f;
            NoSNeedle.transform.eulerAngles = new Vector3(NoSNeedle.transform.eulerAngles.x, NoSNeedle.transform.eulerAngles.y, -NoSNeedleRotation);

        }

        if (heatNeedle) {

            heatNeedleRotation = (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.engineHeat / 110f) * 270f;
            heatNeedle.transform.eulerAngles = new Vector3(heatNeedle.transform.eulerAngles.x, heatNeedle.transform.eulerAngles.y, -heatNeedleRotation);

        }

        if (fuelNeedle) {

            fuelNeedleRotation = (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.fuelTank / CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.fuelTankCapacity) * 270f;
            fuelNeedle.transform.eulerAngles = new Vector3(fuelNeedle.transform.eulerAngles.x, fuelNeedle.transform.eulerAngles.y, -fuelNeedleRotation);

        }

    }

}



