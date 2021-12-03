using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public static class BinaryUtils
{
	public static long BitArrayToBinary(IEnumerable<int> bitArray)
	{
		long value = 0;
		int i = 0;
		foreach (var bit in bitArray)
		{
			if (bit != 0)
				value += Convert.ToInt64(Math.Pow(2, i));
			i++;
		}

		return value;
	}

	public static long BitArrayToBinary(IEnumerable<bool> bitArray)
	{
		long value = 0;
		int i = 0;
		foreach (var bit in bitArray)
		{
			if (bit)
				value += Convert.ToInt64(Math.Pow(2, i));
			i++;
		}

		return value;
	}

	public static long StringToBinary(string bitArray, char trueCharacter)
	{
		long value = 0;
		for (int i = 0; i < bitArray.Length; i++)
		{
			if (bitArray[bitArray.Length - i - 1] == trueCharacter)
				value += Convert.ToInt64(Math.Pow(2, i));
		}

		return value;
	}
}