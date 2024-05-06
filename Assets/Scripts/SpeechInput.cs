using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechInput : MonoBehaviour
{
    [Header("Speech Detection Settings")]
    [SerializeField] private string keywordToDetect = "activate";
    [SerializeField] private float segmentDuration = 1.0f;
    [SerializeField] private float speechThreshold = 0.1f;

    [Header("Recording Settings")]
    [SerializeField] private AudioClip recording;
    [SerializeField] private bool isRecording = false;

    [Header("Linked GameObject")]
    [SerializeField] private GameObject Player;


    void Start()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone found.");
            return;
        }
    }

    void Update()
    {
        // Start recording if not already recording and speech is detected
        if (!isRecording && DetectSpeechActivity())
        {
            recording = Microphone.Start(null, false, Mathf.FloorToInt(segmentDuration), 44100);
            isRecording = true;
        }
        // Stop recording if recording and speech activity ends
        else if (isRecording && !DetectSpeechActivity())
        {
            Microphone.End(null);
            isRecording = false;
        }

        // Process audio if recording is in progress
        if (isRecording)
        {
            // Read audio data from the microphone
            float[] audioData = new float[recording.samples * recording.channels];
            recording.GetData(audioData, 0);

            // PROCESS/FUNCTION CALL
        }
    }

    // Function to detect speech activity
    bool DetectSpeechActivity()
    {
        // Read audio data from the microphone
        float[] audioData = new float[Mathf.FloorToInt(segmentDuration * 44100)];
        int currentPosition = Microphone.GetPosition(null);
        recording.GetData(audioData, currentPosition);

        // Calculate average amplitude
        float averageAmplitude = 0f;
        for (int i = 0; i < audioData.Length; i++)
        {
            averageAmplitude += Mathf.Abs(audioData[i]);
        }
        averageAmplitude /= audioData.Length;

        // Check if average amplitude exceeds the speech threshold
        return averageAmplitude > speechThreshold;
    }
}
