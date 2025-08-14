using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[TestFixture]
public class MenuControllerTest
{
    private MenuController menuController;
    private GameObject downloadProgressPanel;
    private Slider downloadProgressSlider;
    private TextMeshProUGUI downloadProgressText;

    [SetUp]
    public void SetUp()
    {
        // Initialize GameObjects
        GameObject gameObject = new GameObject();
        menuController = gameObject.AddComponent<MenuController>();

        downloadProgressPanel = new GameObject();
        downloadProgressSlider = downloadProgressPanel.AddComponent<Slider>();
        downloadProgressText = downloadProgressPanel.AddComponent<TextMeshProUGUI>();

        // Link to MenuController
        menuController.downloadProgressPanel = downloadProgressPanel;
        menuController.downloadProgressSlider = downloadProgressSlider;
        menuController.downloadProgressText = downloadProgressText;

        menuController.downloadProgressText.gameObject.SetActive(false);
        menuController.downloadProgressPanel.SetActive(false);

        Object.DontDestroyOnLoad(gameObject);
    }

    [Test]
    [TestCase(true, true, true, "Downloading...", 0f)] // All exist
    [TestCase(true, true, false, "", 0f)] // Text is null
    [TestCase(true, false, true, "", 0f)] // Slider is null
    [TestCase(true, false, false, "", 0f)] // Text and Slider are null
    [TestCase(false, true, true, "", 0f)] // Panel is null
    [TestCase(false, true, false, "", 0f)] // Panel and Text are null
    [TestCase(false, false, true, "", 0f)] // Panel and Slider are null
    [TestCase(false, false, false, "", 0f)] // All are null
    public void ShowDownloadUI_ParameterizedTest(
        bool panelExists,
        bool sliderExists,
        bool textExists,
        string expectedText,
        float expectedSliderValue
    )
    {
        // Arrange
        if (!panelExists) menuController.downloadProgressPanel = null;
        if (!sliderExists) menuController.downloadProgressSlider = null;
        if (!textExists) menuController.downloadProgressText = null;

        if (!panelExists) LogAssert.Expect(LogType.Warning, "ShowDownloadUI: downloadProgressPanel is null.");
        if (!sliderExists) LogAssert.Expect(LogType.Warning, "ShowDownloadUI: downloadProgressSlider is null.");
        if (!textExists) LogAssert.Expect(LogType.Warning, "ShowDownloadUI: downloadProgressText is null.");

        // Act
        menuController.ShowDownloadUI();

        // Assert
        if (panelExists && menuController.downloadProgressPanel != null)
            Assert.IsTrue(menuController.downloadProgressPanel.activeSelf);

        if (sliderExists && menuController.downloadProgressSlider != null)
            Assert.AreEqual(expectedSliderValue, menuController.downloadProgressSlider.value);

        if (textExists && menuController.downloadProgressText != null)
        {
            if (panelExists)
            {
                Assert.IsTrue(menuController.downloadProgressText.gameObject.activeSelf);
                Assert.AreEqual(expectedText, menuController.downloadProgressText.text);
            }
            else
            {
                Assert.IsFalse(menuController.downloadProgressText.gameObject.activeSelf);
            }
        }
    }


    [Test]
    [TestCase(true, true, 0.0f, "Downloading... 0%", 0.0f, null)]
    [TestCase(true, true, 0.25f, "Downloading... 25%", 0.25f, null)]
    [TestCase(true, true, 0.50f, "Downloading... 50%", 0.50f, null)]
    [TestCase(true, true, 0.75f, "Downloading... 75%", 0.75f, null)]
    [TestCase(true, true, 1.0f, "Downloading... 100%", 1.0f, null)]
    [TestCase(false, true, 0f, "", 0f, "UpdateDownloadUI: downloadProgressSlider is null.")]
    [TestCase(true, false, 0f, "", 0f, "UpdateDownloadUI: downloadProgressText is null.")]
    [TestCase(false, true, 0.50f, "", 0f, "UpdateDownloadUI: downloadProgressSlider is null.")]
    [TestCase(true, false, 0.50f, "", 0f, "UpdateDownloadUI: downloadProgressText is null.")]
    [TestCase(false, false, 0f, "", 0f, "UpdateDownloadUI: downloadProgressSlider is null.\nUpdateDownloadUI: downloadProgressText is null.")]
    public void UpdateDownloadUI_ParameterizedTest(
       bool sliderExists,
       bool textExists,
       float progress,
       string expectedText,
       float expectedSliderValue,
       string expectedWarnings
   )
    {
        // Arrange
        if (!sliderExists) menuController.downloadProgressSlider = null;
        if (!textExists) menuController.downloadProgressText = null;

        if (expectedWarnings != null)
        {
            var warnings = expectedWarnings.Split('\n');
            foreach (var warning in warnings)
            {
                LogAssert.Expect(LogType.Warning, warning);
            }
        }

        // Act
        menuController.UpdateDownloadUI(progress);

        // Assert
        if (sliderExists && menuController.downloadProgressSlider != null)
            Assert.AreEqual(expectedSliderValue, menuController.downloadProgressSlider.value);

        if (textExists && menuController.downloadProgressText != null)
        {
            Assert.AreEqual(expectedText, menuController.downloadProgressText.text);
        }
    }

