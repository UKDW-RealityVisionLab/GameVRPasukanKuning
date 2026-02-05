using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public static class ApiClient
{
    // Ensure this URL is correct and ngrok is running
    private const string baseUrl = "https://roughy-patient-jolly.ngrok-free.app/ask";

    // 1. Added 'emotion' string to the method arguments
    public static IEnumerator PostQuery(string query, string kategoriUsia, string role, string lokasi, string emotion, System.Action<string> callback)
    {
        // 2. Added emotion to the new QueryData object
        string jsonPayload = JsonUtility.ToJson(new QueryData
        {
            query = query,
            kategori_usia = kategoriUsia,
            role = role,
            lokasi = lokasi,
            emotion = emotion
        });

        using (UnityWebRequest request = new UnityWebRequest(baseUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonText = request.downloadHandler.text;
                // Unity's JsonUtility ignores extra fields sent by the server (like 'time_to_retrieve'), 
                // so this is safe and correct.
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonText);
                callback?.Invoke(response.answer);
            }
            else
            {
                Debug.LogError("API Error: " + request.error + "\nResponse: " + request.downloadHandler.text);
                callback?.Invoke(null);
            }
        }
    }

    [System.Serializable]
    private class QueryData
    {
        public string query;
        public string kategori_usia;
        public string role;
        public string lokasi;
        public string emotion; // 3. Added this field to match Server
    }

    [System.Serializable]
    private class ApiResponse
    {
        public string answer;
    }
}