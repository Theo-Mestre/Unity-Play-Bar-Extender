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
            PlayBarExtender.RightPlayBarGUI.Insert(0, OnPlayBarGUI);
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
            GUILayout.Space(5);

            if (EditorApplication.isPlaying == false)
            {
                string content = playFromHere ? "Play from here enabled" : "Play from here disabled";
                playFromHere = GUILayout.Toggle(playFromHere, new GUIContent(content, "Toggle Play from here"), EditorStyles.toolbarButton);
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

    [InitializeOnLoad]
    public class TimeScaleSlider
    {
        static public Vector2 clampValue = new Vector2(0, 2);
        static TimeScaleSlider()
        {
            PlayBarExtender.LeftPlayBarGUI.Add(OnTimeScaleSliderGUI);
        }
        static void OnTimeScaleSliderGUI()
        {
            GUILayout.FlexibleSpace();

            GUILayout.Label("Time Scale", EditorStyles.miniLabel);
            Time.timeScale = GUILayout.HorizontalSlider(Time.timeScale, clampValue.x, clampValue.y, GUILayout.Width(100));
            GUILayout.Label(Time.timeScale.ToString("0.00"), EditorStyles.miniLabel);
            GUILayout.Space(10);
        }
    }

    [InitializeOnLoad]
    public class CheatSheet : MonoBehaviour
    {
        static readonly PlayBarExtenderSettings settings;

        static CheatSheet()
        {
            PlayBarExtender.RightPlayBarGUI.Add(OnCheatSheetGUI);

            settings = Resources.Load<PlayBarExtenderSettings>("PlayBarExtender/PlayBarExtenderSettings");

            if (settings == null)
            {
                Debug.LogError("PlayBarExtenderSettings not found. Using default settings");
                settings = ScriptableObject.CreateInstance<PlayBarExtenderSettings>();
                return;
            }
        }
        static void OnCheatSheetGUI()
        {
            GUILayout.Space(10);

            if (settings.CheatFunctions.Count == 0)
            {
                GUILayout.FlexibleSpace();
                return;
            }

            if (GUILayout.Button("Cheat Sheet", EditorStyles.toolbarDropDown))
            {
                GenericMenu menu = new GenericMenu();
                for (int i = 0; i < settings.CheatFunctions.Count; i++)
                {
                    if (settings.CheatFunctions[i] == null || 
                        settings.CheatFunctions[i].GetPersistentEventCount() == 0)
                        continue;

                    UnityEvent cheatFunction = settings.CheatFunctions[i];
                    menu.AddItem(new GUIContent(cheatFunction.GetPersistentMethodName(0)), false, () =>
                    {
                        cheatFunction.Invoke();
                    });
                }

                if (menu.GetItemCount() == 0)
                {
                    menu.AddDisabledItem(new GUIContent("No functions added"));
                }

                menu.ShowAsContext();
            }
            GUILayout.FlexibleSpace();
        }
    }
}