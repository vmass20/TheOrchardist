using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrchardist.Data;

namespace TheOrchardist.Pages.GlobalPlants
{
  public class PaginatedList<GlobalPlantList> : List<GlobalPlantList>
  {
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }

    public PaginatedList(List<GlobalPlantList> items, int count, int pageIndex, int pageSize)
    {
      PageIndex = pageIndex;
      TotalPages = (int)Math.Ceiling(count / (double)pageSize);

      this.AddRange(items);
    }


    public bool HasPreviousPage
    {
      get
      {
        return (PageIndex > 1);
      }
    }

    public bool HasNextPage
    {
      get
      {
        return (PageIndex < TotalPages);
      }
    }
    public static async Task<PaginatedList<GlobalPlantList>> CreateAsync(
    IQueryable<GlobalPlantList> source, int pageIndex, int pageSize)
    {
      var count = await source.ToAsyncEnumerable().Count();
      var items = await source.ToAsyncEnumerable().Skip(
          (pageIndex - 1) * pageSize)
          .Take(pageSize).ToList();
      return new PaginatedList<GlobalPlantList>(items, count, pageIndex, pageSize);
    }
  }

}

