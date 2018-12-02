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

    System.Random random;
    List<GameObject> Wounds;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHpValue();
        random = new System.Random();
        Wounds = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(int damage, Collision2D collision = null)
    {
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

        UpdateHpValue();
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
                wound.transform.SetParent(parentObject,false);
                wound.transform.localPosition = new Vector3(Random.Range(0, rect.rect.width), Random.Range(0, rect.rect.height), 0);
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
