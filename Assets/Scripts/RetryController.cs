using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject menuPause;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    public void Retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        menuPause.SetActive(true);
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        menuPause.SetActive(true);
    }
}
