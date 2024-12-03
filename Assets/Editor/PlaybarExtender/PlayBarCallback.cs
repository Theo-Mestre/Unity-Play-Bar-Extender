using UnityEngine.UIElements;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System;

namespace PlayBarExtender
{
    public static class PlayBarCallback
    {
        private static readonly Type PlayBarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");

        private static ScriptableObject CurrentPlayBar;

        // Callbacks for extending the playbar.
        public static Action OnPlayBarGUI;
        public static Action OnPlayBarGUILeft;
        public static Action OnPlayBarGUIRight;

        static PlayBarCallback()
        {
            EditorApplication.update -= OnEditorUpdate;
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            if (CurrentPlayBar == null)
            {
                var toolbars = Resources.FindObjectsOfTypeAll(PlayBarType);
                CurrentPlayBar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;

                if (CurrentPlayBar == null) return;

                // Get the root VisualElement of the playbar
                var rootField = CurrentPlayBar.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
                var rawRoot = rootField?.GetValue(CurrentPlayBar);

                if (rawRoot is VisualElement rootElement)
                {
                    RegisterCallback("ToolbarZoneLeftAlign", OnPlayBarGUILeft);
                    RegisterCallback("ToolbarZoneRightAlign", OnPlayBarGUIRight);
                }
            }
        }

        private static void RegisterCallback(string playBarZoneName, Action callback)
        {
            if (CurrentPlayBar?.GetType()
                .GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance)?
                .GetValue(CurrentPlayBar) is not VisualElement playBarZone) return;

            var zone = playBarZone.Q(playBarZoneName);

            if (zone != null)
            {
                var parent = new VisualElement
                {
                    style =
                    {
                        flexGrow = 1,
                        flexDirection = FlexDirection.Row
                    }
                };

                var container = new IMGUIContainer
                {
                    style = { flexGrow = 1 },
                    onGUIHandler = () => callback?.Invoke()
                };

                parent.Add(container);
                zone.Add(parent);
            }
        }
    }
}
