using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class JamEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Random Solo"))
        {
            (target as GameManager).JM.GuitarSolo();            
        }
        if (GUILayout.Button("Add Guitarist"))
        {
            (target as GameManager).CreateNextGuitarist();
        }
    }
}
