using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedScript : MonoBehaviour
{
    public void ChangeGameSpeed(float speed)
    {
        Time.timeScale = speed;
    }
}
