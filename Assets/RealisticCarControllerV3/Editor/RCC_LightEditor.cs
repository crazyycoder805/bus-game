﻿//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2022 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(CargoTruck_CC_Light)), CanEditMultipleObjects]
public class CargoTruck_CC_LightEditor : Editor {

    CargoTruck_CC_Light prop;

    Color originalGUIColor;

    public override void OnInspectorGUI() {

        originalGUIColor = GUI.color;
        prop = (CargoTruck_CC_Light)target;
        serializedObject.Update();

        CheckLights();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("RCC lights will receive inputs from parent car controller and adjusts intensity for lights. You can choose which type of light you want to use below. You won't need to specify left or right indicator lights.", EditorStyles.helpBox);
        EditorGUILayout.LabelField("''Important'' or ''Not Important'' modes (Pixel or Vertex) overrided by CargoTruck_CC_Settings.", EditorStyles.helpBox);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("lightType"), new GUIContent("Light Type"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inertia"), new GUIContent("Light Inertia"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flare"), new GUIContent("Lens Flare"), false);

        if (!prop.GetComponent<LensFlare>()) {

            if (GUILayout.Button("Create LensFlare")) {

                GameObject[] lights = Selection.gameObjects;

                for (int i = 0; i < lights.Length; i++) {

                    if (lights[i].GetComponent<LensFlare>())
                        break;

                    lights[i].AddComponent<LensFlare>();
                    LensFlare lf = lights[i].GetComponent<LensFlare>();
                    lf.brightness = 0f;
                    lf.color = Color.white;
                    lf.fadeSpeed = 20f;

                }

            }

        } else {

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("RCC uses ''Interpolation'' mode for all rigidbodies. Therefore, lights at front of the vehicle will blink while on high speeds. To fix this, select your RCC layer in LensFlare component as ignored layer. CargoTruck_CC_Light script will simulate lens flares depending on camera distance and angle.''.", EditorStyles.helpBox);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("flareBrightness"), new GUIContent("Lens Flare Brightness"), false);

        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("inertia"), new GUIContent("Lens Flare Fade Speed"), false);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("useEmissionTexture"), new GUIContent("Use Emission Texture"), false);

        if (prop.useEmissionTexture) {

            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("emission"), new GUIContent("Emission"), true);
            EditorGUI.indentLevel--;

        }

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("isBreakable"), new GUIContent("Is Breakable"), false);

        if (prop.isBreakable) {

            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("strength"), new GUIContent("Strength"), false);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("breakPoint"), new GUIContent("Break Point"), false);
            EditorGUI.indentLevel--;

        }

        if (!prop.GetComponentInChildren<TrailRenderer>()) {

            if (GUILayout.Button("Create Trail")) {

                GameObject[] lights = Selection.gameObjects;

                for (int i = 0; i < lights.Length; i++) {

                    if (lights[i].GetComponentInChildren<TrailRenderer>())
                        break;

                    GameObject newTrail = GameObject.Instantiate(CargoTruck_CC_Settings.Instance.lightTrailers, lights[i].transform.position, lights[i].transform.rotation, lights[i].transform);
                    newTrail.name = CargoTruck_CC_Settings.Instance.lightTrailers.name;

                }

            }

        } else {

            if (GUILayout.Button("Select Trail"))
                Selection.activeGameObject = prop.GetComponentInChildren<TrailRenderer>().gameObject;

        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(prop);

    }

    void CheckLights() {

        if (!prop.gameObject.activeInHierarchy)
            return;

        if (prop.GetComponentInParent<CargoTruck_CC_CarControllerV3>() == null)
            return;

        Vector3 relativePos = prop.GetComponentInParent<CargoTruck_CC_CarControllerV3>().transform.InverseTransformPoint(prop.transform.position);

        if (relativePos.z > 0f) {

            if (Mathf.Abs(prop.transform.localRotation.y) > .5f) {

                GUI.color = Color.red;
                EditorGUILayout.HelpBox("Lights is facing to wrong direction!", MessageType.Error);
                GUI.color = originalGUIColor;

                GUI.color = Color.green;

                if (GUILayout.Button("Fix Rotation"))
                    prop.transform.localRotation = Quaternion.identity;

                GUI.color = originalGUIColor;

            }

        } else {

            if (Mathf.Abs(prop.transform.localRotation.y) < .5f) {

                GUI.color = Color.red;
                EditorGUILayout.HelpBox("Lights is facing to wrong direction!", MessageType.Error);
                GUI.color = originalGUIColor;

                GUI.color = Color.green;

                if (GUILayout.Button("Fix Rotation"))
                    prop.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

                GUI.color = originalGUIColor;

            }

        }

        if (!EditorApplication.isPlaying) {

            GameObject[] lights = Selection.gameObjects;

            for (int i = 0; i < lights.Length; i++) {

                if (lights[i].GetComponent<Light>().flare != null)
                    lights[i].GetComponent<Light>().flare = null;

                if (lights[i].GetComponent<LensFlare>())
                    lights[i].GetComponent<LensFlare>().brightness = 0f;

            }

        }

    }

}
