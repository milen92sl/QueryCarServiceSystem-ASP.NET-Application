using System;

namespace QueryServiceSystem2.Services.Queries.Models
{
    public interface IQueryModel
    {
        string Brand { get; }
        DateTime Date { get; }
    }
}
