using System;
using System.Linq;
using CMCore.Contracts;
using CMCore.Utilities.Extensions;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CMCore.Managers
{
    public class AudioManager
    {
        #region Properties & Fields

        private AudioSource[] _musicSources;
        private AudioSource _activeMusicSource;
        public AudioSource[] MusicSources
        {
            get
            {
                if (_musicSources != null && _musicSources.Length > 0) return _musicSources;
                var musicSources = GameObject.Find("MusicSources").GetComponentsInChildren<AudioSource>();
                return _musicSources = musicSources;

            }
        }


        private AudioSource[] _sfxSources;

        public AudioSource[] SfxSources
        {
            get
            {
                if (_sfxSources != null && _sfxSources.Length > 0) return _sfxSources;
                var sfxSources = GameObject.Find("SFXSources").GetComponentsInChildren<AudioSource>();
                return _sfxSources = sfxSources;


            }
        }

        #endregion

        public AudioManager()
        {
        }
        
        public void PlaySfx(string soundName)
        {
            var audioClip = GetClip(soundName);
            if (audioClip == null || !SettingsManager.SfxEnabled) return;

            foreach (var source in SfxSources)
            {
                if (source.isPlaying) continue;

                source.pitch = 1;
                source.volume = 1;
                source.clip = audioClip;
                source.Play();
                break;
            }
        }
        public void PlaySfx(string soundName, float volume)
        {
            var audioClip = GetClip(soundName);
            if (audioClip == null || !SettingsManager.SfxEnabled) return;

            foreach (var source in SfxSources)
            {
                if (source.isPlaying) continue;

                source.pitch = 1;
                source.volume = volume;
                source.clip = audioClip;
                source.Play();
                break;
            }
        }
        
        public void PlaySfx(string soundName, float volume, float pitch)
        {
            var audioClip = GetClip(soundName);
            if (audioClip == null || !SettingsManager.SfxEnabled) return;

            foreach (var source in SfxSources)
            {
                if (source.isPlaying) continue;

                source.pitch = pitch;
                source.volume = volume;
                source.clip = audioClip;
                source.Play();
                break;
            }
        }
        
        private AudioClip GetClip(string soundName)
        {
            var audioClip = Array.Find(GameManager.Instance.Core.Assets.AudioClips, clip => clip.name == soundName);
            return audioClip;
        }
    }
}