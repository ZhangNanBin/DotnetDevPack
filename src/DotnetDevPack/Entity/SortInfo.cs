namespace DotnetDevPack.Entity
{
  /// <summary>
  /// 排序信息
  /// </summary>
  public class SortInfo
  {
    /// <summary>
    /// 构造器
    /// </summary>
    public SortInfo()
    {
    }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="field">字段名称</param>
    public SortInfo(string field)
      : this(field, SortDirection.ASC)
    {
    }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="field">字段名称</param>
    /// <param name="direction">排序顺序</param>
    public SortInfo(string field, SortDirection direction)
    {
      Field = field;
      Direction = direction;
    }

    /// <summary>
    /// 排序字段名
    /// </summary>
    public string Field { get; set; }

    /// <summary>
    /// 升降序
    /// </summary>
    public SortDirection Direction { get; set; } = SortDirection.ASC;
  }
}
