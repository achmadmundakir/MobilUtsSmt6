using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Hit Settings")]
    public float hitCooldown = 0.8f;

    private float lastHitTime = -999f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CountHit(other);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CountHit(other);
        }
    }

    void CountHit(Collider player)
    {
        if (Time.time - lastHitTime < hitCooldown)
        {
            return;
        }

        lastHitTime = Time.time;

        if (ObstacleManager.instance != null)
        {
            ObstacleManager.instance.AddHit();
        }

        CarController car = player.GetComponent<CarController>();

        if (car != null)
        {
            car.HitObstacle();
        }

        Debug.Log(gameObject.name + " tertabrak");
    }
}