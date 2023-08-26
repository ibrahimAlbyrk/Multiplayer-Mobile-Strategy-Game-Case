using UnityEditor;
using UnityEngine;

namespace Core.Runtime.Animations.Event
{
    [CustomEditor(typeof(EventSMB))]
    public class EventSMBEditor : UnityEditor.Editor
    {
        private bool _showEvents = true;
        
        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector(); 

            var myTarget = (EventSMB)target;
            
            EditorGUILayout.BeginHorizontal("box");
            
            if(GUILayout.Button("Add Event"))
            {
                Undo.RecordObject(myTarget, "Add Event");
                myTarget.AddEvent();
                
                serializedObject.Update();
            }

            if(GUILayout.Button("Remove Event"))
            {
                Undo.RecordObject(myTarget, "Remove Event");
                myTarget.RemoveEvent();
                
                serializedObject.Update();
            }
            
            EditorGUILayout.EndHorizontal();
            
            var list = serializedObject.FindProperty("_events");
            
            EditorGUILayout.BeginVertical("box");
            
            _showEvents = EditorGUILayout.Foldout(_showEvents, "Events", true);

            if (_showEvents)
            {
                EditorGUI.indentLevel++;
                
                // Events List
                for (var i = 0; i < list.arraySize; i++)
                {
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent("Event " + (i + 1)));
                }  
                
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.EndVertical();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}