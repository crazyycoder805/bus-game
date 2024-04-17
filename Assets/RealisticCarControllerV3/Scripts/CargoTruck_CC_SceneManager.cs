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

/// <summary>
/// Scene manager that contains current player vehicle, current player camera, current player UI, current player character, recording/playing mechanim, and other vehicles as well.
/// 
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Main/RCC Scene Manager")]
public class CargoTruck_CC_SceneManager : CargoTruck_CC_Singleton<CargoTruck_CC_SceneManager> {

    public CargoTruck_CC_CarControllerV3 activePlayerVehicle;
    private CargoTruck_CC_CarControllerV3 lastActivePlayerVehicle;
    public CargoTruck_CC_Camera activePlayerCamera;
    public CargoTruck_CC_UIDashboardDisplay activePlayerCanvas;
    public Camera activeMainCamera;

    public bool registerFirstVehicleAsPlayer = true;
    public bool disableUIWhenNoPlayerVehicle = false;
    public bool loadCustomizationAtFirst = true;

    public List<CargoTruck_CC_Recorder> allRecorders = new List<CargoTruck_CC_Recorder>();
    public enum RecordMode { Neutral, Play, Record }
    public RecordMode recordMode;

    // Default time scale of the game.
    private float orgTimeScale = 1f;

    public List<CargoTruck_CC_CarControllerV3> allVehicles = new List<CargoTruck_CC_CarControllerV3>();

#if BCG_ENTEREXIT
    public BCG_EnterExitPlayer activePlayerCharacter;
#endif

    public Terrain[] allTerrains;

    public class Terrains {

        //	Terrain data.
        public Terrain terrain;
        public TerrainData mTerrainData;
        public PhysicMaterial terrainCollider;
        public int alphamapWidth;
        public int alphamapHeight;

        public float[,,] mSplatmapData;
        public float mNumTextures;

    }

    public Terrains[] terrains;
    public bool terrainsInitialized = false;

    // Firing an event when main behavior changed.
    public delegate void onBehaviorChanged();
    public static event onBehaviorChanged OnBehaviorChanged;

    // Firing an event when player vehicle changed.
    public delegate void onVehicleChanged();
    public static event onVehicleChanged OnVehicleChanged;

    void Awake() {

        // Overriding Fixed TimeStep.
        if (CargoTruck_CC_Settings.Instance.overrideFixedTimeStep)
            Time.fixedDeltaTime = CargoTruck_CC_Settings.Instance.fixedTimeStep;

        // Overriding FPS.
        if (CargoTruck_CC_Settings.Instance.overrideFPS)
            Application.targetFrameRate = CargoTruck_CC_Settings.Instance.maxFPS;

        if (CargoTruck_CC_Settings.Instance.useTelemetry)
            Instantiate(CargoTruck_CC_Settings.Instance.RCCTelemetry, Vector3.zero, Quaternion.identity);

        CargoTruck_CC_Camera.OnBCGCameraSpawned += CargoTruck_CC_Camera_OnBCGCameraSpawned;
        CargoTruck_CC_CarControllerV3.OnRCCPlayerSpawned += CargoTruck_CC_CarControllerV3_OnRCCSpawned;
        CargoTruck_CC_AICarController.OnRCCAISpawned += CargoTruck_CC_AICarController_OnRCCAISpawned;
        CargoTruck_CC_CarControllerV3.OnRCCPlayerDestroyed += CargoTruck_CC_CarControllerV3_OnRCCPlayerDestroyed;
        CargoTruck_CC_AICarController.OnRCCAIDestroyed += CargoTruck_CC_AICarController_OnRCCAIDestroyed;
        CargoTruck_CC_InputManager.OnSlowMotion += CargoTruck_CC_InputManager_OnSlowMotion;
        activePlayerCanvas = GameObject.FindObjectOfType<CargoTruck_CC_UIDashboardDisplay>();

#if BCG_ENTEREXIT
        BCG_EnterExitPlayer.OnBCGPlayerSpawned += BCG_EnterExitPlayer_OnBCGPlayerSpawned;
        BCG_EnterExitPlayer.OnBCGPlayerDestroyed += BCG_EnterExitPlayer_OnBCGPlayerDestroyed;
#endif

        // Getting default time scale of the game.
        orgTimeScale = Time.timeScale;

        if (CargoTruck_CC_Settings.Instance.lockAndUnlockCursor)
            Cursor.lockState = CursorLockMode.Locked;

    }

