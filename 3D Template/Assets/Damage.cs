using UnityEngine;

public class Damage : MonoBehaviour
{
    Health health;
    public float damageAmount = 1f;
    public float damageInterval = 1f;
    private float nextDamageTime = 0f;
    public GameObject player;
    public GameObject enemy;
    // Update is called once per frame
    void Update()
    {
      //if(Time.time >= nextDamageTime && health != null)
      //  {
      //      nextDamageTime = Time.time + damageInterval;
      //      Debug.Log("Player took damage: " + damageAmount);
      //  }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            health = other.GetComponent<Health>();
            health.TakeDamage(damageAmount);
            Debug.Log("Player entered damage zone");
        }
    }

}
