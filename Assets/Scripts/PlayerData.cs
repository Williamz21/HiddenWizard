using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public int speed;
    public bool died;
    public bool fire;
    public bool time;
    public int mode;
    public float lives;

    public PlayerData(PlayerController player){
        speed = player.speed;
        died = player.died;
        fire = player.fire;
        time = player.time;
        mode = player.mode;
        lives = player.lives;
    }
}
