using System;
using System.Collections.Generic;
using System.Text;

namespace DgAlvaresTEC.Entities.Utils
{
    /// <summary>
    /// Atua na comunicação serviço-repositório. Encapsula um ICollection para dar condições de paginação aos ICollections
    /// retornados pelos métodos de listagens dos repositórios.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedCollection<T> where T : class
    {
        public PagedCollection() { }

        public PagedCollection(PageQueryString queryString, ICollection<T> payload, int count)
        {
            Collection = payload;
            PageNumber = queryString.PageNumber;
            PageSize = queryString.PageSize;
            TotalPages = (int)Math.Ceiling(count / (double)queryString.PageSize);
            TotalCount = count;

        }

        public PagedCollection(PageQueryString queryString, ICollection<T> collection, int count,
            Uri nextPage, Uri previousPage)
        {
            Collection = collection;
            PageNumber = queryString.PageNumber;
            PageSize = queryString.PageSize;
            TotalPages = (int)Math.Ceiling(count / (double)queryString.PageSize);
            TotalCount = count;
        }

        public ICollection<T> Collection { get; set; }

        public int? TotalPages { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public int? TotalCount { get; set; }

        public Uri NextPage { get; set; }

        public Uri PreviousPage { get; set; }
    }
}