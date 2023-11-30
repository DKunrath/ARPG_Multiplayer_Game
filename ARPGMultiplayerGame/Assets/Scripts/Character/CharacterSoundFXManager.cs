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

        public void PlaySoundFX(AudioClip soundFX, float volume = 1, bool randomizePitch = true, float pitchRandom = 0.1f)
        {
            audioSource.PlayOneShot(soundFX, volume);
            // Resets our pitch
            audioSource.pitch = 1;

            if (randomizePitch)
            {
                audioSource.pitch += Random.Range(-pitchRandom, pitchRandom);
            }
        }

        public void PlayRollSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.Instance.rollSFX);
        }
    }
}