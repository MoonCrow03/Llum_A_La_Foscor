using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Notes.Sort((note1, note2) => note2.IsCompleted.CompareTo(note1.IsCompleted));
    }

    public bool AreAllLevel1NotesCompleted()
    {
        return Notes
            .Where(note => note.Key.ToString().Contains("Lvl01"))
            .All(note => note.IsCompleted);
    }

    public bool AreAllLevel2NotesCompleted()
    {
        return Notes
            .Where(note => note.Key.ToString().Contains("Lvl02"))
            .All(note => note.IsCompleted);
    }

    public void ResetNotes()
    {
        foreach (var note in Notes)
        {
            note.IsCompleted = false;
        }
    }
}
