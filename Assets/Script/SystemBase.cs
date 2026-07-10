using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemBase : ISystem
{
    protected GameLuncher luncher;

    public virtual void Init(GameLuncher gameLuncher)
    {
        luncher = gameLuncher;
    }
    public virtual void Update()
    {

    }
    public virtual void Reset()
    {

    }
}
