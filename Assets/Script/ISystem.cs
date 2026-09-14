using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISystem
{
    void Init(GameLuncher gameLuncher);
    void Update();
    void Reset();
}
