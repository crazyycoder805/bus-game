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
/// UI buttons used in options panel. It has an enum for all kind of buttons. 
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC UI Dashboard Button")]
public class CargoTruck_CC_UIDashboardButton : MonoBehaviour, IPointerClickHandler {

    public ButtonType _buttonType;
    public enum ButtonType { Start, ABS, ESP, TCS, Headlights, LeftIndicator, RightIndicator, Gear, Low, Med, High, SH, GearUp, GearDown, HazardLights, SlowMo, Record, Replay, Neutral, ChangeCamera };
    private Scrollbar gearSlider;

    public int gearDirection = 0;

    public void OnPointerClick(PointerEventData eventData) {

        OnClicked();

    }

    void Start() {

        if (_buttonType == ButtonType.Gear && GetComponentInChildren<Scrollbar>()) {

            gearSlider = GetComponentInChildren<Scrollbar>();
            gearSlider.onValueChanged.AddListener(delegate { ChangeGear(); });

        }

    }

    void OnEnable() {

        Check();

    }

    private void OnClicked() {

        switch (_buttonType) {

            case ButtonType.Low:

                QualitySettings.SetQualityLevel(1);

                break;

            case ButtonType.Med:

                QualitySettings.SetQualityLevel(3);

                break;

            case ButtonType.High:

                QualitySettings.SetQualityLevel(5);

                break;

            case ButtonType.SlowMo:

                if (Time.timeScale != .2f)
                    Time.timeScale = .2f;
                else
                    Time.timeScale = 1f;

                break;

            case ButtonType.Record:

                CargoTruck_CC.StartStopRecord();

                break;

            case ButtonType.Replay:

                CargoTruck_CC.StartStopReplay();

                break;

            case ButtonType.Neutral:

                CargoTruck_CC.StopRecordReplay();

                break;

            case ButtonType.ChangeCamera:

                CargoTruck_CC.ChangeCamera();

                break;


            case ButtonType.Start:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
                    CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.KillOrStartEngine();

                break;

            case ButtonType.ABS:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
                    CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.ABS = !CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.ABS;

                break;

            case ButtonType.ESP:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
                    CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.ESP = !CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.ESP;

                break;

            case ButtonType.TCS:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
                    CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.TCS = !CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.TCS;

                break;

            case ButtonType.SH:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
                    CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.steeringHelper = !CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.steeringHelper;

                break;

            case ButtonType.Headlights:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle) {

                    if (!CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn && CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn) {

                        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn = true;
                        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn = true;
                        break;

                    }

                    if (!CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn)
                        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn = true;

                    if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn) {

                        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn = false;
                        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn = false;

                    }

                }

                break;

            case ButtonType.LeftIndicator:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle) {

                    if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.indicatorsOn != CargoTruck_CC_CarControllerV3.IndicatorsOn.Left)
                        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = CargoTruck_CC_CarControllerV3.IndicatorsOn.Left;
                    else
                        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = CargoTruck_CC_CarControllerV3.IndicatorsOn.Off;

                }

                break;

            case ButtonType.RightIndicator:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle) {

                    if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.indicatorsOn != CargoTruck_CC_CarControllerV3.IndicatorsOn.Right)
                        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = CargoTruck_CC_CarControllerV3.IndicatorsOn.Right;
                    else
                        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = CargoTruck_CC_CarControllerV3.IndicatorsOn.Off;

                }

                break;

            case ButtonType.HazardLights:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle) {

                    if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.indicatorsOn != CargoTruck_CC_CarControllerV3.IndicatorsOn.All)
                        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = CargoTruck_CC_CarControllerV3.IndicatorsOn.All;
                    else
                        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.indicatorsOn = CargoTruck_CC_CarControllerV3.IndicatorsOn.Off;

                }

                break;

            case ButtonType.GearUp:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
                    CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.GearShiftUp();

                break;

            case ButtonType.GearDown:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
                    CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.GearShiftDown();

                break;

        }

        Check();

    }

    public void Check() {

        if (!GetComponent<Image>())
            return;

        if (!CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
            return;

        switch (_buttonType) {

            case ButtonType.ABS:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.ABS)
                    GetComponent<Image>().color = new Color(1, 1, 1, 1);
                else
                    GetComponent<Image>().color = new Color(.25f, .25f, .25f, 1);

                break;

            case ButtonType.ESP:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.ESP)
                    GetComponent<Image>().color = new Color(1, 1, 1, 1);
                else
                    GetComponent<Image>().color = new Color(.25f, .25f, .25f, 1);

                break;

            case ButtonType.TCS:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.TCS)
                    GetComponent<Image>().color = new Color(1, 1, 1, 1);
                else
                    GetComponent<Image>().color = new Color(.25f, .25f, .25f, 1);

                break;

            case ButtonType.SH:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.steeringHelper)
                    GetComponent<Image>().color = new Color(1, 1, 1, 1);
                else
                    GetComponent<Image>().color = new Color(.25f, .25f, .25f, 1);

                break;

            case ButtonType.Headlights:

                if (CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn || CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn)
                    GetComponent<Image>().color = new Color(1, 1, 1, 1);
                else
                    GetComponent<Image>().color = new Color(.25f, .25f, .25f, 1);

                break;

        }

    }

    public void ChangeGear() {

        if (!CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
            return;

        if (gearDirection == Mathf.CeilToInt(gearSlider.value * 2))
            return;

        gearDirection = Mathf.CeilToInt(gearSlider.value * 2);

        CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.semiAutomaticGear = true;

        switch (gearDirection) {

            case 0:
                CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.StartCoroutine("ChangeGear", 0);
                CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.NGear = false;
                break;

            case 1:
                CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.NGear = true;
                break;

            case 2:
                CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.StartCoroutine("ChangeGear", -1);
                CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.NGear = false;
                break;

        }

    }

    void OnDisable() {

        //		if (!CargoTruck_CC_SceneManager.Instance.activePlayerVehicle)
        //			return;
        //
        //		if(_buttonType == ButtonType.Gear){
        //
        //			CargoTruck_CC_SceneManager.Instance.activePlayerVehicle.semiAutomaticGear = false;
        //
        //		}

    }

}
