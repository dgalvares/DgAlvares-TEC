using DgAlvaresTEC.Entities.Interface;
using DgAlvaresTEC.Entities.Models;
using DgAlvaresTEC.Entities.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DgAlvaresTEC.Entities.Base
{
    /// <summary>
    /// Classe-base do Padrão Repositório, mantém operações CRUD básicas associadas à entidade que o herdou.
    /// Alguns métodos de listagem foram considerados especificos às entidades e por isso são implementados nos
    /// respectivos repositórios.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly HmpContext _context;

        public BaseRepository(HmpContext context)
        {

            _context = context;
            _context.ChangeTracker.LazyLoadingEnabled = false;

        }

        /// <summary>
        /// Insere uma entidade
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<T> Adicionar(T t)
        {
            await this.ObterContexto().AddAsync(t);
            return t;
        }

        /// <summary>
        /// Sinaliza o contexto de que uma entidade foi alterada
        /// </summary>
        /// <param name="t"></param>
        public void Atualizar(T t)
        {

            this.ObterContexto().Update(t);
        }

        /// <summary>
        /// Busca a entidade através da sua chave primária. Somente este método notifica o contexto de possíveis mudanças na entidade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> BuscarPorId(object id)
        {
            return await this.ObterContexto().FindAsync(id);
        }

        /// <summary>
        /// Exclui uma entidade
        /// </summary>
        /// <param name="t"></param>
        public void Excluir(T t)
        {
            this.ObterContexto().Remove(t);
        }

        /// <summary>
        /// Retorna o contexto da entidade. Utilizado em consultas complexas a partir da camada de serviço.
        /// </summary>
        /// <returns></returns>
        public DbSet<T> ObterContexto()
        {
            return _context.Set<T>();
        }

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
        /// <summary>
        /// Metodo de Executar Procedures, passando os parametros de entrada e saída
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<List<OracleParameter>> ExecWithStoreProcedure(string query, List<OracleParameter> parameters)
        {

            var sql = await _context.Database.ExecuteSqlRawAsync(query, parameters);
            return parameters.Where(x => x.Direction == ParameterDirection.Output).ToList();

        }

        public async Task<PagedCollection<T>> ListarAsync(PageQueryString pagina, Expression<Func<T, bool>> filtro, Expression<Func<T, object>> ordernarPor,
            Expression<Func<T, object>> ordernarSecPor = null, string direcao = "ASC", params string[] associacoes)
        {
            IQueryable<T> entidade = this.ObterContexto();
            if (associacoes != null)
            {
                foreach (string filho in associacoes)
                {
                    if (string.IsNullOrWhiteSpace(filho))
                        continue;

                    entidade = entidade.Include(filho);
                }
            }

            if (filtro != null)
                entidade = entidade.Where(filtro);
            if (ordernarPor != null)
            {
                if (direcao == "DESC")
                {
                    entidade = entidade.OrderByDescending(ordernarPor);
                    if (ordernarSecPor != null)
                        entidade = entidade.OrderByDescending(ordernarPor).ThenByDescending(ordernarSecPor);

                }
                else
                {
                    entidade = entidade.OrderBy(ordernarPor);
                    if (ordernarSecPor != null)
                        entidade = entidade.OrderBy(ordernarPor).ThenBy(ordernarSecPor);

                }
            }
            var count = await entidade.CountAsync();
            var skip = (pagina.PageNumber - 1) * pagina.PageSize;
            var result = await entidade.AsNoTracking().Skip(skip).Take(pagina.PageSize).ToListAsync();
            return new PagedCollection<T>(pagina, result, count);
        }

        /// <summary>
        /// Retorna a lista de entidades do contexto.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ListarTodos()
        {
            var t = await this.ObterContexto().ToListAsync();
            return t;
        }
    }
}