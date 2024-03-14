using UnityEngine;

public class SpotController : MonoBehaviour
{
    public Transform[] fullLegs;           // Array of full legs (parent of upper and lower leg)
    public Transform[] upperLegs;          // Array of upper legs
    public Transform[] lowerLegs;          // Array of lower legs (knee)
    public float[] legOffsets;             // Time offset for each leg
    public float strideLength = 0.1f;      // Length of each step
    public float strideHeight = 0.05f;     // Height of each step
    public float stepDuration = 1f;        // Duration of each step
    public float lowerLegRotationAngle = 30f; // Angle of rotation for the lower leg

    void Update()
    {
        // Calculate the current time
        float currentTime = Time.time;

        // Loop through each leg and update its position and rotation
        for (int i = 0; i < fullLegs.Length; i++)
        {
            // Calculate the phase of the leg movement based on the leg offset and current time
            float phase = (currentTime * (1f / stepDuration) + legOffsets[i]) % 1f;

            // Calculate the rotation of the leg joints based on the phase
            float x = Mathf.Sin(2f * Mathf.PI * phase) * strideLength;
            Quaternion fullLegRotation = Quaternion.Euler(x * Mathf.Rad2Deg, 0f, 0f); // Rotate around the x-axis based on stride length

            // Apply rotation to the full leg
            fullLegs[i].localRotation = fullLegRotation;

            // Calculate the rotation of the lower leg relative to the upper leg's rotation
            Quaternion lowerLegRotation = Quaternion.Inverse(upperLegs[i].rotation) * fullLegRotation;

            // Apply the angle of rotation for the lower leg
            lowerLegRotation = Quaternion.Euler(0f, lowerLegRotationAngle, 0f); // Rotate around the Y-axis

            // Apply rotation to the lower leg
            lowerLegs[i].localRotation = lowerLegRotation;
        }
    }
}