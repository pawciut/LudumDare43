using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObstacle : MonoBehaviour
{
    public int Damage;
    public float KnockbackForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var healthController = collision.gameObject.GetComponent<HealthController>();
        if (healthController != null)
            healthController.Damage(Damage, KnockbackForce, collision);
    }




}
