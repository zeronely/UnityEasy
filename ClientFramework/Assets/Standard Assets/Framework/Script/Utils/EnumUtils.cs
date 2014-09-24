using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
public class EnumUtils
{
	private static Dictionary<Type, Dictionary<string, object>> s_enumCache = new Dictionary<Type, Dictionary<string, object>>();
	public static string GetString<T>(T enumVal)
	{
		string text = enumVal.ToString();
		FieldInfo field = enumVal.GetType().GetField(text);
		DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
		if (array.Length > 0)
		{
			return array[0].Description;
		}
		return text;
	}
	public static T GetEnum<T>(string str)
	{
		return EnumUtils.GetEnum<T>(str, StringComparison.Ordinal);
	}
	public static T GetEnum<T>(string str, StringComparison comparisonType)
	{
		Type typeFromHandle = typeof(T);
		Dictionary<string, object> dictionary;
		EnumUtils.s_enumCache.TryGetValue(typeFromHandle, out dictionary);
		object obj;
		if (dictionary != null && dictionary.TryGetValue(str, out obj))
		{
			return (T)((object)obj);
		}
		IEnumerator enumerator = Enum.GetValues(typeFromHandle).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				T t = (T)((object)enumerator.Current);
				string @string = EnumUtils.GetString<T>(t);
				if (@string.Equals(str, comparisonType))
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, object>();
						EnumUtils.s_enumCache.Add(typeFromHandle, dictionary);
					}
					if (!dictionary.ContainsKey(str))
					{
						dictionary.Add(str, t);
					}
					return t;
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		string message = string.Format("EnumUtils.GetEnum() - {0} has no matching value in enum {1}", str, typeFromHandle);
		throw new ArgumentException(message);
	}
	public static bool TryGetEnum<T>(string str, out T outVal)
	{
		return EnumUtils.TryGetEnum<T>(str, StringComparison.Ordinal, out outVal);
	}
	public static bool TryGetEnum<T>(string str, StringComparison comparisonType, out T outVal)
	{
		outVal = default(T);
		bool result;
		try
		{
			outVal = EnumUtils.GetEnum<T>(str, comparisonType);
			result = true;
		}
		catch (ArgumentException)
		{
			result = false;
		}
		return result;
	}
	public static T Parse<T>(string str)
	{
		return (T)((object)Enum.Parse(typeof(T), str));
	}
	public static T SafeParse<T>(string str)
	{
		T result;
		try
		{
			result = (T)((object)Enum.Parse(typeof(T), str));
		}
		catch (Exception)
		{
			result = default(T);
		}
		return result;
	}
	public static bool TryCast<T>(object inVal, out T outVal)
	{
		outVal = default(T);
		bool result;
		try
		{
			outVal = (T)((object)inVal);
			result = true;
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}
	public static int Length<T>()
	{
		return Enum.GetValues(typeof(T)).Length;
	}
}
