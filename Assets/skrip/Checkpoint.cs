using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isPassed = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object masuk checkpoint: " + other.name);

        if (isPassed) return;

        if (other.CompareTag("Player"))
        {
            isPassed = true;

            GameManager.instance.AddCheckpoint();

            Debug.Log(gameObject.name + " dilewati");

            gameObject.SetActive(false);
        }
    }
}