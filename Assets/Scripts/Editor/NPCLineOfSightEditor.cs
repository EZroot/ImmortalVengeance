using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCLineOfSight))]
public class NPCLineOfSightEditor : Editor
{
    private void OnSceneGUI()
    {
        NPCLineOfSight los = (NPCLineOfSight)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(los.transform.position, Vector3.forward, Vector3.up, 360, los.viewRadius);
        Vector3 viewAngleA = los.DirFromAngle(-los.viewAngle / 2, false);
        Vector3 viewAngleB = los.DirFromAngle(los.viewAngle / 2, false);

        Handles.DrawLine(los.transform.position, los.transform.position + viewAngleA * los.viewRadius);
        Handles.DrawLine(los.transform.position, los.transform.position + viewAngleB * los.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in los.visibleTargets)
        {
            Handles.DrawLine(los.transform.position, visibleTarget.position);
        }
    }
}