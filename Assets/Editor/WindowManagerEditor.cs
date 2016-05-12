using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using System.Text;

/// <summary>
/// Custom Editor for the WindowManager.
/// It allows us to have a reorderable list of Windows.
/// And also, we can regenerate the Windows Enum via a button,
/// so we can create more easily sequences of windows.
/// </summary>
[CustomEditor(typeof(WindowManager))]
public class WindowManagerEditor : Editor
{
    private ReorderableList _list;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Generate Window Enums"))
        {
            var windows = ((WindowManager)target)._windows;
            var total = windows.Length;

            // class to deal with strings without memory leaks
            var sb = new StringBuilder();

            // Generate the Windows enum file
            sb.Append("public enum EWindows\n");
            sb.Append("{\n");
            sb.Append("None,\n");
            for (var i = 0; i < total; i++)
            {
                sb.Append(windows[i].name.Replace(" ", ""));
                if (i < total - 1)
                {
                    sb.Append(",\n");
                }
            }
            sb.Append("\n}");

            // Ask for the path to save the file
            var path = EditorUtility.SaveFilePanel("Save The Window Enums", "Assets/Scripts/UI/Windows", "WindowEnums.cs", "cs");

            // Save the file to that path
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(sb.ToString());
                }
            }

            // Refresh the project folder
            AssetDatabase.Refresh();
        }
    }


    private void OnEnable()
    {
        _list = new ReorderableList(serializedObject, serializedObject.FindProperty("_windows"), true, true, true, true);

        _list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Windows");
        };

        _list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = _list.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, Screen.width - 75, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
        };
    }
}
