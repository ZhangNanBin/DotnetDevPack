namespace DotnetDevPack.PagingUtility
{
  using System.Collections.Generic;

  /// <summary>
  /// 分页后返回的结果
  /// </summary>
  /// <typeparam name="TOfResult">返回结果的类型</typeparam>
  public class PagedResult<TOfResult>
  {
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="pagingInfo">分页信息</param>
    /// <param name="result">结果</param>
    public PagedResult(PagingInfo pagingInfo, List<TOfResult> result)
    {
      PagingInfo = pagingInfo;
      Result = result;
    }

    /// <summary>
    /// 分页信息
    /// </summary>
    public PagingInfo PagingInfo { get; set; }

    /// <summary>
    /// 结果
    /// </summary>
    /// <value>设置或获取结果</value>
    public List<TOfResult> Result { get; set; }
  }
}
