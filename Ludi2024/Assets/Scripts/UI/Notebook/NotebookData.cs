using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NotebookData", menuName = "Notebook/NotebookData", order = 1)]
public class NotebookData : ScriptableObject
{
    [System.Serializable]
    public class Note
    {
        public Scenes Key;
        public string Content;
        public bool IsCompleted;
    }

    public List<Note> Notes = new List<Note>();

    public Note FindNoteByScene(Scenes p_scene)
    {
        return Notes.Find(l_note => l_note.Key == p_scene);
    }

    public bool IsNoteCompleted(Scenes p_scene)
    {
        return Notes.Find(l_note => l_note.Key == p_scene).IsCompleted;
    }

    public void SetNoteCompleted(Scenes p_scene)
    {
        Notes.Find(l_note => l_note.Key == p_scene).IsCompleted = true;
    }
}
