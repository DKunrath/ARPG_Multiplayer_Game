using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DK
{
    public class WorldSoundFXManager : MonoBehaviour
    {
        public static WorldSoundFXManager Instance;

        [Header("Damage Sounds")]
        public AudioClip[] physicalDamageSFX;

        [Header("Action Sounds")]
        public AudioClip rollSFX;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public AudioClip ChooseRandomSFXFromArray(AudioClip[] arrayOfSounds)
        {
            int index = Random.Range(0, arrayOfSounds.Length);

            return arrayOfSounds[index];
        }
    }
}