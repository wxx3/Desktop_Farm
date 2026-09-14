using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class RandomHelper
{
    private static readonly System.Random m_random = new System.Random();

    public static int RangeInt(int min, int max)
    {
        return m_random.Next(min, max);
    }

    public static float RangeFloat(float min, float max)
    {
        return (float)(m_random.NextDouble() * (max - min) + min);
    }

    public static bool Chance (float chance)
    {
        return m_random.NextDouble() <= chance;
    }
}