    #region ONSPAWNED

    void CargoTruck_CC_CarControllerV3_OnRCCSpawned(CargoTruck_CC_CarControllerV3 RCC) {

        if (!allVehicles.Contains(RCC)) {

            allVehicles.Add(RCC);

            allRecorders = new List<CargoTruck_CC_Recorder>();
            allRecorders.AddRange(gameObject.GetComponentsInChildren<CargoTruck_CC_Recorder>());

            CargoTruck_CC_Recorder recorder = null;

            if (allRecorders != null && allRecorders.Count > 0) {

                for (int i = 0; i < allRecorders.Count; i++) {

                    if (allRecorders[i] != null && allRecorders[i].carController == RCC) {
                        recorder = allRecorders[i];
                        break;
                    }

                }

            }

            if (recorder == null) {

                recorder = gameObject.AddComponent<CargoTruck_CC_Recorder>();
                recorder.carController = RCC;

            }

        }

        StartCoroutine(CheckMissingRecorders());

        if (registerFirstVehicleAsPlayer)
            RegisterPlayer(RCC);

#if BCG_ENTEREXIT
        if (RCC.gameObject.GetComponent<BCG_EnterExitVehicle>())
            RCC.gameObject.GetComponent<BCG_EnterExitVehicle>().correspondingCamera = activePlayerCamera.gameObject;
#endif

    }

    void CargoTruck_CC_AICarController_OnRCCAISpawned(CargoTruck_CC_AICarController RCCAI) {

        if (!allVehicles.Contains(RCCAI.carController)) {

            allVehicles.Add(RCCAI.carController);

            allRecorders = new List<CargoTruck_CC_Recorder>();
            allRecorders.AddRange(gameObject.GetComponentsInChildren<CargoTruck_CC_Recorder>());

            CargoTruck_CC_Recorder recorder = null;

            if (allRecorders != null && allRecorders.Count > 0) {

                for (int i = 0; i < allRecorders.Count; i++) {

                    if (allRecorders[i] != null && allRecorders[i].carController == RCCAI.carController) {
                        recorder = allRecorders[i];
                        break;
                    }

                }

            }

            if (recorder == null) {

                recorder = gameObject.AddComponent<CargoTruck_CC_Recorder>();
                recorder.carController = RCCAI.carController;

            }

        }

        StartCoroutine(CheckMissingRecorders());

    }

    void CargoTruck_CC_Camera_OnBCGCameraSpawned(GameObject BCGCamera) {

        activePlayerCamera = BCGCamera.GetComponent<CargoTruck_CC_Camera>();

    }

#if BCG_ENTEREXIT
    void BCG_EnterExitPlayer_OnBCGPlayerSpawned(BCG_EnterExitPlayer player) {

        activePlayerCharacter = player;

    }
#endif

    #endregion

    #region ONDESTROYED

    void CargoTruck_CC_CarControllerV3_OnRCCPlayerDestroyed(CargoTruck_CC_CarControllerV3 RCC) {

        if (allVehicles.Contains(RCC))
            allVehicles.Remove(RCC);

        StartCoroutine(CheckMissingRecorders());

    }

