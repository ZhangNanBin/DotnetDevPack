namespace DotnetDevPack.Utilities
{
  using System;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Reflection;
  using DotnetDevPack.Entity;


  /// <summary>
  /// Linq排序工具
  /// </summary>
  public static class LinqSortUtils
  {
    // 类型的所有公开属性缓存，以类型的全名为Key，Value是全部的公共属性
    private static readonly ConcurrentDictionary<string, PropertyInfo[]> PropertyInfoCache = new ConcurrentDictionary<string, PropertyInfo[]>();
    private static readonly ConcurrentDictionary<string, UnaryExpression> SortLambdaCache = new ConcurrentDictionary<string, UnaryExpression>();

    /// <summary>
    /// 获取排序信息列表
    /// </summary>
    /// <param name="sorts">排序信息列表</param>
    /// <returns>返回一个SortInfo列表，包含一个元素，此元素信息与传入参数信息相同</returns>
    public static List<SortInfo> GetDefaultSortInfo(params SortInfo[] sorts)
    {
      return sorts.ToList();
    }

    /// <summary>
    /// 获取排序信息列表
    /// </summary>
    /// <param name="fieldName">排序字段名</param>
    /// <param name="direction">排序方向</param>
    /// <returns>返回一个SortInfo列表，包含一个元素，此元素信息与传入参数信息相同</returns>
    public static List<SortInfo> GetDefaultSortInfo(string fieldName, SortDirection direction)
    {
      return GetDefaultSortInfo(new SortInfo { Field = fieldName, Direction = direction });
    }

    /// <summary>
    /// 获取排序信息列表
    /// </summary>
    /// <param name="fieldName">排序字段名</param>
    /// <returns>返回一个SortInfo列表，包含一个元素，此元素信息与传入参数信息相同，默认排正序</returns>
    public static List<SortInfo> GetDefaultSortInfo(string fieldName)
        => GetDefaultSortInfo(fieldName, SortDirection.ASC);

    /// <summary>
    /// 验证排序信息是否正确
    /// </summary>
    /// <typeparam name="T">在哪个类型上验证排序信息</typeparam>
    /// <param name="sortInfo">排序信息</param>
    /// <returns>如果排序信息在给定的类型上正确，返回true，否则返回false</returns>
    public static bool ValidateSortInfo<T>(SortInfo sortInfo)
    {
      if (sortInfo == null)
      {
        throw new ArgumentNullException(nameof(sortInfo));
      }

      if (string.IsNullOrEmpty(sortInfo.Field))
      {
        return false;
      }

      if (!Enum.IsDefined(typeof(SortDirection), sortInfo.Direction))
      {
        return false;
      }

      if (!ValidateProperty<T>(sortInfo.Field))
      {
        return false;
      }

      return true;
    }

    /// <summary>
    /// 验证排序信息列表是否正确
    /// </summary>
    /// <typeparam name="T">在哪个类型上验证排序信息</typeparam>
    /// <param name="sortInfoList">排序信息列表</param>
    /// <param name="validationMessage">如果排序信息有错，则validationMessage包含错误消息，如果没有错，validationMessage为空字符串</param>
    /// <returns>如果排序信息列表在给定的类型上正确，返回true，否则返回false</returns>
    public static bool ValidateSortInfoList<T>(List<SortInfo> sortInfoList, out string validationMessage)
    {
      if (sortInfoList == null)
      {
        throw new ArgumentNullException(nameof(sortInfoList));
      }

      validationMessage = string.Empty;

      foreach (SortInfo sort in sortInfoList)
      {
        if (!ValidateSortInfo<T>(sort))
        {
          validationMessage = $"排序字段{sort.Field}不存在或排序规则不合法";
          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// 验证给出的属性名是否存在给定的类型上
    /// </summary>
    /// <typeparam name="T">任何类型</typeparam>
    /// <param name="name">属性名称</param>
    /// <returns>如果属性名存在给定的类型上，则返回true，否则返回false</returns>
    public static bool ValidateProperty<T>(string name)
    {
      return GetPropertyInfo<T>(name) != null;
    }

    /// <summary>
    /// 根据给定的SortInfo排序信息生成IQueryable
    /// </summary>
    /// <typeparam name="T">需要在哪个类型上进行排序</typeparam>
    /// <param name="source">对指定类型的IQueryable</param>
    /// <param name="sortInfo">排序信息</param>
    /// <returns>IQueryable接口</returns>
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<SortInfo> sortInfo)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (sortInfo == null)
      {
        throw new ArgumentNullException(nameof(sortInfo));
      }

      var expression = source.Expression;
      int count = 0;
      foreach (var item in sortInfo)
      {
        var property = GetPropertyInfo<T>(item.Field);

        var method = item.Direction == SortDirection.DESC ?
            (count == 0 ? "OrderByDescending" : "ThenByDescending") :
            (count == 0 ? "OrderBy" : "ThenBy");

        var lambdaExp = GetLambdaExpressionForColumnSort<T>(item.Field);

        expression = Expression.Call(typeof(Queryable), method,
            new Type[] { source.ElementType, property.PropertyType },
            expression, lambdaExp);
        count++;
      }

      return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
    }

    /// <summary>
    /// 获取属性信息
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="propertyName">属性名称</param>
    /// <returns></returns>
    private static PropertyInfo GetPropertyInfo<T>(string propertyName)
    {
      var typeFullName = typeof(T).FullName;
      var props = PropertyInfoCache.GetOrAdd(typeFullName, _ =>
      {
        return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
      });

      return props.FirstOrDefault(p => p.Name == propertyName);
    }

    /// <summary>
    /// 此函数生成并缓存用于排序的Lambda表达式
    /// 代码示例：block => block.BlockNumber
    /// </summary>
    private static UnaryExpression GetLambdaExpressionForColumnSort<T>(string propertyName)
    {
      var typeFullName = typeof(T).FullName;
      var propFullName = $"{typeFullName}.{propertyName}";

      return SortLambdaCache.GetOrAdd(propFullName, _ =>
      {
        var typeParam = Expression.Parameter(typeof(T), "o");
        var property = GetPropertyInfo<T>(propertyName);

        // 创建一个访问属性的表达式
        var selector = Expression.MakeMemberAccess(typeParam, property);
        var orderByExp = Expression.Lambda(selector, typeParam);
        return Expression.Quote(orderByExp);
      });
    }
  }
}
