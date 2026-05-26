using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 18f;
    public float reverseSpeed = 8f;
    public float turnSpeed = 65f;

    [Header("Control Fix")]
    public bool invertForward = true;
    public bool invertTurn = true;

    [Header("Ground Snap Settings")]
    public float rayStartHeight = 5f;
    public float rayDistance = 20f;
    public float rideHeight = 0.55f;

    [Header("Start Control")]
    public bool canDrive = false;

    [Header("Obstacle Hit Settings")]
    public float hitPushBackDistance = 2f;
    public float hitStopDuration = 0.4f;

    private float move;
    private float turn;
    private bool isHitStopping = false;

    void Update()
    {
        SnapToGround();

        // Mobil tidak bisa bergerak sebelum countdown selesai
        // atau saat sedang terkena efek tabrakan obstacle
        if (!canDrive || isHitStopping)
        {
            return;
        }

        ReadInput();
        MoveCar();
        TurnCar();
    }

    void ReadInput()
    {
        move = 0f;
        turn = 0f;

        // Kontrol maju dan mundur
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            move = 1f;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            move = -1f;
        }

        // Kontrol belok kiri dan kanan
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turn = -1f;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turn = 1f;
        }

        // Digunakan karena arah model mobil sebelumnya terbalik
        if (invertForward)
        {
            move = -move;
        }

        if (invertTurn)
        {
            turn = -turn;
        }
    }

    void MoveCar()
    {
        if (move == 0f) return;

        float speed = move > 0 ? forwardSpeed : reverseSpeed;

        // Mobil hanya bergerak pada bidang datar agar tidak naik/turun sendiri
        Vector3 flatForward = transform.forward;
        flatForward.y = 0f;
        flatForward.Normalize();

        transform.position += flatForward * move * speed * Time.deltaTime;
    }

    void TurnCar()
    {
        if (turn == 0f) return;

        float turnAmount = turn * turnSpeed * Time.deltaTime;

        // Arah belok dibalik saat mobil mundur
        if (move < 0)
        {
            turnAmount = -turnAmount;
        }

        transform.Rotate(0f, turnAmount, 0f);
    }

    void SnapToGround()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * rayStartHeight;

        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, Vector3.down, rayDistance);

        if (hits.Length == 0) return;

        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            // Jangan membaca collider mobil sendiri
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
            {
                continue;
            }

            // Obstacle jangan dianggap sebagai jalan
            // supaya mobil tidak naik ke atas rintangan
            if (hit.collider.CompareTag("Obstacle"))
            {
                continue;
            }

            Vector3 newPosition = transform.position;
            newPosition.y = hit.point.y + rideHeight;
            transform.position = newPosition;

            return;
        }
    }

    public void EnableDrive()
    {
        canDrive = true;
    }

    public void DisableDrive()
    {
        canDrive = false;
    }

    public void HitObstacle()
    {
        if (!gameObject.activeInHierarchy) return;

        StartCoroutine(HitObstacleRoutine());
    }

    IEnumerator HitObstacleRoutine()
    {
        isHitStopping = true;

        // Mobil terdorong mundur saat terkena obstacle
        Vector3 pushDirection = -transform.forward;
        pushDirection.y = 0f;
        pushDirection.Normalize();

        transform.position += pushDirection * hitPushBackDistance;

        yield return new WaitForSeconds(hitStopDuration);

        isHitStopping = false;
    }
}