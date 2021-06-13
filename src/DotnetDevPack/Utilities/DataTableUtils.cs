namespace DotnetDevPack.Utilities
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Linq;
  using System.Reflection;

  /// <summary>
  /// DataTable工具类
  /// </summary>
  public static class DataTableUtils
  {
    /// <summary>
    /// 转换List列表
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="dataTable">DataTable</param>
    /// <returns>返回List列表</returns>
    public static List<T> ToList<T>(this DataTable dataTable)
      where T : class, new()
    {
      Type entityType = typeof(T);

      PropertyInfo[] propertyInfo = entityType.GetProperties();
      List<T> list = new List<T>();

      foreach (DataRow item in dataTable.Rows)
      {
        object obj = null;
        foreach (PropertyInfo s in propertyInfo)
        {
          string typeName = s.Name;
          if (dataTable.Columns.Contains(typeName))
          {
            if (!s.CanWrite)
            {
              continue;
            }

            object value = item[typeName];
            if (value == DBNull.Value)
            {
              continue;
            }

            if (s.PropertyType == typeof(string))
            {
              s.SetValue(obj, value.ToString(), null);
            }
            else
            {
              s.SetValue(obj, value, null);
            }
          }
        }

        list.Add((T)obj);
      }

      return list;
    }

    /// <summary>
    /// 转换List
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="type">类型</param>
    /// <param name="dataReader">数据Reader</param>
    /// <returns></returns>
    public static List<T> DataReaderToListNoUsing<T>(Type type, IDataReader dataReader)
    {
      if (type.Name.StartsWith("KeyValuePair"))
      {
        return GetKeyValueList<T>(type, dataReader);
      }
      else if (type.GetTypeInfo().IsValueType || type == typeof(string) || type == typeof(byte[]))
      {
        return GetValueTypeList<T>(type, dataReader);
      }
      else if (type.IsArray)
      {
        return GetArrayList<T>(type, dataReader);
      }

      return new List<T>();
    }

    private static List<T> GetKeyValueList<T>(Type type, IDataReader dataReader)
    {
      List<T> result = new List<T>();
      while (dataReader.Read())
      {
        GetKeyValueList(type, dataReader, result);
      }

      return result;
    }

    private static void GetKeyValueList<T>(Type type, IDataReader dataReader, List<T> result)
    {
      if (typeof(KeyValuePair<object, object>) == type)
      {
        var kv = new KeyValuePair<object, object>(dataReader.GetValue(0), dataReader.GetValue(1));
        result.Add((T)Convert.ChangeType(kv, typeof(KeyValuePair<object, object>)));
      }
      else if (typeof(KeyValuePair<int, string>) == type)
      {
        var kv = new KeyValuePair<int, string>(dataReader.GetValue(0).ToInt(), dataReader.GetValue(1).ToString());
        result.Add((T)Convert.ChangeType(kv, typeof(KeyValuePair<int, string>)));
      }
      else if (typeof(KeyValuePair<int, int>) == type)
      {
        var kv = new KeyValuePair<int, int>(dataReader.GetValue(0).ToInt(), dataReader.GetValue(1).ToInt());
        result.Add((T)Convert.ChangeType(kv, typeof(KeyValuePair<int, int>)));
      }
      else if (typeof(KeyValuePair<string, int>) == type)
      {
        var kv = new KeyValuePair<string, int>(dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToInt());
        result.Add((T)Convert.ChangeType(kv, typeof(KeyValuePair<string, int>)));
      }
      else if (typeof(KeyValuePair<string, object>) == type)
      {
        var kv = new KeyValuePair<string, object>(dataReader.GetValue(0).ToString(), dataReader.GetValue(1));
        result.Add((T)Convert.ChangeType(kv, typeof(KeyValuePair<string, object>)));
      }
      else if (typeof(KeyValuePair<string, string>) == type)
      {
        var kv = new KeyValuePair<string, string>(dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString());
        result.Add((T)Convert.ChangeType(kv, typeof(KeyValuePair<string, string>)));
      }
      else
      {
        throw new Exception("暂时不支持这种类型的字典。您可以尝试Dictionary<string, string>，或与作者联系!");
      }
    }

    private static List<T> GetValueTypeList<T>(Type type, IDataReader dataReader)
    {
      List<T> result = new List<T>();
      while (dataReader.Read())
      {
        GetValueTypeList(type, dataReader, result);
      }

      return result;
    }

    private static void GetValueTypeList<T>(Type type, IDataReader dataReader, List<T> result)
    {
      var value = dataReader.GetValue(0);
      if (type == typeof(Guid))
      {
        value = Guid.Parse(value.ToString());
      }

      if (value == DBNull.Value)
      {
        result.Add(default(T));
      }
      else if (type.IsEnum)
      {
        result.Add((T)Enum.Parse(type, value.ToString()));
      }
      else
      {
        Type newType = Nullable.GetUnderlyingType(type);
        result.Add((T)Convert.ChangeType(value, type == null ? type : newType));
      }
    }

    private static List<T> GetArrayList<T>(Type type, IDataReader dataReader)
    {
      List<T> result = new List<T>();
      int count = dataReader.FieldCount;
      var childType = type.GetElementType();
      while (dataReader.Read())
      {
        GetArrayList(type, dataReader, result, count, childType);
      }

      return result;
    }

    private static void GetArrayList<T>(Type type, IDataReader dataReader, List<T> result, int count, Type childType)
    {
      object[] array = new object[count];
      for (int i = 0; i < count; i++)
      {
        array[i] = Convert.ChangeType(dataReader.GetValue(i), childType);
      }

      if (childType == typeof(string))
      {
        result.Add((T)Convert.ChangeType(array.Select(it => it.ToString()).ToArray(), type));
      }
      else if (childType == typeof(object))
      {
        result.Add((T)Convert.ChangeType(array.Select(it => it == DBNull.Value ? null : (object)it).ToArray(), type));
      }
      else if (childType == typeof(bool))
      {
        result.Add((T)Convert.ChangeType(array.Select(it => it.ToBool()).ToArray(), type));
      }
      else if (childType == typeof(byte))
      {
        result.Add((T)Convert.ChangeType(array.Select(it => it == DBNull.Value ? 0 : (byte)it).ToArray(), type));
      }
      else if (childType == typeof(decimal))
      {
        result.Add((T)Convert.ChangeType(array.Select(it => it.ToDecimal()).ToArray(), type));
      }
      else if (childType == typeof(Guid))
      {
        result.Add((T)Convert.ChangeType(array.Select(it => it == DBNull.Value ? Guid.Empty : (Guid)it).ToArray(), type));
      }
      else if (childType == typeof(DateTime))
      {
        result.Add((T)Convert.ChangeType(array.Select(it => it == DBNull.Value ? DateTime.MinValue : (DateTime)it).ToArray(), type));
      }
      else if (childType == typeof(int))
      {
        result.Add((T)Convert.ChangeType(array.Select(it => it.ToInt()).ToArray(), type));
      }
      else
      {
        throw new Exception("This type of Array is not supported for the time being. You can try object[] or contact the author!!");
      }
    }
  }
}
