using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    [System.Serializable]
    // Since we want to reference this data for every file, this script is not a monobehaviour and is instead serializable
    public class CharacterSaveData
    {
        [Header("Scene Index")]
        public int sceneIndex = 1;

        [Header("Character Name")]
        public string characterName = "Character";

        [Header("Time Played")]
        public float secondsPlayed;

        // Question: why not use a vector3?
        // Answer: we can only save data from "basic" variable types (Float, Int, String, Bool, etc)
        [Header("World Coordinates")]
        public float xPosition;
        public float yPosition;
        public float zPosition;
    }
}