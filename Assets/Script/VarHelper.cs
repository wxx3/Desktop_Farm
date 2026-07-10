using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct VarValue
{
    public enum ValueType
    {
        IntValue,
        FloatValue,
        StringValue,
        BoolValue,
        None
    }


    public ValueType valueType;

    public int intValue;
    public float floatValue;
    public string stringValue;
    public bool boolValue;
}

public static class VarHelper
{
    public static VarValue PackValue(int value)
    {
        VarValue varValue = new VarValue()
        {
            valueType = VarValue.ValueType.IntValue,
            intValue = value
        };
        return varValue;
    }

    public static VarValue PackValue(float value)
    {
        VarValue varValue = new VarValue()
        {
            valueType = VarValue.ValueType.FloatValue,
            floatValue = value
        };
        return varValue;
    }

    public static VarValue PackValue(string value)
    {
        VarValue varValue = new VarValue()
        {
            valueType = VarValue.ValueType.StringValue,
            stringValue = value
        };
        return varValue;
    }

    public static VarValue PackValue(bool value)
    {
        VarValue varValue = new VarValue()
        {
            valueType = VarValue.ValueType.BoolValue,
            boolValue = value
        };
        return varValue;
    }

    public static int GetInt(VarValue value, int defaultValue = -1)
    {
        if (value.valueType == VarValue.ValueType.IntValue)
        {
            return value.intValue;
        }
        return defaultValue;
    }

    public static float GetFloat(VarValue value, float defaultValue = -1)
    {
        if (value.valueType == VarValue.ValueType.FloatValue)
        {
            return value.floatValue;
        }
        return defaultValue;
    }

    public static string GetString(VarValue value, string defaultValue = null)
    {
        if (value.valueType == VarValue.ValueType.StringValue)
        {
            return value.stringValue;
        }
        return defaultValue;
    }

    public static bool GetBool(VarValue value, bool defaultValue = false)
    {
        if(value.valueType == VarValue.ValueType.BoolValue)
        {
            return (value.boolValue);
        }
        return defaultValue;
    }

}
