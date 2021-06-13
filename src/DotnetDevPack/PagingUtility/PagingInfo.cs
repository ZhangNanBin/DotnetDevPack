namespace DotnetDevPack.PagingUtility
{
  using System;

  /// <summary>
  /// 分页信息
  /// </summary>
  public class PagingInfo
  {
    private int totalRecordCount;
    private int pageSize;
    private int currentPage;

    /// <summary>
    /// PagingInfo构造器
    /// </summary>
    public PagingInfo()
    {
    }

    /// <summary>
    /// PagingInfo构造器
    /// </summary>
    /// <param name="totalRecordCount">数据总数</param>
    /// <param name="pageSize">每页数据量</param>
    /// <param name="currentPage">页号</param>
    public PagingInfo(int totalRecordCount, int pageSize, int currentPage)
    {
      this.totalRecordCount = totalRecordCount;
      this.pageSize = pageSize;
      this.currentPage = currentPage;
    }

    /// <summary>
    /// 数据总数
    /// </summary>
    public int TotalRecordCount
    {
      get
      {
        return totalRecordCount;
      }

      set
      {
        if (value < 0)
        {
          throw new ArgumentOutOfRangeException(nameof(value));
        }

        totalRecordCount = value;
      }
    }

    /// <summary>
    /// 每页数据数量
    /// </summary>
    public int PageSize
    {
      get
      {
        return pageSize;
      }

      set
      {
        if (value <= 0)
        {
          throw new ArgumentOutOfRangeException(nameof(value));
        }

        pageSize = value;
      }
    }

    /// <summary>
    /// 页号
    /// </summary>
    public int CurrentPage
    {
      get
      {
        return currentPage;
      }

      set
      {
        if (value <= 0)
        {
          throw new ArgumentOutOfRangeException(nameof(value));
        }

        currentPage = value;
      }
    }

    /// <summary>
    /// 总页数
    /// </summary>
    public int PageCount
    {
      get
      {
        if (pageSize == 0)
        {
          return 0;
        }

        return (int)Math.Ceiling((double)totalRecordCount / pageSize);
      }
    }

    /// <summary>
    /// 是否还有更多数据
    /// </summary>
    public bool NoMoreData
    {
      get
      {
        return currentPage >= PageCount;
      }
    }
  }
}
