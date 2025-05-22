using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public static class ApiClient
{
    private const string baseUrl = "https://roughy-patient-jolly.ngrok-free.app/ask";

    public static IEnumerator PostQuery(string query, string kategoriUsia, System.Action<string> callback)
    {
        string jsonPayload = JsonUtility.ToJson(new QueryData { query = query, kategori_usia = kategoriUsia });

        using (UnityWebRequest request = new UnityWebRequest(baseUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            Debug.Log("Disini kepanggil ko");
            if (request.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke(request.downloadHandler.text);
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
    }
}
