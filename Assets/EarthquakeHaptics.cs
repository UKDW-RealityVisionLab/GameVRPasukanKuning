using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using System.Collections;

public class EarthquakeHaptics : MonoBehaviour
{
    public HapticImpulsePlayer leftHapticPlayer;
    public HapticImpulsePlayer rightHapticPlayer;

    [Range(0f, 1f)]
    public float vibrationAmplitude = 0.5f;
    public float pulseInterval = 0.1f; // small delay between repeated pulses

    private Coroutine hapticCoroutine;

    public void TriggerHapticPulse()
    {
        if (hapticCoroutine != null)
            StopCoroutine(hapticCoroutine);

        hapticCoroutine = StartCoroutine(HapticLoop());
    }

    public void StopHapticPulse()
    {
        if (hapticCoroutine != null)
        {
            StopCoroutine(hapticCoroutine);
            hapticCoroutine = null;
        }

        // Immediately send a zero impulse to stop any continuous rumble feeling
        leftHapticPlayer?.SendHapticImpulse(0f, 0.01f);
        rightHapticPlayer?.SendHapticImpulse(0f, 0.01f);
    }

    private IEnumerator HapticLoop()
    {
        while (true)
        {
            if (leftHapticPlayer != null)
                leftHapticPlayer.SendHapticImpulse(vibrationAmplitude, pulseInterval);

            if (rightHapticPlayer != null)
                rightHapticPlayer.SendHapticImpulse(vibrationAmplitude, pulseInterval);

            yield return new WaitForSeconds(pulseInterval);
        }
    }
}
