using Packets;
using System;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    public void SetPosition(PlayerPacket data)
    {
        Vector2 pos = transform.position;
        pos.x = data.X; 
        pos.y = data.Y;

        transform.position = pos;
    }
}
