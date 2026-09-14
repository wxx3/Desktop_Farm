using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    VarValue GetProp(PropId propId);
    void OnCreate();
    void OnUpdate();
    void OnDestroy();
    void InitPops();
}
