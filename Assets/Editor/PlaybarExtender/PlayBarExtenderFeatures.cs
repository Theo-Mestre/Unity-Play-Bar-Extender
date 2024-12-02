using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine;

namespace PlayBarExtender
{
    [InitializeOnLoad]
    public class PlayFromHereButton : MonoBehaviour
    {
        static bool playFromHere = false;
        static readonly PlayBarExtenderSettings settings;

        static PlayFromHereButton()
        {
            PlayBarExtender.RightToolbarGUI.Add(OnToolbarGUI);
            EditorApplication.playModeStateChanged += OnPlayFromHere;
            settings = Resources.Load<PlayBarExtenderSettings>("PlayBarExtender/PlayBarExtenderSettings");

            if (settings == null)
            {
                Debug.LogError("PlayBarExtenderSettings not found. Using default settings");
                settings = ScriptableObject.CreateInstance<PlayBarExtenderSettings>();
                return;
            }
        }
        static void OnToolbarGUI()
        {
            if (EditorApplication.isPlaying == false)
            {
                string content = playFromHere ? "Play from here enabled" : "Play from here disabled";
                playFromHere = GUILayout.Toggle(playFromHere, new GUIContent(content, "Toggle Play from here"), EditorStyles.toolbarButton);
                GUILayout.FlexibleSpace();
                return;
            }

            if (GUILayout.Button(new GUIContent("Move Camera", "Move the camera at the editor camera position"), EditorStyles.toolbarButton))
            {
                SpawnCameraAtEditorCameraPosition();
            }
            GUILayout.FlexibleSpace();
        }

        static void OnPlayFromHere(PlayModeStateChange state)
        {
            if (playFromHere == false || state != PlayModeStateChange.EnteredPlayMode) return;

            if (settings.UseDefaultSpawnFunctions)
            {
                SpawnCameraAtEditorCameraPosition();
            }

            settings.PlayerFromHereFunctions.Invoke();
        }

        public static void SpawnCameraAtEditorCameraPosition()
        {
            Transform editorCameraTransform = SceneView.GetAllSceneCameras()[0].transform;

            Camera.main.transform.position = editorCameraTransform.position;
            Camera.main.transform.forward = editorCameraTransform.forward;
        }
    }

    [InitializeOnLoad]
    public class SwitchSceneButtons : MonoBehaviour
    {
        static SwitchSceneButtons()
        {
            PlayBarExtender.LeftToolbarGUI.Add(OnSceneChangerGUI);
        }

        static void OnSceneChangerGUI()
        {
            GUILayout.FlexibleSpace();

            for (int i = 0; i < EditorSceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                if (GUILayout.Button(new GUIContent(i.ToString(), sceneName), EditorStyles.toolbarButton))
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                    }
                }
                GUILayout.Space(5);
            }
        }
    }
}