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

    [Header("Joystick Mobile")]
    public Joystick joystick;

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


        // Mobil tidak bisa jalan sebelum countdown selesai
        // atau saat terkena obstacle
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


        // =====================
        // KEYBOARD WASD
        // =====================

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            move = 1f;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            move = -1f;
        }


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turn = -1f;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turn = 1f;
        }



        // =====================
        // MOBILE JOYSTICK
        // =====================

        if (joystick != null)
        {

            // maju mundur joystick
            if (Mathf.Abs(joystick.Vertical) > 0.1f)
            {
                move = joystick.Vertical;
            }


            // belok joystick
            if (Mathf.Abs(joystick.Horizontal) > 0.1f)
            {
                turn = joystick.Horizontal;
            }

        }



        // =====================
        // FIX ARAH MOBIL
        // =====================

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


        Vector3 flatForward = transform.forward;

        flatForward.y = 0f;

        flatForward.Normalize();


        transform.position += flatForward * move * speed * Time.deltaTime;
    }




    void TurnCar()
    {
        if (turn == 0f) return;


        float turnAmount = turn * turnSpeed * Time.deltaTime;


        // belok saat mundur dibalik
        if (move < 0)
        {
            turnAmount = -turnAmount;
        }


        transform.Rotate(0f, turnAmount, 0f);
    }




    void SnapToGround()
    {

        Vector3 rayOrigin = transform.position + Vector3.up * rayStartHeight;


        RaycastHit[] hits = Physics.RaycastAll(
            rayOrigin,
            Vector3.down,
            rayDistance
        );


        if (hits.Length == 0) return;



        System.Array.Sort(
            hits,
            (a,b)=>a.distance.CompareTo(b.distance)
        );



        foreach(RaycastHit hit in hits)
        {

            // jangan kena mobil sendiri
            if(hit.transform == transform ||
               hit.transform.IsChildOf(transform))
            {
                continue;
            }



            // obstacle jangan dianggap jalan
            if(hit.collider.CompareTag("Obstacle"))
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

        if(!gameObject.activeInHierarchy)
            return;


        StartCoroutine(HitObstacleRoutine());

    }




    IEnumerator HitObstacleRoutine()
    {

        isHitStopping = true;



        Vector3 pushDirection = -transform.forward;

        pushDirection.y = 0f;

        pushDirection.Normalize();



        transform.position += pushDirection * hitPushBackDistance;



        yield return new WaitForSeconds(hitStopDuration);



        isHitStopping = false;

    }

}