using UnityEngine.UIElements;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System;

namespace PlayBarExtender
{
    public static class ToolbarCallback
    {
        private static readonly Type ToolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");

        private static ScriptableObject currentToolbar;

        // Callbacks for extending the toolbar.
        public static Action OnToolbarGUI;
        public static Action OnToolbarGUILeft;
        public static Action OnToolbarGUIRight;

        static ToolbarCallback()
        {
            EditorApplication.update -= OnEditorUpdate;
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            if (currentToolbar == null)
            {
                var toolbars = Resources.FindObjectsOfTypeAll(ToolbarType);
                currentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;

                if (currentToolbar != null)
                {
                    // Get the root VisualElement of the toolbar
                    var rootField = currentToolbar.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
                    var rawRoot = rootField?.GetValue(currentToolbar);

                    if (rawRoot is VisualElement rootElement)
                    {
                        RegisterCallback("ToolbarZoneLeftAlign", OnToolbarGUILeft);
                        RegisterCallback("ToolbarZoneRightAlign", OnToolbarGUIRight);
                    }
                }
            }
        }

        private static void RegisterCallback(string toolbarZoneName, Action callback)
        {
            if (currentToolbar?.GetType()
                .GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance)?
                .GetValue(currentToolbar) is not VisualElement toolbarZone) return;

            var zone = toolbarZone.Q(toolbarZoneName);

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
