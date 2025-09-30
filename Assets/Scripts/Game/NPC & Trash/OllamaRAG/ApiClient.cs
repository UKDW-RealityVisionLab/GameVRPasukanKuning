using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using UnityEditor.AddressableAssets.HostingServices;

public static class ApiClient
{
    private const string baseUrl = "https://roughy-patient-jolly.ngrok-free.app/ask";

    public static IEnumerator PostQuery(string query, string kategoriUsia, string role, string lokasi, System.Action<string> callback)
    {
        string jsonPayload = JsonUtility.ToJson(new QueryData { query = query, kategori_usia = kategoriUsia, role = role, lokasi = lokasi });

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
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonText);
                callback?.Invoke(response.answer);  // ✅ Only return the "answer"
            }
            else
            {
                Debug.LogError("API Error: " + request.error);
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
    }

    [System.Serializable]
    private class ApiResponse
    {
        public string answer;
    }
}