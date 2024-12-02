using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayBarExtenderSettings", menuName = "PlayBarExtender/PlayBarExtenderSettings")]
public class PlayBarExtenderSettings : ScriptableObject
{
    [Header("Play From Here Settings")]

    [Tooltip("Use default spawn functions taht only move the camera")]
    [SerializeField]
    public bool UseDefaultSpawnFunctions = true;

    [Tooltip("Functions to call when 'Play From Here' is enabled")]
    [SerializeField]
    public UnityEvent PlayerFromHereFunctions = new();
}
