using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ClearPlayerPrefsOnBuild
{
    static ClearPlayerPrefsOnBuild()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}