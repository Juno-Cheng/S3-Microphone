using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using HuggingFace.API;
using TMPro;


public class SpeechInput : MonoBehaviour
{
    [Header("Speech Detection Settings")]
    [SerializeField] private string keywordToDetect = "activate";
    [SerializeField] private float segmentDuration = 5.0f;
    [SerializeField] private float speechThreshold = 0.1f;

    [Header("Recording Settings")]
    [SerializeField] private AudioClip recording;
    [SerializeField] private bool isRecording = false;
    [SerializeField] private byte[] bytes;

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
            StartRecording();
        }
        // Stop recording if recording and speech activity ends
        else if (isRecording && !DetectSpeechActivity() && Microphone.GetPosition(null) >= recording.samples)
        {
            StopRecording();
        }

        
    }


    private void StartRecording()
    {
        recording = Microphone.Start(null, false, Mathf.FloorToInt(segmentDuration), 44100);
        isRecording = true;

    }

    private void StopRecording()
    {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * recording.channels];
        recording.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, recording.frequency, recording.channels);
        isRecording = false;
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

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples)
                {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }


}
