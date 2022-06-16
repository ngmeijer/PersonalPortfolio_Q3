using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyController))]
public class EnemyControllerEditor : Editor
{
    private E_State currentState;
    
    public override void OnInspectorGUI()
    {
        EnemyController myTarget = (EnemyController)target;
        
        currentState = (E_State)EditorGUILayout.EnumPopup("State:", currentState);
        
        myTarget.ChangeState(currentState);

        DrawDefaultInspector();
    }
}