    [Test]
    [TestCase(true, true, true, "Download Error!", 0f, "")] // EC1: Both are not null
    [TestCase(true, false, false, "", 0f, "ShowDownloadError: downloadProgressSlider is null.")] // EC2: Text is not null, Slider is null
    [TestCase(false, true, false, "", 0f, "ShowDownloadError: downloadProgressText is null.")] // EC3: Text is null, Slider is not null
    [TestCase(false, false, false, "", 0f, "ShowDownloadError: downloadProgressSlider is null.\nShowDownloadError: downloadProgressText is null.")] // EC4: Both are null
    public void ShowDownloadError_ParameterizedTest(
            bool textExists,
            bool sliderExists,
            bool expectedTextActive,
            string expectedText,
            float expectedSliderValue,
            string expectedWarnings
        )
    {
        // Arrange
        if (!textExists) menuController.downloadProgressText = null;
        if (!sliderExists) menuController.downloadProgressSlider = null;

        // Expect logs if warnings are supposed to happen
        if (!string.IsNullOrEmpty(expectedWarnings))
        {
            var warnings = expectedWarnings.Split('\n');
            foreach (var warning in warnings)
            {
                LogAssert.Expect(LogType.Warning, warning);
            }
        }

        // Act
        menuController.ShowDownloadError();

        // Assert
        if (textExists && sliderExists)
        {
            Assert.IsTrue(menuController.downloadProgressText.gameObject.activeSelf, "Text object should be active");
            Assert.AreEqual(expectedText, menuController.downloadProgressText.text, "Text should display 'Download Error!'");
            Assert.AreEqual(expectedSliderValue, menuController.downloadProgressSlider.value, "Slider value should be set to 0");
        }
        else if (textExists && !sliderExists)
        {
            Assert.IsFalse(menuController.downloadProgressText.gameObject.activeSelf, "Text object should not be active when slider is null.");
        }
        else if (!textExists && sliderExists)
        {
            Assert.AreEqual(expectedSliderValue, menuController.downloadProgressSlider.value, "Slider value should be set to 0");
        }
        else if (!textExists && !sliderExists)
        {
            Assert.Pass("No objects to activate or modify.");
        }
    }



    // Define test cases using ValueSource
    private static IEnumerable<TestCaseData> SceneLoadCases()
    {
        yield return new TestCaseData("Assets/BundledAsset/level1/Level 1.unity", AsyncOperationStatus.Succeeded, "", 1f)
            .SetName("Level 1 - Success");
        yield return new TestCaseData("Assets/BundledAsset/level2/Level 2.unity", AsyncOperationStatus.Succeeded, "", 1f)
            .SetName("Level 2 - Success");
        yield return new TestCaseData("Invalid/Scene/Path.unity", AsyncOperationStatus.Failed, "Download Error!", 0f)
            .SetName("Invalid Path - Failure");
    }

