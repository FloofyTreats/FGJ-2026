using System;
using System.Collections;
using UnityEngine;

public static class AudioClipReverse
{
    public static IEnumerator CreateReversedCoroutine(AudioClip original, Action<AudioClip> onDone)
    {
        if (original == null)
        {
            onDone?.Invoke(null);
            yield break;
        }

        original.LoadAudioData();

        while (original.loadState == AudioDataLoadState.Loading)
        {
            yield return null;
        }

        if (original.loadState != AudioDataLoadState.Loaded)
        {
            Debug.LogError($"AudioClip not loaded. loadState={original.loadState}. " +
                           "In WebGL, enable 'Preload Audio Data' and avoid Streaming.");
            onDone?.Invoke(null);
            yield break;
        }

        int channels = original.channels;
        int samples = original.samples;

        if (channels <= 0 || samples <= 0)
        {
            Debug.LogError("Clip reports 0 samples/channels even though it claims loaded.");
            onDone?.Invoke(null);
            yield break;
        }

        float[] data = new float[samples * channels];

        if (!original.GetData(data, 0))
        {
            Debug.LogError("Failed to read audio data via GetData(). " +
                           "Try: Preload Audio Data ON, Load In Background OFF, and/or PCM WAV import.");
            onDone?.Invoke(null);
            yield break;
        }

        for (int i = 0, j = samples - 1; i < j; i++, j--)
        {
            int iBase = i * channels;
            int jBase = j * channels;

            for (int c = 0; c < channels; c++)
            {
                (data[iBase + c], data[jBase + c]) = (data[jBase + c], data[iBase + c]);
            }
        }

        var reversed = AudioClip.Create(
            original.name + "_Reversed",
            samples,
            channels,
            original.frequency,
            false
        );

        reversed.SetData(data, 0);
        onDone?.Invoke(reversed);

    }
}
