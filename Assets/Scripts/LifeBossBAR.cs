using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBossBAR : MonoBehaviour
{
    private Slider slider;
    public BossController bossController;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        MaxLife();
        ChangeLife(bossController.vida);
    }

    // Update is called once per frame
    public void MaxLife()
    {
        slider.maxValue = bossController.vida;
    }

    public void ChangeLife(float life)
    {
        slider.value = life;
    }
}
