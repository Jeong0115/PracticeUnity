using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using UnityEngine;

public class Function
{
    static string[] numberScales = { "", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��" };

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

        if (unitIndex == 0) // �� ���� �̸��̸� �׳� �� ��ȯ
        {
            return value.ToString();
        }

        BigInteger primaryValue = value / BigInteger.Pow(unitValue, unitIndex);
        BigInteger secondaryValue = (value % BigInteger.Pow(unitValue, unitIndex)) / BigInteger.Pow(unitValue, unitIndex - 1);

        if (secondaryValue == 0) // �� ��° ���� ���� 0�̸� ù ��° ������ ���
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

        if (unitIndex == 0) // �� ���� �̸��̸� �׳� �� ��ȯ
        {
            return integerValue.ToString();
        }

        BigInteger primaryValue = integerValue / BigInteger.Pow(unitValue, unitIndex);
        BigInteger secondaryValue = (integerValue % BigInteger.Pow(unitValue, unitIndex)) / BigInteger.Pow(unitValue, unitIndex - 1);

        if (secondaryValue == 0) // �� ��° ���� ���� 0�̸� ù ��° ������ ���
        {
            return $"{primaryValue}{numberScales[unitIndex]}";
        }
        else
        {
            return $"{primaryValue}{numberScales[unitIndex]}{secondaryValue}{numberScales[unitIndex - 1]}";
        }
    }
}