using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    public void MaxLife(float life)
    {
        slider.maxValue = life;
    }

    public void ChangeLife(float life)
    {
        slider.value = life;
    }

    public void SetLife(float life)
    {
        MaxLife(life);
        ChangeLife(life);
    }
}
