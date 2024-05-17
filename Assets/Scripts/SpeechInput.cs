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
    [SerializeField] private float segmentDuration = 2.0f;
    [SerializeField] private float speechThreshold = 0.1f;

    [Header("Recording Settings")]
    [SerializeField] private AudioClip recording;
    [SerializeField] private bool isRecording = false;
    [SerializeField] private byte[] bytes;

    [Header("Linked GameObject")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Image;
    [SerializeField] private GameObject Image2;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] public CommandManager commandManager;




    void Start()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone found.");
            Image.SetActive(true);
 

        }
        else
        {
            Image2.SetActive(true);
            Debug.Log("Microphone Found");
            StartRecording();
        }
    }

    void Update()
    {

        if (isRecording)
        {
            CheckAndProcessRecording();
        }
    }


    private void StartRecording()
    {
        Debug.Log("Started Recording!!!");
        recording = Microphone.Start(null, false, Mathf.FloorToInt(segmentDuration), 44100);
        isRecording = true;

    }

    private void CheckAndProcessRecording()
    {
        
        int currentPosition = Microphone.GetPosition(null);
        if (currentPosition >= recording.samples)
        {
            Debug.Log("Processing");
            float[] samples = new float[recording.samples * recording.channels];
            recording.GetData(samples, 0);
            if (DetectSpeechActivity(samples))
            {
                
                byte[] audioData = EncodeAsWAV(samples, recording.frequency, recording.channels);
                SendRecording(audioData); // Send the recording, now expecting byte[] as input
            }
            Microphone.End(null); // Stop the current recording
            StartRecording(); // Immediately start another recording segment
        }
    }



    // Function to detect speech activity
    bool DetectSpeechActivity(float[] samples)
    {
        Debug.Log("Activity");
        float averageAmplitude = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            averageAmplitude += Mathf.Abs(samples[i]);
        }
        averageAmplitude /= samples.Length;
        Debug.Log($"Average Amplitude: {averageAmplitude}, Threshold: {speechThreshold}");

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

    private void SendRecording(byte[] audioData)
    {
        HuggingFaceAPI.AutomaticSpeechRecognition(audioData, response => {
            // Update UI with the response on success
            text.color = Color.white;
            text.text = response;
            HandleCommand(response.ToLower());
        }, error => {
            // Update UI with the error message on failure
            text.color = Color.red;
            text.text = error;
        });
    }

    private void HandleCommand(string command)
    {
        Debug.Log($"Handling command: {command}");
        // Parse the command and change state based on the recognized text
        if (command.Contains("die"))
        {
            commandManager.ChangeState(commandManager.laserState);
        }
        else if (command.Contains("laser"))
        {
            commandManager.ChangeState(commandManager.laserState);
        }
        else if (command.Contains("shield"))
        {
            commandManager.ChangeState(commandManager.bubbleState);
        }
        else if (command.Contains("run"))
        {
            commandManager.ChangeState(commandManager.runState);
        }
        // Add additional commands as needed
    }



}
