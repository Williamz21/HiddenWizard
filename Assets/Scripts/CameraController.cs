using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public int leftMargin;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {
        if (player != null && player.transform.position.x>=leftMargin)
        {
            Vector3 posicionObjetivo = player.transform.position;
            posicionObjetivo.y = transform.position.y;
            posicionObjetivo.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, posicionObjetivo, 123);
        }
    }
}
