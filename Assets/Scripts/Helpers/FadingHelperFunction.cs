using System.Collections;
using TMPro;
using UnityEngine;

namespace Helpers
{
    static class FadingHelperFunction
    {
        public delegate void CoroutineFinishedEvent();

        // i stole this method from a unity blog
        //https://johnleonardfrench.com/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/
        //added the deligate tho
        public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume, CoroutineFinishedEvent finishedEvent)
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }

            finishedEvent?.Invoke();

            yield break;
        }

        public static IEnumerator StartFade(TextMeshProUGUI textSource, float duration, float targetAlpha, CoroutineFinishedEvent finishedEvent)
        {
            float currentTime = 0;
            float start = textSource.alpha;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                textSource.alpha = Mathf.Lerp(start, targetAlpha, currentTime / duration);
                yield return null;
            }

            finishedEvent?.Invoke();

            yield break;
        }

    }
}
