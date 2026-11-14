using System;
using JQuants.Net.Abstractions.Models;
using JQuants.Net.Equities.Dtos;

namespace JQuants.Net.Equities.Mappers
{
    internal static class ListedIssueMapper
    {
        public static ListedIssue Map(
            ListedIssueResponse source)
        {
            return new ListedIssue
            {
                Date = DateTime.Parse(source.Date),
                Code = source.Code,
                CoName = source.CoName,
                CoNameEn = source.CoNameEn,
                S17 = source.S17,
                S17Nm = source.S17Nm,
                S33 = source.S33,
                S33Nm = source.S33Nm,
                ScaleCat = source.ScaleCat,
                Mkt = source.Mkt,
                MktNm = source.MktNm,
                Mrgn = source.Mrgn,
                MrgnNm = source.MrgnNm,
                ProdCat = source.ProdCat
            };
        }
    }
}