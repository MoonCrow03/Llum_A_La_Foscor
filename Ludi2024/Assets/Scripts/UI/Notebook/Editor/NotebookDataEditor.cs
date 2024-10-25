using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NotebookData), false)]
public class NotebookDataEditor : Editor
{
    private NotebookData m_NotebookData => target as NotebookData;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Loop through existing notes
        for (int i = 0; i < m_NotebookData.Notes.Count; i++)
        {
            var m_Note = m_NotebookData.Notes[i];

            EditorGUILayout.BeginVertical("Box");
            m_Note.Key = (Scenes)EditorGUILayout.EnumPopup("Scene", m_Note.Key);

            EditorGUILayout.LabelField("Content");
            m_Note.Content = EditorGUILayout.TextArea(m_Note.Content, GUILayout.MinHeight(60));

            m_Note.IsCompleted = EditorGUILayout.Toggle("Is Completed", m_Note.IsCompleted);

            // Remove note button
            if (GUILayout.Button("Remove"))
            {
                m_NotebookData.Notes.RemoveAt(i);
                break;
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        EditorGUILayout.Space();

        // Add new note button
        if (GUILayout.Button("Add New Note"))
        {
            m_NotebookData.Notes.Add(new NotebookData.Note());
        }

        serializedObject.ApplyModifiedProperties();
    }
}
