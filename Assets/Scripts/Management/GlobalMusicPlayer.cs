using Helpers;
using UnityEngine;

namespace Management
{
    public class GlobalMusicPlayer : MonoBehaviour
    {
        public AudioSource mainAudioSource;
        public AudioSource interuptTrack;

        public float defaultFadeSeconds = 5;
        public bool defaultFade;

        [SerializeField]
        protected AudioClip currentClip;

        public AudioClip CurrentPlayingTrack
        {
            get => currentClip;
            protected set => currentClip = value;
        }

        protected AudioClip _nextClip;
        protected AudioClip _interuptClip;

        protected float _nextTrackFadeSeconds;
        protected bool _nextTrackCrossfade;
        protected float _trackVolume;

        public void Start()
        {
            _trackVolume = mainAudioSource.volume;
            mainAudioSource.volume = 0;
        }


        public void PlayNewAudio(AudioClip clip, bool fade = true, float fadeSeconds = 5f, bool crossfade = true)
        {
            if (fade)
            {
                _nextClip = clip;
                _nextTrackFadeSeconds = fadeSeconds;
                _nextTrackCrossfade = crossfade;
                _trackVolume = mainAudioSource.volume;

                StartCoroutine(FadingHelperFunction.StartFade(mainAudioSource, fadeSeconds, 0f, FadeoutFinishedEvent));
            }
            else
            {
                PlayNewTrackImmediate(clip);
            }
        }

        private void FadeoutFinishedEvent()
        {
            PlayNewTrackImmediate(_nextClip);
            StartCoroutine(FadingHelperFunction.StartFade(mainAudioSource, _nextTrackFadeSeconds, _trackVolume, null));
        }

        public void PlayNewTrackImmediate(AudioClip clip, bool resetVolume = false)
        {
            currentClip = clip;
            mainAudioSource.Stop();
            mainAudioSource.clip = clip;
            mainAudioSource.Play();

            if (resetVolume)
            {
                mainAudioSource.volume = _trackVolume;
            }
        }

        public void Pause() => mainAudioSource.Pause();

        public void Play()
        {
            if (mainAudioSource.time > 0f)
            {
                mainAudioSource.UnPause();
            }
            else
            {
                mainAudioSource.Play();
            }
        }
    }
}
