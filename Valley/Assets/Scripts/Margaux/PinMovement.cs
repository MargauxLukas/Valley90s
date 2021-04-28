using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinMovement : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        transform.position = new Vector2(player.position.x, player.position.z)*0.1f + new Vector2(100,0);
    }
}
