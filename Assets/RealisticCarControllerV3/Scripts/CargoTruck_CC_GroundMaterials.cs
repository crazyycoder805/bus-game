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

[System.Serializable]
public class CargoTruck_CC_GroundMaterials : ScriptableObject {

    #region singleton
    private static CargoTruck_CC_GroundMaterials instance;
    public static CargoTruck_CC_GroundMaterials Instance { get { if (instance == null) instance = Resources.Load("RCC Assets/CargoTruck_CC_GroundMaterials") as CargoTruck_CC_GroundMaterials; return instance; } }
    #endregion

    [System.Serializable]
    public class GroundMaterialFrictions {

        public PhysicMaterial groundMaterial;
        public float forwardStiffness = 1f;
        public float sidewaysStiffness = 1f;
        public float slip = .25f;
        public float damp = 1f;
        [Range(0f, 1f)] public float volume = 1f;
        public GameObject groundParticles;
        public AudioClip groundSound;
        public CargoTruck_CC_Skidmarks skidmark;
        public bool deflate = false;

    }

    public GroundMaterialFrictions[] frictions;

    [System.Serializable]
    public class TerrainFrictions {

        public PhysicMaterial groundMaterial;

        [System.Serializable]
        public class SplatmapIndexes {

            public int index = 0;

        }

        public SplatmapIndexes[] splatmapIndexes;

    }

    public TerrainFrictions[] terrainFrictions;

}


