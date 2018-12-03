using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{

    public Canvas MenuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        MenuCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowMenu()
    {
        MenuCanvas.enabled = true;
        MenuCanvas.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Prologue");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
