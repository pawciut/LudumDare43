using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Svarog : MonoBehaviour
{
    public string[] TextDialogs;
    public AudioClip[] AudioDialogs;
    public float[] TextDuration;

    public UnityEngine.UI.Text ChatTextBox;
    public GameObject EyeCanvas;

    int pokeCount = 0;
    bool alreadySpoken = false;

    public AudioClip BlindSound;
    public string BlindText;
    public float BlindDuration;


    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pokeCount > 2)
            Blind();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!alreadySpoken)
        {
            alreadySpoken = true;
            StartCoroutine(Speak(TextDialogs,AudioDialogs,TextDuration));
        }

        if(other.gameObject.tag == "Spear")
        {
            var sc = other.gameObject.GetComponent<SpearHeadController>();
            if(sc.IsAttacking)
            {
                ++pokeCount;
            }

        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {

        if (c.gameObject.tag == "Spear")
        {

        }
    }

    IEnumerator Speak(string[] dialog, AudioClip[] dAudio, float[] dDuration)
    {
        for (int i = 0; i < dialog.Length; ++i)
        {
            ChatTextBox.text = dialog[i];
            audioSource.Stop();
            audioSource.clip = dAudio[i];
            audioSource.Play();


            yield return new WaitForSeconds(dDuration[i]);
            ChatTextBox.text = string.Empty;
        }
    }

    void Blind()
    {
        if(!EyeCanvas.active)
        {
            StartCoroutine(Speak2(BlindText, BlindSound, BlindDuration));
        }
        EyeCanvas.SetActive(true);
    }

    IEnumerator Speak2(string dialog, AudioClip dAudio, float dDuration)
    {
            ChatTextBox.text = dialog;
            audioSource.Stop();
            audioSource.clip = dAudio;
            audioSource.Play();


            yield return new WaitForSeconds(dDuration);
        ChatTextBox.text = string.Empty;
    }
}
