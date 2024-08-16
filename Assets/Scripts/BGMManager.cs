using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMManager : MonoBehaviour
{
    public AudioMixer mixer;
    private string exposedParamName = "SoundtrackVol";

    public static BGMManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static void Fade(float duration, float targetVolume)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.FadeBGMGroup(duration, targetVolume));
    }

    IEnumerator FadeBGMGroup(float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        mixer.GetFloat(exposedParamName, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            mixer.SetFloat(exposedParamName, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        mixer.SetFloat(exposedParamName, Mathf.Log10(targetValue) * 20);

        yield break;
    }
}
