using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("What is the other? - " + other.gameObject.name);
        if (other.gameObject.name == "NPC")
        {
            Destroy(other.gameObject);
            //Destroy(gameObject);
            BulletObjectPool.Instance.ReturnToPool(this);
        }
    }
    private void OnEnable()
    {
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1.5f)
        {
            BulletObjectPool.Instance.ReturnToPool(this);
        }
    }
}
