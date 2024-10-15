using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(BoardData), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class BoardDataDrawer : Editor
{
    public BoardData m_GameDataInstance => target as BoardData;
    private ReorderableList m_DataList;

    private void OnEnable()
    {
        InitializeReorderableList(ref m_DataList, "m_SearchWords", "Searching Words");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawColumnsRowsInputFields();
        EditorGUILayout.Space();

        if (m_GameDataInstance.m_Board != null && m_GameDataInstance.m_Columns > 0 && m_GameDataInstance.m_Rows > 0)
        {
            DrawBoardTable();
        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(m_GameDataInstance);
            Repaint();
        }
    }

    private void DrawColumnsRowsInputFields()
    {
        int t_columns = m_GameDataInstance.m_Columns;
        int t_rows = m_GameDataInstance.m_Rows;

        m_GameDataInstance.m_Columns = EditorGUILayout.IntField("Columns", m_GameDataInstance.m_Columns);
        m_GameDataInstance.m_Rows = EditorGUILayout.IntField("Rows", m_GameDataInstance.m_Rows);

        if ((m_GameDataInstance.m_Columns != t_columns || m_GameDataInstance.m_Rows != t_rows) &&
            m_GameDataInstance.m_Columns > 0 && m_GameDataInstance.m_Rows > 0)
        {
            m_GameDataInstance.CreateBoard();
        }
    }

    private void DrawBoardTable()
    {
        GUIStyle l_tableStyle = new GUIStyle("box")
        {
            padding = new RectOffset(10, 10, 10, 10),
            margin = { left = 32 }
        };

        GUIStyle l_headerColumnStyle = new GUIStyle();
        l_headerColumnStyle.fixedWidth = 35;

        GUIStyle l_columnStyle = new GUIStyle();
        l_columnStyle.fixedWidth = 50;

        GUIStyle l_rowStyle = new GUIStyle();
        l_rowStyle.fixedHeight = 25;
        l_rowStyle.fixedWidth = 40;
        l_rowStyle.alignment = TextAnchor.MiddleCenter;

        GUIStyle l_textFieldStyle = new GUIStyle();
        l_textFieldStyle.normal.background = Texture2D.grayTexture;
        l_textFieldStyle.normal.textColor = Color.white;
        l_textFieldStyle.fontStyle = FontStyle.Bold;
        l_textFieldStyle.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.BeginHorizontal(l_tableStyle);

        for (int x = 0; x < m_GameDataInstance.m_Columns; x++)
        {
            EditorGUILayout.BeginVertical(x == -1 ? l_headerColumnStyle : l_columnStyle);

            for (int y = 0; y < m_GameDataInstance.m_Rows; y++)
            {
                if (x >= 0 && y >= 0)
                {
                    EditorGUILayout.BeginHorizontal(l_rowStyle);
                    var l_character =
                        (string)EditorGUILayout.TextArea(m_GameDataInstance.m_Board[x].m_Row[y], l_textFieldStyle);

                    if (m_GameDataInstance.m_Board[x].m_Row[y].Length > 1)
                    {
                        l_character = m_GameDataInstance.m_Board[x].m_Row[y].Substring(0, 1);
                    }

                    m_GameDataInstance.m_Board[x].m_Row[y] = l_character;
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void InitializeReorderableList(ref ReorderableList p_list, string p_propertyName, string p_listLable)
    {
        p_list = new ReorderableList(serializedObject, serializedObject.FindProperty(p_propertyName),
            true, true, true, true);

        p_list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, p_listLable);
        };

        var l = p_list;

        p_list.drawElementCallback = (Rect t_rect, int t_index, bool t_isActive, bool t_isFocused) =>
        {
            var l_element = l.serializedProperty.GetArrayElementAtIndex(t_index);
            t_rect.y += 2;

            EditorGUI.PropertyField(new Rect(t_rect.x, t_rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight),
                l_element.FindPropertyRelative("m_Word"), GUIContent.none);
        };
    }
}
