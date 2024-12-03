using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace PlayBarExtender
{
    [InitializeOnLoad]
    public static class PlayBarExtender
    {
        private static readonly int ToolCount;
        private static GUIStyle CommandStyle;

        public static readonly List<Action> LeftPlayBarGUI = new();
        public static readonly List<Action> RightPlayBarGUI = new();

        // Constants for layout dimensions
        private const float Spacing = 8f;
        private const float LargeSpacing = 20f;
        private const float ButtonWidth = 32f;
        private const float DropdownWidth = 80f;
        private const float PlayPauseStopWidth = 140f;

        static PlayBarExtender()
        {
            // Determine the number of playbar tools
            Type playBarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
            FieldInfo toolIconsField = playBarType?.GetField("k_ToolCount", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            ToolCount = toolIconsField != null ? (int)toolIconsField.GetValue(null) : 8;

            // Set up playbar callbacks
            PlayBarCallback.OnPlayBarGUI = OnPlayBarGUI;
            PlayBarCallback.OnPlayBarGUILeft = DrawLeftPlayBar;
            PlayBarCallback.OnPlayBarGUIRight = DrawRightPlayBar;
        }

        private static void OnPlayBarGUI()
        {
            CommandStyle ??= new GUIStyle("CommandLeft");

            float screenWidth = EditorGUIUtility.currentViewWidth;

            // Calculate positions
            float playButtonPosition = Mathf.RoundToInt((screenWidth - PlayPauseStopWidth) / 2);

            Rect leftRect = CreateLeftRect(screenWidth, playButtonPosition);
            Rect rightRect = CreateRightRect(screenWidth, playButtonPosition);

            // Draw left playbar
            if (leftRect.width > 0)
            {
                GUILayout.BeginArea(leftRect);
                GUILayout.BeginHorizontal();
                foreach (var handler in LeftPlayBarGUI)
                {
                    handler();
                }
                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }

            // Draw right playbar
            if (rightRect.width > 0)
            {
                GUILayout.BeginArea(rightRect);
                GUILayout.BeginHorizontal();
                foreach (var handler in RightPlayBarGUI)
                {
                    handler();
                }
                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }
        }

        private static Rect CreateLeftRect(float screenWidth, float playButtonPosition)
        {
            return new Rect(0, 0, screenWidth, Screen.height)
            {
                xMin = Spacing + ButtonWidth * ToolCount + LargeSpacing + (64 * 2),
                xMax = playButtonPosition,
                y = 4,
                height = 22
            };
        }

        private static Rect CreateRightRect(float screenWidth, float playButtonPosition)
        {
            return new Rect(0, 0, screenWidth, Screen.height)
            {
                xMin = playButtonPosition + CommandStyle.fixedWidth * 3,
                xMax = screenWidth - (DropdownWidth * 3 + Spacing * 4 + ButtonWidth + 78),
                y = 4,
                height = 22
            };
        }

        public static void DrawLeftPlayBar()
        {
            GUILayout.BeginHorizontal();
            foreach (var handler in LeftPlayBarGUI)
            {
                handler();
            }
            GUILayout.EndHorizontal();
        }

        public static void DrawRightPlayBar()
        {
            GUILayout.BeginHorizontal();
            foreach (var handler in RightPlayBarGUI)
            {
                handler();
            }
            GUILayout.EndHorizontal();
        }
    }
}
