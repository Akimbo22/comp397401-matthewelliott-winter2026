using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("What is the other? - " + other.gameObject.name);
        if (other.gameObject.name == "NPC")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
