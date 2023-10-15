using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    private Slider slider;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        MaxLife();
        ChangeLife(playerController.lives);
    }

    // Update is called once per frame
    public void MaxLife()
    {
        slider.maxValue = playerController.lives;
    }

    public void ChangeLife(float life)
    {
        slider.value = life;
    }
}
