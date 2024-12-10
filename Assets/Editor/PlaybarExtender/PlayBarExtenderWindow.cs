using UnityEngine;
using UnityEditor;

namespace PlayBarExtender
{
    public class PlayBarExtenderWindow : EditorWindow
    {
        static PlayBarExtenderSettings settings;
        static SerializedObject serializedObject;

        [MenuItem("Tools/Play Bar Extender Settings")]
        public static void ShowWindow()
        {
            PlayBarExtenderWindow window = GetWindow<PlayBarExtenderWindow>();
            window.titleContent = new GUIContent("Play Bar Extender Settings");
            window.Show();

            settings = Resources.Load<PlayBarExtenderSettings>("PlayBarExtender/PlayBarExtenderSettings");
            if (settings == null)
            {
                Debug.LogError("PlayBarExtenderSettings not found. Please Create One");
                return;
            }
            serializedObject = new SerializedObject(settings, window);
        }

        private void OnGUI()
        {
            if (settings == null) return;

            EditorGUILayout.LabelField("Play Bar Extender Settings", EditorStyles.boldLabel);
            float originalValue = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 225.0f;

            EditorGUILayout.Space();
            settings.UseDefaultSpawnFunctions = EditorGUILayout.Toggle("Use Default Spawn Functions", settings.UseDefaultSpawnFunctions, GUILayout.ExpandWidth(true));

            EditorGUILayout.Space();
            settings.InvokeFunctionOnMoveCamera = EditorGUILayout.Toggle("Invoke Function On Move Camera", settings.InvokeFunctionOnMoveCamera, GUILayout.ExpandWidth(true));
            EditorGUIUtility.labelWidth = originalValue;

            EditorGUILayout.Space();
            TimeScaleSlider.clampValue = EditorGUILayout.Vector2Field("Time Scale Slider Clamp", TimeScaleSlider.clampValue);
            Time.timeScale = Mathf.Clamp(Time.timeScale, TimeScaleSlider.clampValue.x, TimeScaleSlider.clampValue.y);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Add functions to call when 'Play From Here' is enabled or 'Move Camera' is used");
            serializedObject.Update();
            SerializedProperty serializedProperty = serializedObject.FindProperty("PlayerFromHereFunctions");
            EditorGUILayout.PropertyField(serializedProperty);
            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(settings);
            }
        }
    }
}