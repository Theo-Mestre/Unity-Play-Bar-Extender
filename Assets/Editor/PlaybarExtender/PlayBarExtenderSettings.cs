using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayBarExtenderSettings", menuName = "PlayBarExtender/PlayBarExtenderSettings")]
public class PlayBarExtenderSettings : ScriptableObject
{
    [Header("Play From Here Settings")]

    [Tooltip("Use default spawn functions taht only move the camera")]
    [SerializeField]
    public bool UseDefaultSpawnFunctions = true;

    [Tooltip("Invoke functions when 'Move Camera' is used")]
    [SerializeField]
    public bool InvokeFunctionOnMoveCamera = true;

    [Tooltip("Functions to call when 'Play From Here' is enabled")]
    [SerializeField]
    public UnityEvent PlayerFromHereFunctions = new();

    [Tooltip("Functions to call when using 'Cheat Functions'")]
    [SerializeField]
    public List<UnityEvent> CheatFunctions = new();
}
