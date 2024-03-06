using UnityEngine;
using UnityEditor;

public class PlayerPrefsViewer : EditorWindow
{
    private string keyToInspect = "";

    [MenuItem("Window/PlayerPrefs Viewer")]
    static void Init()
    {
        PlayerPrefsViewer window = (PlayerPrefsViewer)EditorWindow.GetWindow(typeof(PlayerPrefsViewer));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("PlayerPrefs Viewer", EditorStyles.boldLabel);

        GUILayout.Space(10);

        keyToInspect = EditorGUILayout.TextField("Enter Key to Inspect:", keyToInspect);

        if (GUILayout.Button("Inspect"))
        {
            string value = PlayerPrefs.GetString(keyToInspect);
            Debug.Log($"PlayerPrefs Key: {keyToInspect}, Value: {value}");
        }
    }
}
