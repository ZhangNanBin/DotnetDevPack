namespace DotnetDevPack.Utilities
{
  using System;
  using System.ComponentModel;
  using System.Reflection;

  /// <summary>
  /// Enum工具类
  /// </summary>
  public static class EnumUtils
  {
    /// <summary>
    /// 获取枚举值的Description特性描述信息
    /// </summary>
    /// <param name="enumValue">枚举值</param>
    /// <returns>返回Description特性描述信息，如不存在则返回类型的全名</returns>
    public static string GetDescription(this Enum enumValue)
    {
      if (enumValue is null)
      {
        throw new ArgumentNullException(nameof(enumValue));
      }

      string value = enumValue.ToString();
      FieldInfo field = enumValue.GetType().GetField(value);
      DescriptionAttribute desc = field.GetCustomAttribute<DescriptionAttribute>(false); // 获取描述属性

      return desc == null ? $"{enumValue.GetType().FullName}.{value}" : desc.Description;
    }
  }
}