    [UnityTest]
    public IEnumerator HandleSceneLoadResult_ParameterizedTest(
     [ValueSource(nameof(SceneLoadCases))] TestCaseData testData)
    {
        LogAssert.ignoreFailingMessages = true;

        // Extract parameters from TestCaseData
        string sceneKey = (string)testData.Arguments[0];
        AsyncOperationStatus expectedStatus = (AsyncOperationStatus)testData.Arguments[1];
        string expectedErrorMessage = (string)testData.Arguments[2];
        float expectedSliderValue = (float)testData.Arguments[3];

        // Act: Attempt to load the scene
        var handle = Addressables.LoadSceneAsync(sceneKey, LoadSceneMode.Single);

        // Wait for the scene load to complete
        while (!handle.IsDone)
            yield return null;

        // Assert the expected status
        Assert.AreEqual(expectedStatus, handle.Status, $"Expected operation to be {expectedStatus}");

        // Setup UI elements to prevent null references
        menuController.downloadProgressText = new GameObject("DownloadText").AddComponent<TextMeshProUGUI>();
        menuController.downloadProgressSlider = new GameObject("DownloadSlider").AddComponent<Slider>();

        // Optional: set inactive if needed
        menuController.downloadProgressText.gameObject.SetActive(true);
        menuController.downloadProgressSlider.gameObject.SetActive(true);

        // Call the handler and update UI accordingly
        menuController.HandleSceneLoadResult(handle);

        // Assert UI behavior
        Assert.AreEqual(expectedErrorMessage, menuController.downloadProgressText.text ?? "", "Text message mismatch");
        Assert.AreEqual(expectedSliderValue, menuController.downloadProgressSlider.value, "Slider value mismatch");


        yield break;
    }

    private static IEnumerable<TestCaseData> LoadSceneWithProgressCases()
    {
        yield return new TestCaseData("Assets/BundledAsset/level1/Level 1.unity", AsyncOperationStatus.Succeeded, "", 1f)
            .SetName("EP1_ValidScene_Success");

        yield return new TestCaseData("Assets/BundledAsset/level2/Level 2.unity", AsyncOperationStatus.Succeeded, "", 1f)
            .SetName("Level 2 - Success");

        yield return new TestCaseData("Invalid/Path/Scene.unity", AsyncOperationStatus.Failed, "Download Error!", 0f)
            .SetName("EP3_InvalidPath_Failed");
    }


    [UnityTest]
    public IEnumerator LoadSceneWithProgress_ParameterizedTest(
    [ValueSource(nameof(LoadSceneWithProgressCases))] TestCaseData testData)
    {
        LogAssert.ignoreFailingMessages = true;

        string sceneKey = (string)testData.Arguments[0];
        AsyncOperationStatus expectedStatus = (AsyncOperationStatus)testData.Arguments[1];
        string expectedText = (string)testData.Arguments[2];
        float expectedSliderValue = (float)testData.Arguments[3];

        // Ensure menuController is initialized and persistent
        if (menuController == null)
        {
            var menuGO = new GameObject("MenuController");
            menuController = menuGO.AddComponent<MenuController>();
            Object.DontDestroyOnLoad(menuGO); // Prevent destruction on scene load
        }

        // Setup UI and persist it
        var panelGO = new GameObject("Panel");
        var sliderGO = new GameObject("Slider");
        var textGO = new GameObject("Text");

        Object.DontDestroyOnLoad(panelGO);
        Object.DontDestroyOnLoad(sliderGO);
        Object.DontDestroyOnLoad(textGO);

        menuController.downloadProgressPanel = panelGO;
        menuController.downloadProgressSlider = sliderGO.AddComponent<Slider>();
        menuController.downloadProgressText = textGO.AddComponent<TextMeshProUGUI>();

        // Act
        yield return menuController.TestableLoadScene(sceneKey);

        // Validate handle result
        var handle = Addressables.LoadSceneAsync(sceneKey, LoadSceneMode.Single);
        while (!handle.IsDone)
            yield return null;

        Assert.AreEqual(expectedStatus, handle.Status, "Scene load status mismatch");

        // Safety assertions
        Assert.IsNotNull(menuController.downloadProgressText, "downloadProgressText is null");
        Assert.IsNotNull(menuController.downloadProgressSlider, "downloadProgressSlider is null");

        // Get the actual UI values
        string actualText = menuController.downloadProgressText.text ?? "";
        string expectedSafe = string.IsNullOrEmpty(expectedText) ? "" : expectedText;

        Assert.AreEqual(expectedSafe, actualText, "Text mismatch");
        Assert.AreEqual(expectedSliderValue, menuController.downloadProgressSlider.value, "Slider mismatch");
    }
}