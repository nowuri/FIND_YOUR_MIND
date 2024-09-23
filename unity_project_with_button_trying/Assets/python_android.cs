using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;
using System.Linq;

public class python_android : MonoBehaviour
{
    [Serializable]
    public class EmotionResponse
    {
        public string emotion { get; set; }  // 서버로부터 받은 감정 값
    }

    [SerializeField]
    //public GameObject[] facePoints;
    //public TextMeshProUGUI text1; // Display text for original emotion
    //public TextMeshProUGUI text2; // Display text for smoothed emotion

    public string emotionServerUrl = "http://192.168.140.16:5000/receive";  // Python 서버 URL

    private List<string> orgValues = new List<string>();
    private List<string> smoothValues = new List<string>();
    public string emotion;
    public bool strong = false;
    public float requestInterval = 1.0f;  // 요청 간격 (초)

    void Start()
    {
        Debug.Log("Start function initiated.");
        StartCoroutine(CheckServerStatus());
    }

    private IEnumerator CheckServerStatus()
    {
        Debug.Log("Checking if the server is up and running...");

        var request = new UnityWebRequest(emotionServerUrl, "HEAD");  // 서버 상태 확인을 위해 HEAD 요청
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Server is running. Proceeding with emotion data requests to {emotionServerUrl}");
            StartCoroutine(RequestEmotionDataRepeatedly());
        }
        else
        {
            Debug.LogError($"Failed to reach the server at {emotionServerUrl}. Error: {request.error}");
        }
    }

    private IEnumerator RequestEmotionDataRepeatedly()
    {
        while (true)  // 무한 반복 루프
        {
            yield return ReceiveEmotionFromServer();
            yield return new WaitForSeconds(requestInterval);  // 일정 간격 대기 후 다시 요청
        }
    }

    private IEnumerator ReceiveEmotionFromServer()
    {
        Debug.Log("ReceiveEmotionFromServer called. Waiting for emotion data...");

        var request = UnityWebRequest.Get(emotionServerUrl);  // GET 요청
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Received response from server: " + responseText);

            if (string.IsNullOrEmpty(responseText))
            {
                Debug.LogWarning("Received empty response from server.");
            }
            else
            {
                try
                {
                    EmotionResponse emotionResponse = JsonConvert.DeserializeObject<EmotionResponse>(responseText);
                    if (emotionResponse != null)
                    {
                        Debug.Log("EmotionResponse successfully deserialized: " + JsonConvert.SerializeObject(emotionResponse));
                        ProcessReceivedEmotion(emotionResponse);
                    }
                    else
                    {
                        Debug.LogWarning("Deserialized EmotionResponse is null.");
                    }
                }
                catch (JsonException jsonEx)
                {
                    Debug.LogError("JSON deserialization error: " + jsonEx.Message);
                }
            }
        }
        else
        {
            Debug.LogError("Request error: " + request.error);
        }
    }

    private void ProcessReceivedEmotion(EmotionResponse data)
    {
        Debug.Log("Processing received emotion...");

        if (data == null || string.IsNullOrEmpty(data.emotion))
        {
            Debug.LogWarning("Received data is null or empty.");
            return;
        }

        string receivedEmotion = data.emotion;

        orgValues.Add(receivedEmotion);
        smoothValues.Add(receivedEmotion);
        int count = orgValues.Count;

        Debug.Log("Original values count: " + count);

        // smoothing
        if (count >= 3)
        {
            List<string> recentValues = smoothValues.TakeLast(3).ToList();
            // 중간값이 앞뒤의 값과 같은 것이 없으면 smoothing
            if (recentValues[1] != recentValues[0] && recentValues[1] != recentValues[2])
            {
                smoothValues[count - 2] = recentValues[0];
            }
        }

        // emotion 결정, lv2를 위한 strong 계산
        emotion = smoothValues[Math.Max(0, count - 2)];
        try
        {
            if (emotion == smoothValues[count - 3] &&
                emotion == smoothValues[count - 4] &&
                emotion == smoothValues[count - 5] &&
                emotion == smoothValues[count - 6] &&
                emotion == smoothValues[count - 7] &&
                emotion == smoothValues[count - 8] &&
                emotion == smoothValues[count - 9]
                //emotion == smoothValues[count - 10] &&
                //emotion == smoothValues[count - 11] &&
                //emotion == smoothValues[count - 12] &&
                //emotion == smoothValues[count - 13] &&
                //emotion == smoothValues[count - 14] &&
                //emotion == smoothValues[count - 15]
                )
            {
                strong = true;
                UnityEngine.Debug.Log("strong, " + emotion);
            }
            else
            {
                strong = false;
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Failed to get strong index: " + e.Message);
        }

    }

}
