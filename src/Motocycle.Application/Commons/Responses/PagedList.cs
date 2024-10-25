using System;
using System.Collections.Generic;
using System.Linq;

namespace Motocycle.Application.Commons.Responses;


public class PagedList<T> : List<T>
{
    public MetaDataResponse MetaData { get; private set; }
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalRecords { get; private set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList()
    {
    }

    public PagedList(List<T> items, int totalRecords, int pageNumber, int pageSize)
    {
        TotalRecords = totalRecords;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        MetaData = new MetaDataResponse(CurrentPage, TotalPages, PageSize, TotalRecords, HasPrevious, HasNext);
        AddRange(items);
    }

    public static PagedList<T> ToPagedList(List<T> items, int pageNumber, int pageSize, int totalRecords = 0)
        => new(items, totalRecords, pageNumber, pageSize);

    public static PagedList<T> ToPagedList(IList<T> source, int pageNumber, int totalRecords = 0)
    {
        totalRecords = totalRecords == 0 ? source.Count : totalRecords;
        var items = source.ToList();

        return new PagedList<T>(items, totalRecords, pageNumber + 1, totalRecords);
    }
}