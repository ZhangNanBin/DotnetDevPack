namespace DotnetDevPack.Utilities
{
  using System;

  /// <summary>
  /// Object工具类
  /// </summary>
  public static class ObjectUtils
  {
    /// <summary>
    /// 转换为Int
    /// </summary>
    /// <param name="thisValue">数值</param>
    /// <returns>转换后数据</returns>
    public static int ToInt(this object thisValue)
    {
      int reval = 0;
      if (thisValue == null)
      {
        return 0;
      }

      if (thisValue is Enum)
      {
        return (int)thisValue;
      }

      if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
      {
        return reval;
      }

      return reval;
    }

    /// <summary>
    /// 转换为Int
    /// </summary>
    /// <param name="thisValue">数值</param>
    /// <param name="errorValue">错误时数值</param>
    /// <returns>转换后数据</returns>
    public static int ToInt(this object thisValue, int errorValue)
    {
      int reval = 0;
      if (thisValue is Enum)
      {
        return (int)thisValue;
      }

      if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
      {
        return reval;
      }

      return errorValue;
    }

    /// <summary>
    /// 转换为double
    /// </summary>
    /// <param name="thisValue">数值</param>
    /// <returns>转换后数据</returns>
    public static double ToDouble(this object thisValue)
    {
      double reval = 0;
      if (thisValue is Enum)
      {
        return (int)thisValue;
      }

      if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
      {
        return reval;
      }

      return 0;
    }

    /// <summary>
    /// 转换为double
    /// </summary>
    /// <param name="thisValue">数值</param>
    /// <param name="errorValue">错误时数值</param>
    /// <returns>转换后数据</returns>
    public static double ToDouble(this object thisValue, double errorValue)
    {
      double reval = 0;
      if (thisValue is Enum)
      {
        return (int)thisValue;
      }

      if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
      {
        return reval;
      }

      return errorValue;
    }

    /// <summary>
    /// 转换为Decimal
    /// </summary>
    /// <param name="thisValue">数值</param>
    /// <returns>转换后数据</returns>
    public static decimal ToDecimal(this object thisValue)
    {
      decimal reval = 0;
      if (thisValue is Enum)
      {
        return (int)thisValue;
      }

      if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
      {
        return reval;
      }

      return 0;
    }

    /// <summary>
    /// 转换为Decimal
    /// </summary>
    /// <param name="thisValue">数值</param>
    /// <param name="errorValue">错误时数值</param>
    /// <returns>转换后数据</returns>
    public static decimal ToDecimal(this object thisValue, decimal errorValue)
    {
      decimal reval = 0;
      if (thisValue is Enum)
      {
        return (int)thisValue;
      }

      if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
      {
        return reval;
      }

      return errorValue;
    }

    /// <summary>
    /// 转换为时间类型
    /// </summary>
    /// <param name="thisValue">数值</param>
    /// <returns>转换后数据,错误返回最小时间</returns>
    public static DateTime ToDateTime(this object thisValue)
    {
      DateTime reval = DateTime.MinValue;
      if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
      {
        return reval;
      }

      return reval;
    }

    /// <summary>
    /// 转换为时间类型
    /// </summary>
    /// <param name="thisValue">数值</param>
    /// <param name="errorValue">错误时数值</param>
    /// <returns>转换后数据</returns>
    public static DateTime ToDateTime(this object thisValue, DateTime errorValue)
    {
      DateTime reval = DateTime.MinValue;
      if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
      {
        return reval;
      }

      return errorValue;
    }

    /// <summary>
    /// 转换为布尔型
    /// </summary>
    /// <param name="thisValue">数值</param>
    /// <returns>转换后数据，错误返回false</returns>
    public static bool ToBool(this object thisValue)
    {
      bool reval = false;
      if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
      {
        return reval;
      }

      return reval;
    }
  }
}
