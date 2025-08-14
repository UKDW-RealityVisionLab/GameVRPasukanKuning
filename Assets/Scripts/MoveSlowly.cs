using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;

public class MoveSlowly : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    [SerializeField] private float moveSpeed = 1.0f;


    private async void Start()
    {
        await UnityServices.InitializeAsync();

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteConfig;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    private void ApplyRemoteConfig(ConfigResponse response)
    {
        if (RemoteConfigService.Instance.appConfig.HasKey("cubeSpeed"))
        {
            moveSpeed = RemoteConfigService.Instance.appConfig.GetFloat("cubeSpeed");
            Debug.Log("Remote config moveUpSpeed: " + moveSpeed);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
