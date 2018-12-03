using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public bool CanBeDownJumped;

    public Collider2D MainCollider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void IgnoreCollisionsWith(Collider2D[] colliders, float duration)
    {
        StartCoroutine(DisableFeetColliders(colliders, duration));
    }

    IEnumerator DisableFeetColliders(Collider2D[] colliders, float disableTime)
    {
        if (colliders != null)
        {
            foreach (var feetCollider in colliders)
                Physics2D.IgnoreCollision(MainCollider, feetCollider, true);

            yield return new WaitForSeconds(disableTime);

            foreach (var feetCollider in colliders)
                Physics2D.IgnoreCollision(MainCollider, feetCollider, false);
        }
    }
}
