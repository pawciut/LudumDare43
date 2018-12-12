using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public bool IsPlayer;
    public bool CanBeWounded;
    public int MaxHealth;
    public int CurrentHealth;
    public UnityEngine.UI.Text HPValueText;

    public DeathController DeathController;
    public GameObject BigWound;
    public GameObject SmallWound;

    public Transform[] BigWoundsSpawnBox;
    public Transform[] SmallWoundsSpawnBox;

    public AudioClip[] DefaultHitSounds;

    System.Random random;
    List<GameObject> Wounds;

    List<GameObject> CollisionCooldown;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateHpValue();
        random = new System.Random();
        Wounds = new List<GameObject>();
        CollisionCooldown = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(int damage, float knockbackForce, Collision2D collision = null)
    {
        var collidedObject = collision.otherCollider.gameObject;
        if (CollisionCooldown.Contains(collidedObject))
            return;

        if (CanBeWounded)
        {
            AddWound(damage > 1, collision);
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

        PlayHitSound();
        UpdateHpValue();
        //var g1 = gameObject.GetInstanceID();
        //var g2 = collision.gameObject.GetInstanceID();
        //var g3 = collision.otherCollider.gameObject.GetInstanceID();
        //var g4 = collision.gameObject;


        if (!CollisionCooldown.Contains(collidedObject))
            CollisionCooldown.Add(collidedObject);
        StartCoroutine(RemoveCollisionCooldown(collidedObject, 0.5f));

        if (collision != null && knockbackForce > 0)
            AddKnockback(collision, knockbackForce);
    }

    void PlayHitSound()
    {
        if (DefaultHitSounds != null && DefaultHitSounds.Length > 0)
        {
            var clip = DefaultHitSounds[random.Next(0, DefaultHitSounds.Length - 1)];
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    void AddKnockback(Collision2D c, float knockbackForce)
    {
        // Calculate Angle Between the collision point and the player
        Vector2 dir = c.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
        // We then get the opposite (-Vector3) and normalize it
        dir = -dir.normalized;
        // And finally we add force in the direction of dir and multiply it by force. 
        // This will push back the player
        GetComponent<Rigidbody2D>().AddForce(dir * knockbackForce);
    }

    IEnumerator RemoveCollisionCooldown(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (CollisionCooldown.Contains(go))
            CollisionCooldown.Remove(go);
    }

    void AddWound(bool bigWound, Collision2D collision = null)
    {
        Debug.Log("Add wound");
        if (BigWound == null || SmallWound == null)
        {
            Debug.LogError("Wound prefabs not set");
            return;
        }
        if (collision != null)
        {
            foreach (var cp in collision.contacts)
            {
                //TODO:Spawn in random wound spawner

                GameObject wound;
                Transform parentObject = null;
                GameObject woundPrefab = null;
                RectTransform rect;

                if (bigWound)
                {
                    //Instantiate(bigWound ? BigWound : SmallWound, cp.point, Quaternion.identity);

                    rect = BigWoundsSpawnBox[0].GetComponent<RectTransform>();
                    woundPrefab = BigWound;
                }
                else
                {
                    rect = SmallWoundsSpawnBox[0].GetComponent<RectTransform>();
                    woundPrefab = SmallWound;
                }

                parentObject = rect.gameObject.transform;
                wound = Instantiate(woundPrefab, cp.point, Quaternion.identity);
                wound.transform.SetParent(parentObject, false);
                wound.transform.localPosition = new Vector3(Random.Range(0, rect.rect.width), Random.Range(0, rect.rect.height), 0);
                Wounds.Add(wound);
            }
        }
        else
        {
            Debug.LogError("Collision missing");
        }
    }

    void UpdateHpValue()
    {
        if (HPValueText != null)
            HPValueText.text = $"{CurrentHealth}/{MaxHealth}";
    }


}
