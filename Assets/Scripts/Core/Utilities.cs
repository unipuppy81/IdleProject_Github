using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class Utilities
{
    public static T GetOrAddComponent<T>(GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() ?? obj.AddComponent<T>();
    }

    public static string GenerateID()
    {
        StringBuilder builder = new();
        Enumerable
            .Range(65, 26)
            .Select(e => ((char)e).ToString())
            .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
            .OrderBy(e => Guid.NewGuid())
            .Take(11)
            .ToList().ForEach(e => builder.Append(e));

        return builder.ToString();
    }

    #region NumberConvert

    public static string ConvertToString(long number) => Convert(number);
    private static string Convert(long number)
    {
        if (number < 1_000)
        {
            return number.ToString();
        }
        string[] numSymbol = { "", "A ", "B  ", "C ", "D ", "E ", "F", "G", "H", "I", "J", "K" };
        int magnitudeIndex = (int)Mathf.Log10(number) / 3;
        StringBuilder sb = new StringBuilder()
        
            .Append((number * Mathf.Pow(0.001f, magnitudeIndex)).ToString("N2"))
            .Append(numSymbol[magnitudeIndex]);

        return sb.ToString();
    }

    #endregion

    #region Download

    public static long OneGB = 1000000000;
    public static long OneMB = 1000000;
    public static long OneKB = 1000;

    public static SizeUnits GetProperByteUnit(long byteSize)
    {
        if (byteSize >= OneGB)
        {
            return SizeUnits.GB;
        }
        else if (byteSize >= OneMB) 
        {
            return SizeUnits.MB;
        }
        else if (byteSize >= OneKB)
        {
            return SizeUnits.KB;
        }

        return SizeUnits.Bytes;
    }

    /// <summary>
    /// 바이트를 단위 사이즈에 맞게 숫자로 변환
    /// </summary>
    public static long ConvertByteByUnit(long byteSize, SizeUnits unit)
    {
        return (long)(byteSize / (double)Math.Pow(1024, (long)unit));
    }

    #endregion

    private static string SetTierColor(ItemTier itemTier)
    {
        switch (itemTier)
        {
            case ItemTier.Common:
                return "#C3C2C5";
            case ItemTier.Uncommon:
                return "#93DCC3";
            case ItemTier.Rare:
                return "#56BAF8";
            case ItemTier.epic:
                return "#D500FF";
            case ItemTier.Legendary:
                return "#FFD150";
            default:
                return "등록되지 않은 티어";
        }
    }

    public static Color SetSlotTierColor(ItemTier itemTier)
    {
        ColorUtility.TryParseHtmlString(SetTierColor(itemTier), out Color color);
        return color;
    }
}