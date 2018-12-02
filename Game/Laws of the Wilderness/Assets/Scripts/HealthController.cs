using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public bool IsPlayer;
    public bool CanBeWounded;
    public int MaxHealth;
    public int CurrentHealth;

    public DeathController DeathController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage, Collision2D collision = null)
    {
        if (CanBeWounded)
        {
            AddWound();
        }

        if (CurrentHealth - damage <= 0)
        {
            CurrentHealth = 0;
            if (DeathController == null)
                Debug.LogError($"{nameof(DeathController)} is null");
            DeathController.Die();
        }
        else
            CurrentHealth -= damage;

    }

    void AddWound()
    {

    }


}
