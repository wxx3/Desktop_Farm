using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICompenent
{
    void Bind(EntityBase entity);
    void OnUpdate();

    void OnDestroy();
    
}
