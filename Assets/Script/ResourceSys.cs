using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceSys : SystemBase
{
    public GameObject GetRes(string resPath)
    {
        GameObject prefab = Resources.Load<GameObject>(resPath);

        if (prefab == null)
        {
            Debug.LogError($"在 Resources 目录下找不到预制体，路径：{resPath}");
            return null;
        }
        return prefab;
    }
}