    void CargoTruck_CC_AICarController_OnRCCAIDestroyed(CargoTruck_CC_AICarController RCCAI) {

        if (allVehicles.Contains(RCCAI.carController))
            allVehicles.Remove(RCCAI.carController);

        StartCoroutine(CheckMissingRecorders());

    }

#if BCG_ENTEREXIT
    void BCG_EnterExitPlayer_OnBCGPlayerDestroyed(BCG_EnterExitPlayer player) {

    }
#endif

    #endregion

    private void Start() {

        StartCoroutine(GetAllTerrains());

    }

    public IEnumerator GetAllTerrains() {

        yield return new WaitForFixedUpdate();
        allTerrains = Terrain.activeTerrains;
        yield return new WaitForFixedUpdate();
        terrains = new Terrains[allTerrains.Length];

        for (int i = 0; i < allTerrains.Length; i++) {

            if (allTerrains[i].terrainData == null) {

                Debug.LogError("Terrain data of the " + allTerrains[i].transform.name + " is missing! Check the terrain data...");
                yield return null;

            }

        }

        for (int i = 0; i < terrains.Length; i++) {

            terrains[i] = new Terrains();
            terrains[i].terrain = allTerrains[i];
            terrains[i].mTerrainData = allTerrains[i].terrainData;
            terrains[i].terrainCollider = allTerrains[i].GetComponent<TerrainCollider>().sharedMaterial;
            terrains[i].alphamapWidth = allTerrains[i].terrainData.alphamapWidth;
            terrains[i].alphamapHeight = allTerrains[i].terrainData.alphamapHeight;

            terrains[i].mSplatmapData = allTerrains[i].terrainData.GetAlphamaps(0, 0, terrains[i].alphamapWidth, terrains[i].alphamapHeight);
            terrains[i].mNumTextures = terrains[i].mSplatmapData.Length / (terrains[i].alphamapWidth * terrains[i].alphamapHeight);

        }

        terrainsInitialized = true;

    }

    void Update() {

        if (activePlayerVehicle) {

            if (activePlayerVehicle != lastActivePlayerVehicle) {

                if (OnVehicleChanged != null)
                    OnVehicleChanged();

            }

            lastActivePlayerVehicle = activePlayerVehicle;

        }

        if (disableUIWhenNoPlayerVehicle && activePlayerCanvas)
            CheckCanvas();

        activeMainCamera = Camera.main;

        if (allRecorders != null && allRecorders.Count > 0) {

            switch (allRecorders[0].mode) {

                case CargoTruck_CC_Recorder.Mode.Neutral:

                    recordMode = RecordMode.Neutral;

                    break;

                case CargoTruck_CC_Recorder.Mode.Play:

                    recordMode = RecordMode.Play;

                    break;

                case CargoTruck_CC_Recorder.Mode.Record:

                    recordMode = RecordMode.Record;

                    break;

            }

        }

    }

    public void Record() {

        if (allRecorders != null && allRecorders.Count > 0) {

            for (int i = 0; i < allRecorders.Count; i++)
                allRecorders[i].Record();

        }

    }

    public void Play() {

        if (allRecorders != null && allRecorders.Count > 0) {

            for (int i = 0; i < allRecorders.Count; i++)
                allRecorders[i].Play();

        }

    }

    public void Stop() {

        if (allRecorders != null && allRecorders.Count > 0) {

            for (int i = 0; i < allRecorders.Count; i++)
                allRecorders[i].Stop();

        }

    }

    private IEnumerator CheckMissingRecorders() {

        yield return new WaitForFixedUpdate();

        allRecorders = new List<CargoTruck_CC_Recorder>();
        allRecorders.AddRange(gameObject.GetComponentsInChildren<CargoTruck_CC_Recorder>());

        if (allRecorders != null && allRecorders.Count > 0) {

            for (int i = 0; i < allRecorders.Count; i++) {

                if (allRecorders[i].carController == null)
                    Destroy(allRecorders[i]);

            }

        }
        yield return new WaitForFixedUpdate();

        allRecorders = new List<CargoTruck_CC_Recorder>();
        allRecorders.AddRange(gameObject.GetComponentsInChildren<CargoTruck_CC_Recorder>());

    }

