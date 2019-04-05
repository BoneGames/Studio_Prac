using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JoystickInput), true)]
public class JoystickEditor : Editor {

    private SerializedProperty      handlerRange,
                                    deadZone,
                                    axisOptions,
                                    snapX,
                                    snapY,
                                    handle;
    protected SerializedProperty    background;


    protected Vector2 center = new Vector2(.5f, .5f);

    protected virtual void OnEnable()
    {
        handlerRange = serializedObject.FindProperty("handlerRange");
        deadZone = serializedObject.FindProperty("deadZone");
        axisOptions = serializedObject.FindProperty("axisOptions");
        snapX = serializedObject.FindProperty("snapX");
        snapY = serializedObject.FindProperty("snapY");
        background = serializedObject.FindProperty("background");
        handle = serializedObject.FindProperty("handle");
    }

    protected virtual void DrawValues()
    {
        EditorGUILayout.PropertyField(handlerRange, new GUIContent("Handler Range", "The distance the visual handle can move from the centre of the joystick"));
        EditorGUILayout.PropertyField(deadZone, new GUIContent("Dead Zone", "The distance away from the centre the input has to be before registering"));
        EditorGUILayout.PropertyField(axisOptions, new GUIContent("Axis options", "Which axis the joystick uses"));
        EditorGUILayout.PropertyField(snapX, new GUIContent("Snap X", "Snap the Horizontal input to a whole value"));
        EditorGUILayout.PropertyField(snapY, new GUIContent("Snap Y", "Snap the Vertical input to a whole value"));
    }

    protected virtual void DrawComponents()
    {
        EditorGUILayout.ObjectField(background, new GUIContent("Background", "The background's RectTransform component."));
        EditorGUILayout.ObjectField(handle, new GUIContent("Handle", "The handle's RectTransform component."));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawValues();
        EditorGUILayout.Space();
        DrawComponents();

        serializedObject.ApplyModifiedProperties();

        if(handle != null)
        {
            RectTransform handleRect = (RectTransform)handle.objectReferenceValue;
            handleRect.anchorMax = center;
            handleRect.anchorMin = center;
            handleRect.pivot = center;
            handleRect.anchoredPosition = Vector2.zero;
        }
    }
}
