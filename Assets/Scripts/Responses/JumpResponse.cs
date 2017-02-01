using UnityEngine;
using System.Collections;
using System;

public class JumpResponse : Response
{
    Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    public override void Execute()
    {
        player.Jump();
    }
}
