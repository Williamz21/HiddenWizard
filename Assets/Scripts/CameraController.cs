using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public int leftMargin = -33;
    public int rigthMargin = -33;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {
        if (player != null && player.transform.position.x>=leftMargin && player.transform.position.x<=rigthMargin)
        {
            Vector3 posicionObjetivo = player.transform.position;
            posicionObjetivo.y = transform.position.y;
            posicionObjetivo.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, posicionObjetivo, 123);
        }
    }
}
