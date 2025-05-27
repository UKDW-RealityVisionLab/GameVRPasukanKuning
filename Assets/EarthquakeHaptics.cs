using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class EarthquakeHaptics : MonoBehaviour
{
    public HapticImpulsePlayer leftHapticPlayer;
    public HapticImpulsePlayer rightHapticPlayer;

    public float vibrationDuration = 1f;
    [Range(0f, 1f)]
    public float vibrationAmplitude = 0.5f;


    public void TriggerHapticPulse()
    {
        if (leftHapticPlayer != null)
            leftHapticPlayer.SendHapticImpulse(vibrationAmplitude, vibrationDuration);

        if (rightHapticPlayer != null)
            rightHapticPlayer.SendHapticImpulse(vibrationAmplitude, vibrationDuration);
    }
}