    public void RegisterPlayer(CargoTruck_CC_CarControllerV3 playerVehicle) {

        activePlayerVehicle = playerVehicle;

        if (activePlayerCamera)
            activePlayerCamera.SetTarget(activePlayerVehicle.gameObject);

        if (loadCustomizationAtFirst)
            CargoTruck_CC_Customization.LoadStats(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle);

        if (FindObjectOfType<CargoTruck_CC_CustomizerExample>())
            FindObjectOfType<CargoTruck_CC_CustomizerExample>().CheckUIs();

    }

    public void RegisterPlayer(CargoTruck_CC_CarControllerV3 playerVehicle, bool isControllable) {

        activePlayerVehicle = playerVehicle;
        activePlayerVehicle.SetCanControl(isControllable);

        if (activePlayerCamera)
            activePlayerCamera.SetTarget(activePlayerVehicle.gameObject);

        if (loadCustomizationAtFirst)
            CargoTruck_CC_Customization.LoadStats(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle);

        if (FindObjectOfType<CargoTruck_CC_CustomizerExample>())
            FindObjectOfType<CargoTruck_CC_CustomizerExample>().CheckUIs();

    }

    public void RegisterPlayer(CargoTruck_CC_CarControllerV3 playerVehicle, bool isControllable, bool engineState) {

        activePlayerVehicle = playerVehicle;
        activePlayerVehicle.SetCanControl(isControllable);
        activePlayerVehicle.SetEngine(engineState);

        if (activePlayerCamera)
            activePlayerCamera.SetTarget(activePlayerVehicle.gameObject);

        if (loadCustomizationAtFirst)
            CargoTruck_CC_Customization.LoadStats(CargoTruck_CC_SceneManager.Instance.activePlayerVehicle);

        if (FindObjectOfType<CargoTruck_CC_CustomizerExample>())
            FindObjectOfType<CargoTruck_CC_CustomizerExample>().CheckUIs();

    }

    public void DeRegisterPlayer() {

        if (activePlayerVehicle)
            activePlayerVehicle.SetCanControl(false);

        activePlayerVehicle = null;

        if (activePlayerCamera)
            activePlayerCamera.RemoveTarget();

    }

    public void CheckCanvas() {

        if (!activePlayerVehicle || !activePlayerVehicle.canControl || !activePlayerVehicle.gameObject.activeInHierarchy || !activePlayerVehicle.enabled) {

            //			if (activePlayerCanvas.displayType == CargoTruck_CC_UIDashboardDisplay.DisplayType.Full)
            //				activePlayerCanvas.SetDisplayType(CargoTruck_CC_UIDashboardDisplay.DisplayType.Off);

            activePlayerCanvas.SetDisplayType(CargoTruck_CC_UIDashboardDisplay.DisplayType.Off);

            return;

        }

        //		if(!activePlayerCanvas.gameObject.activeInHierarchy)
        //			activePlayerCanvas.displayType = CargoTruck_CC_UIDashboardDisplay.DisplayType.Full;

        if (activePlayerCanvas.displayType != CargoTruck_CC_UIDashboardDisplay.DisplayType.Customization)
            activePlayerCanvas.displayType = CargoTruck_CC_UIDashboardDisplay.DisplayType.Full;

    }

    ///<summary>
    /// Sets new behavior.
    ///</summary>
    public static void SetBehavior(int behaviorIndex) {

        CargoTruck_CC_Settings.Instance.overrideBehavior = true;
        CargoTruck_CC_Settings.Instance.behaviorSelectedIndex = behaviorIndex;

        if (OnBehaviorChanged != null)
            OnBehaviorChanged();

    }

    // Changes current camera mode.
    public void ChangeCamera() {

        if (activePlayerCamera)
            activePlayerCamera.ChangeCamera();

    }

