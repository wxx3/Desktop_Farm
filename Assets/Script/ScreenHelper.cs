using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHelper
{
    private static Camera m_MainCamera;

    private static Camera MainCamera
    {
        get
        {
            if(m_MainCamera == null){
                m_MainCamera = Camera.main;
            }
            return m_MainCamera;
        }
    }

    public static float Bottom
    {
        get
        {
            return MainCamera.ViewportToWorldPoint(new Vector2(0, 0)).y;
        }
    }
    public static float Left
    {
        get
        {
            return MainCamera.ViewportToWorldPoint(new Vector2(0, 0)).x;
        }
    }
    public static float Right
    {
        get
        {
            return MainCamera.ViewportToWorldPoint(new Vector2(1, 0)).x;
        }
    }
}
