using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public enum DeathType
    {
        Animation,
        Destroy
    }

    public DeathType Type;
    public Animator Animator;
    public string AnimationDeathParameter;
    public bool DeathParameterValue;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        switch (Type)
        {
            case DeathType.Animation:
                if (Animator != null)
                {
                    if(!System.String.IsNullOrEmpty(AnimationDeathParameter))
                    {
                        Animator.SetBool(AnimationDeathParameter, DeathParameterValue);
                    }
                    else
                    {
                        Debug.LogError($"{nameof(AnimationDeathParameter)} is not set");
                    }
                }
                else
                    Debug.LogError($"{nameof(Animator)} is not set");
                break;
            default:
                Destroy(gameObject);
                break;
        }

    }
}
