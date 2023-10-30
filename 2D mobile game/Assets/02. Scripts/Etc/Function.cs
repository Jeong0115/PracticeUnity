using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using UnityEngine;

public class Function
{
    static string[] numberScales = { "", "만", "억", "조", "경", "해", "자", "양", "구", "간", "정" };

    public static string BigIntegerToString(BigInteger value)
    {
        BigInteger unitValue = 10000;
        int unitIndex = 0;

        for (int i = numberScales.Length - 1; i >= 0; i--)
        {
            if (value >= BigInteger.Pow(unitValue, i))
            {
                unitIndex = i;
                break;
            }
        }

        if (unitIndex == 0) // 만 단위 미만이면 그냥 값 반환
        {
            return value.ToString();
        }

        BigInteger primaryValue = value / BigInteger.Pow(unitValue, unitIndex);
        BigInteger secondaryValue = (value % BigInteger.Pow(unitValue, unitIndex)) / BigInteger.Pow(unitValue, unitIndex - 1);

        if (secondaryValue == 0) // 두 번째 단위 값이 0이면 첫 번째 단위만 출력
        {
            return $"{primaryValue}{numberScales[unitIndex]}";
        }
        else
        {
            return $"{primaryValue}{numberScales[unitIndex]}{secondaryValue}{numberScales[unitIndex - 1]}";
        }
    }
    public static string FloatToString(float value)
    {
        BigInteger integerValue = (BigInteger)value;
        BigInteger unitValue = 10000;
        int unitIndex = 0;

        for (int i = numberScales.Length - 1; i >= 0; i--)
        {
            if (integerValue >= BigInteger.Pow(unitValue, i))
            {
                unitIndex = i;
                break;
            }
        }

        if (unitIndex == 0) // 만 단위 미만이면 그냥 값 반환
        {
            return integerValue.ToString();
        }

        BigInteger primaryValue = integerValue / BigInteger.Pow(unitValue, unitIndex);
        BigInteger secondaryValue = (integerValue % BigInteger.Pow(unitValue, unitIndex)) / BigInteger.Pow(unitValue, unitIndex - 1);

        if (secondaryValue == 0) // 두 번째 단위 값이 0이면 첫 번째 단위만 출력
        {
            return $"{primaryValue}{numberScales[unitIndex]}";
        }
        else
        {
            return $"{primaryValue}{numberScales[unitIndex]}{secondaryValue}{numberScales[unitIndex - 1]}";
        }
    }
}