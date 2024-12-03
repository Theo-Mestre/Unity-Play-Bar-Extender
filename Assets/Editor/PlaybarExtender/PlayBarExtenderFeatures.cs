using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
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
            PlayBarExtender.RightPlayBarGUI.Add(OnPlayBarGUI);
            EditorApplication.playModeStateChanged += OnPlayFromHere;
            settings = Resources.Load<PlayBarExtenderSettings>("PlayBarExtender/PlayBarExtenderSettings");

            if (settings == null)
            {
                Debug.LogError("PlayBarExtenderSettings not found. Using default settings");
                settings = ScriptableObject.CreateInstance<PlayBarExtenderSettings>();
                return;
            }
        }
        static void OnPlayBarGUI()
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

                if (settings.InvokeFunctionOnMoveCamera)
                {
                    settings.PlayerFromHereFunctions.Invoke();
                }
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
            // Add a new button to the left playbar
            PlayBarExtender.LeftPlayBarGUI.Add(OnSceneChangerGUI);
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