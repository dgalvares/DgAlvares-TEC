using System;
using System.Collections.Generic;
using System.Text;

namespace DgAlvaresTEC.Entities.Utils
{
    /// <summary>
    /// Abstrai a funcionalidade de paginação através de parâmetros passados pela QueryString.
    /// Ex.: http://www.exemplo.com.br/entidade?pageNumber=1&pageSize=50
    /// Valor padrão do tamanho de página: 50 registros
    /// Autor: Eduardo Elarrat
    /// </summary>
    public class PageQueryString
    {
        private const int MAX_PAGE_SIZE = 100;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 25;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
            }
        }
    }
}
