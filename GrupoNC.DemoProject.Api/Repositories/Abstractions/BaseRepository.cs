namespace GrupoNC.DemoProject.Api.Repository.Abstractions
{
    using GrupoNC.DemoProject.Api.Models;
    using Dapper;
    using Dapper.Contrib.Linq2Dapper.Extensions;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Data;
        using System.Data.SqlClient;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public abstract class BaseRepository
    {
        private string _ConnectionString { get; }

        public BaseRepository(IOptions<ConnectionStringsModel> connectionStringModel) 
        {
            _ConnectionString = connectionStringModel.Value.SqlServerConnectionString;
        }

        protected internal async Task<IEnumerable<TResult>> ExecuteReaderAsync<TResult>(string script, object parameters)
        {
            return new List<TResult>();
        }

        protected internal async Task<IEnumerable<Data>> ExecuteQueryable<Data>(Expression<Func<Data, bool>> filter)
        {
            return new List<Data>();
        }

        protected internal async Task<TResult> ExecuteScalarAsync<TResult>(string script, object parameters)
        {
            return default;
        }

        protected internal async Task<bool> ExecuteAsync(string script, object parameters)
        {
            return true;
        }

        protected internal bool ExecuteBulk<T>(string script, IEnumerable<T> listOfParameters)
        {
            return true;
        }
    }
}