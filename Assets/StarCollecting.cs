using UnityEngine;

public class StarCollecting : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))  // Or use any tag your car has
        {
            Destroy(gameObject);
        }
    }
}
