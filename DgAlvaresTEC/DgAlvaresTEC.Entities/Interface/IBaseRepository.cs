using DgAlvaresTEC.Entities.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DgAlvaresTEC.Entities.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Retorna a lista de entidades do contexto.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> ListarTodos();
        /// <summary>
        /// Retorna o contexto da entidade. Utilizado em consultas complexas a partir da camada de serviço.
        /// </summary>
        /// <returns></returns>
        DbSet<T> ObterContexto();

        /// <summary>
        /// Busca a entidade através da sua chave primária. Somente este método notifica o contexto de possíveis mudanças na entidade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> BuscarPorId(object id);

        /// <summary>
        /// Sinaliza o contexto de que uma entidade foi alterada
        /// </summary>
        /// <param name="t"></param>
        void Atualizar(T t);

        /// <summary>
        /// Insere uma entidade
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<T> Adicionar(T t);

        /// <summary>
        /// Exclui uma entidade
        /// </summary>
        /// <param name="t"></param>
        void Excluir(T t);

        /// <summary>
        /// Lista todos os registros explicitando quais as entidades-filho irão compor o retorno. Visto em
        /// https://www.c-sharpcorner.com/blogs/eager-loading-in-repository-pattern-entity-framework-core
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="ordernarPor"></param>
        /// <param name="direcao"></param>
        /// <param name="associacoes"></param>
        /// <returns></returns>
        Task<PagedCollection<T>> ListarAsync(PageQueryString pagina, Expression<Func<T, bool>> filtro, Expression<Func<T, object>> ordernarPor, Expression<Func<T, object>> ordernarSecPor = null, string direcao = "ASC", params string[] associacoes);


        ///// <summary>
        ///// Metodo de Executar Procedures, passando os parametros de entrada e saída
        ///// </summary>
        ///// <param name="query"></param>
        ///// <param name="parameters"></param>
        ///// <returns></returns>
        //Task<List<OracleParameter>> ExecWithStoreProcedure(string query, List<OracleParameter> parameters);
    }

}
