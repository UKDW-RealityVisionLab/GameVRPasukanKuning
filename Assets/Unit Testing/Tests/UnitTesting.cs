using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UnitTesting
{
    private GameObject testObj;
    private MenuController controller;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        testObj = new GameObject("TestMenuController");
        controller = testObj.AddComponent<MenuController>();

        controller.downloadProgressSlider = new GameObject("Slider").AddComponent<Slider>();
        controller.downloadProgressPanel = new GameObject("Panel");
        controller.downloadProgressText = new GameObject("DownloadText").AddComponent<TextMeshProUGUI>();

        controller.downloadProgressText.gameObject.SetActive(false);
        controller.downloadProgressPanel.SetActive(false);

        Object.DontDestroyOnLoad(testObj);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(testObj);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShowDownloadUI_ShouldActivateUIElementsAndSetInitialText()
    {
        controller.ShowDownloadUI();

        yield return null; // wait one frame

        Assert.IsTrue(controller.downloadProgressPanel.activeSelf);
        Assert.IsTrue(controller.downloadProgressText.gameObject.activeSelf);
        Assert.AreEqual("Downloading...", controller.downloadProgressText.text);
        Assert.AreEqual(0f, controller.downloadProgressSlider.value);
    }

    [UnityTest]
    public IEnumerator ShowDownloadUI_ShouldNotActivateUIElementsAndSetInitialText()
    {
        // Set all components to null to test the 'else' (invalid input) paths
        controller.downloadProgressPanel = null;
        controller.downloadProgressSlider = null;
        controller.downloadProgressText = null;

        // Expect warning logs for each null component
        LogAssert.Expect(LogType.Warning, "ShowDownloadUI: downloadProgressPanel is null.");
        LogAssert.Expect(LogType.Warning, "ShowDownloadUI: downloadProgressSlider is null.");
        LogAssert.Expect(LogType.Warning, "ShowDownloadUI: downloadProgressText is null.");

        // Call the method under test
        controller.ShowDownloadUI();

        // Wait one frame to allow Unity to process and log the warnings
        yield return null;

        // No further assertions are needed since LogAssert ensures warnings were logged
    }



    [UnityTest]
    public IEnumerator UpdateDownloadUI_ShouldUpdateSliderAndText()
    {
        controller.UpdateDownloadUI(0.75f);

        yield return null; // wait one frame

        Assert.AreEqual(0.75f, controller.downloadProgressSlider.value);
        Assert.AreEqual("Downloading... 75%", controller.downloadProgressText.text);
    }

    [UnityTest]
    public IEnumerator UpdateDownloadUI_ShouldNotUpdateSliderAndText()
    {
        // Simulate invalid input by setting components to null
        controller.downloadProgressSlider = null;
        controller.downloadProgressText = null;

        // Expect warning messages to be logged
        LogAssert.Expect(LogType.Warning, "UpdateDownloadUI: downloadProgressSlider is null.");
        LogAssert.Expect(LogType.Warning, "UpdateDownloadUI: downloadProgressText is null.");

        // Call the method with a sample progress value
        controller.UpdateDownloadUI(0.5f);

        // Wait one frame to process UI logic and logs
        yield return null;

        // No assertion required beyond LogAssert as it validates the warning logs
    }


    [UnityTest]
    public IEnumerator ShowDownloadError_ShouldDisplayErrorMessage()
    {
        controller.ShowDownloadError();

        yield return null; // wait one frame

        Assert.IsTrue(controller.downloadProgressText.gameObject.activeSelf);
        Assert.AreEqual("Download Error!", controller.downloadProgressText.text);
        Assert.AreEqual(0f, controller.downloadProgressSlider.value);
    }

    [UnityTest]
    public IEnumerator ShowDownloadError_ShouldNotDisplayErrorMessage_WhenComponentsAreNull()
    {
        // Set UI components to null to simulate invalid input
        controller.downloadProgressText = null;
        controller.downloadProgressSlider = null;

        // Expect warning logs for each null component
        LogAssert.Expect(LogType.Warning, "ShowDownloadError: downloadProgressText is null.");
        LogAssert.Expect(LogType.Warning, "ShowDownloadError: downloadProgressSlider is null.");

        // Call the method to trigger the logic
        controller.ShowDownloadError();

        // Wait one frame to process the method and warnings
        yield return null;

        // No assertions required beyond LogAssert, as we're testing warnings
    }


    [UnityTest]
    public IEnumerator HandleSceneLoadResult_Success_ShouldNotShowError()
    {
        LogAssert.ignoreFailingMessages = true;
        string validSceneKey = "Assets/BundledAsset/level1/Level 1.unity";
        var handle = Addressables.LoadSceneAsync(validSceneKey, LoadSceneMode.Single);

        while (!handle.IsDone)
            yield return null;

        controller.HandleSceneLoadResult(handle);

        Assert.AreNotEqual("Download Error!", controller.downloadProgressText.text);
    }

    [UnityTest]
    public IEnumerator HandleSceneLoadResult_Failed_ShouldShowDownloadError()
    {
        LogAssert.ignoreFailingMessages = true;
        string invalidSceneKey = "Invalid/Scene/Path.unity";
        var handle = Addressables.LoadSceneAsync(invalidSceneKey, LoadSceneMode.Single);

        while (!handle.IsDone)
            yield return null;

        // Assert that the handle failed as expected (it should not succeed with an invalid scene key)
        Assert.AreEqual(AsyncOperationStatus.Failed, handle.Status, "Expected operation to fail for invalid key");

        // Handle the failure and update UI accordingly
        controller.HandleSceneLoadResult(handle);

        // Assert that the UI reflects the error
        Assert.AreEqual("Download Error!", controller.downloadProgressText.text);
        Assert.AreEqual(0f, controller.downloadProgressSlider.value);
    }
}
