using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRollSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.Instance.rollSFX);
        }
    }
}