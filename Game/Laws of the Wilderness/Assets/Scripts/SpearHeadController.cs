using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearHeadController : MonoBehaviour
{

    public HunterPlayerController PlayerScript;

    public bool IsAttacking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsAttacking = PlayerScript.isAttacking || PlayerScript.isThrowing;
    }
}
