using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextUtils
{
    /// <summary>
    /// Convert Int Number to String <br>ex: 1202 => 1.2K </br>
    /// </summary>
    /// <param name="number"></param>
    /// <param name="decimalPlaces"></param>
    /// <returns></returns>
    public static string ToKString(int number, int decimalPlaces = 1)
    {
        if (number < 1000)
        {
            return number.ToString();
        }

        double absNumber = Math.Abs(number);
        double value = number / 1000.0;
        string suffix = "K";

        string formatString = "0";
        if (decimalPlaces > 0)
        {
            formatString += "." + new string('0', decimalPlaces);
        }

        return value.ToString(formatString) + suffix;
    }

    public static string TimeToMMSS(float timeValue)
    {
        int totalSeconds = (int)Math.Ceiling(timeValue);
        
        return $"{totalSeconds / 60 :D2}:{totalSeconds % 60 :D2}";
    }

    public static string TimeToHHMMSS(float timeValue)
    {
        int totalSeconds = (int)Math.Ceiling(timeValue);

        return $"{totalSeconds / 3600: D2}:{totalSeconds / 60: D2}:{totalSeconds % 60: D2}";
    }
}