    /// <summary>
    /// Transport player vehicle the specified position and rotation.
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="rotation">Rotation.</param>
    public void Transport(Vector3 position, Quaternion rotation) {

        if (activePlayerVehicle) {

            activePlayerVehicle.rigid.velocity = Vector3.zero;
            activePlayerVehicle.rigid.angularVelocity = Vector3.zero;

            activePlayerVehicle.transform.position = position;
            activePlayerVehicle.transform.rotation = rotation;

            activePlayerVehicle.throttleInput = 0f;
            activePlayerVehicle.brakeInput = 1f;
            activePlayerVehicle.engineRPM = activePlayerVehicle.minEngineRPM;
            activePlayerVehicle.currentGear = 0;

            for (int i = 0; i < activePlayerVehicle.allWheelColliders.Length; i++)
                activePlayerVehicle.allWheelColliders[i].wheelCollider.motorTorque = 0f;

            StartCoroutine(Freeze(activePlayerVehicle));

        }

    }

    /// <summary>
    /// Transport target vehicle the specified position and rotation.
    /// </summary>
    /// <param name="vehicle"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    public void Transport(CargoTruck_CC_CarControllerV3 vehicle, Vector3 position, Quaternion rotation) {

        if (vehicle) {

            vehicle.rigid.velocity = Vector3.zero;
            vehicle.rigid.angularVelocity = Vector3.zero;

            vehicle.transform.position = position;
            vehicle.transform.rotation = rotation;

            vehicle.throttleInput = 0f;
            vehicle.brakeInput = 1f;
            vehicle.engineRPM = vehicle.minEngineRPM;
            vehicle.currentGear = 0;

            for (int i = 0; i < vehicle.allWheelColliders.Length; i++)
                vehicle.allWheelColliders[i].wheelCollider.motorTorque = 0f;

            StartCoroutine(Freeze(vehicle));

        }

    }

    private IEnumerator Freeze(CargoTruck_CC_CarControllerV3 vehicle) {

        float timer = 1f;

        while (timer > 0) {

            timer -= Time.deltaTime;
            vehicle.canControl = false;
            vehicle.rigid.velocity = new Vector3(0f, vehicle.rigid.velocity.y, 0f);
            vehicle.rigid.angularVelocity = Vector3.zero;
            yield return null;

        }

        vehicle.canControl = true;

    }

    private void CargoTruck_CC_InputManager_OnSlowMotion(bool state) {

        if (state)
            Time.timeScale = .2f;
        else
            Time.timeScale = orgTimeScale;

    }

    void OnDisable() {

        CargoTruck_CC_Camera.OnBCGCameraSpawned -= CargoTruck_CC_Camera_OnBCGCameraSpawned;

        CargoTruck_CC_CarControllerV3.OnRCCPlayerSpawned -= CargoTruck_CC_CarControllerV3_OnRCCSpawned;
        CargoTruck_CC_AICarController.OnRCCAISpawned -= CargoTruck_CC_AICarController_OnRCCAISpawned;
        CargoTruck_CC_CarControllerV3.OnRCCPlayerDestroyed -= CargoTruck_CC_CarControllerV3_OnRCCPlayerDestroyed;
        CargoTruck_CC_AICarController.OnRCCAIDestroyed -= CargoTruck_CC_AICarController_OnRCCAIDestroyed;
        CargoTruck_CC_InputManager.OnSlowMotion -= CargoTruck_CC_InputManager_OnSlowMotion;

#if BCG_ENTEREXIT
        BCG_EnterExitPlayer.OnBCGPlayerSpawned -= BCG_EnterExitPlayer_OnBCGPlayerSpawned;
        BCG_EnterExitPlayer.OnBCGPlayerDestroyed -= BCG_EnterExitPlayer_OnBCGPlayerDestroyed;
#endif

    }

}
