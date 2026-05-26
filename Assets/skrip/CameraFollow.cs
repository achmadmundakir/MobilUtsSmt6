using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 5f, 8f);
    public float lookHeight = 1.5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + target.TransformDirection(offset);

        // Kamera langsung mengikuti posisi mobil agar tidak delay/dandet
        transform.position = targetPosition;

        transform.LookAt(target.position + Vector3.up * lookHeight);
    }
}