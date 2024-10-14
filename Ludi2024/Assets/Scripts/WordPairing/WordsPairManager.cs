using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsPairManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private List<WordsPair> m_WordsSetters;

    private void Start()
    {
        
    }
    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void CheckPairs()
    {
        foreach (var t_pair in m_WordsSetters)
        {
            //if(t_pair.)
        }
    }
}
