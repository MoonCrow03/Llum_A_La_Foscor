using System;
using UnityEngine;

namespace Wordle
{
    public class Row : MonoBehaviour
    {
        public Tile[] Tiles { get; private set; }

        public string word
        {
            get
            {
                string word = "";
                for (int i = 0; i < Tiles.Length; i++)
                {
                    word += Tiles[i].Letter;
                }
                
                return word;
            }
        }
        private void Awake()
        {
            Tiles = GetComponentsInChildren<Tile>();
        }
        
        
    }
}
