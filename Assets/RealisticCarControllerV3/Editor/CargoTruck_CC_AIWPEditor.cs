//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2022 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(CargoTruck_CC_AIWaypointsContainer))]
public class CargoTruck_CC_AIWPEditor : Editor {

    CargoTruck_CC_AIWaypointsContainer wpScript;

    public override void OnInspectorGUI() {

        wpScript = (CargoTruck_CC_AIWaypointsContainer)target;
        serializedObject.Update();

        EditorGUILayout.HelpBox("Create Waypoints By Shift + Left Mouse Button On Your Road", MessageType.Info);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("waypoints"), new GUIContent("Waypoints", "Waypoints"), true);

        foreach (Transform item in wpScript.transform) {

            if (item.gameObject.GetComponent<CargoTruck_CC_Waypoint>() == null)
                item.gameObject.AddComponent<CargoTruck_CC_Waypoint>();

        }

        if (GUILayout.Button("Delete Waypoints")) {

            foreach (CargoTruck_CC_Waypoint t in wpScript.waypoints) {
                DestroyImmediate(t.gameObject);
            }
            wpScript.waypoints.Clear();

        }

        serializedObject.ApplyModifiedProperties();

    }

    void OnSceneGUI() {

        Event e = Event.current;
        wpScript = (CargoTruck_CC_AIWaypointsContainer)target;

        if (e != null) {

            if (e.isMouse && e.shift && e.type == EventType.MouseDown) {

                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, 5000.0f)) {

                    Vector3 newTilePosition = hit.point;

                    GameObject wp = new GameObject("Waypoint " + wpScript.waypoints.Count.ToString());
                    wp.AddComponent<CargoTruck_CC_Waypoint>();
                    wp.transform.position = newTilePosition;
                    wp.transform.SetParent(wpScript.transform);

                    GetWaypoints();

                }

            }

            if (wpScript)
                Selection.activeGameObject = wpScript.gameObject;

        }

        GetWaypoints();

    }

    public void GetWaypoints() {

        wpScript.waypoints = new List<CargoTruck_CC_Waypoint>();

        CargoTruck_CC_Waypoint[] allTransforms = wpScript.transform.GetComponentsInChildren<CargoTruck_CC_Waypoint>();

        foreach (CargoTruck_CC_Waypoint t in allTransforms) {

            if (t != wpScript.transform)
                wpScript.waypoints.Add(t);

        }

    }

}
