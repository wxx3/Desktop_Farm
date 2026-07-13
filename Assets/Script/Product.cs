using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : EntityBase
{
    public override void OnCreate()
    {
        InstanceId = Animal.sid;
    }
}
