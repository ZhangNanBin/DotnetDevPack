namespace DotnetDevPack.SortUtility
{
  using System.ComponentModel;

  /// <summary>
  /// 排序顺序
  /// </summary>
  public enum SortDirection
  {
    /// <summary>
    /// 升序
    /// </summary>
    [Description("升序")]
    ASC = 1,

    /// <summary>
    /// 降序
    /// </summary>
    [Description("降序")]
    DESC = 2
  }
}
