using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class AudioRecorderVR : MonoBehaviour
{
    public AudioSource audioSource;
    public int sampleRate = 16000;
    public int recordingDurationInSeconds = 5;

    private AudioClip recordedAudio;
    private string microphoneDevice;

    private void Start()
    {
        XRSettings.enabled = true; // Make sure XR is enabled for Meta Quest 2

        // Get the default microphone device
        microphoneDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;

        if (microphoneDevice == null)
        {
            Debug.LogError("No microphone device found.");
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void StartRecording()
    {
        if (microphoneDevice != null)
        {
            recordedAudio = Microphone.Start(microphoneDevice, false, recordingDurationInSeconds, sampleRate);
        }
    }

    public void StopRecording()
    {
        if (Microphone.IsRecording(microphoneDevice))
        {
            Microphone.End(microphoneDevice);
        }
    }

    public void PlayRecordedAudio()
    {
        if (recordedAudio != null)
        {
            audioSource.clip = recordedAudio;
            audioSource.Play();
        }
    }
}

