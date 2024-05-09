namespace GrupoNC.DemoProject.Api.Repository
{
    using GrupoNC.DemoProject.Api.Domains;
    using GrupoNC.DemoProject.Api.Models;
    using GrupoNC.DemoProject.Api.Repository.Abstractions;
    using GrupoNC.DemoProject.Api.Repository.Interfaces;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public partial class UsersRepository : BaseRepository, IUsersRepository
    {
        #region Constructor

        public UsersRepository(IOptions<ConnectionStringsModel> connectionStringModel) : base(connectionStringModel)
        {
        }

        #endregion Constructor

        #region Public Methods

        public async Task<bool> Delete(Users obj) =>
            await base.ExecuteAsync(DeleteScript(obj), obj);

        public async Task<Guid> Insert(Users obj) =>
            await base.ExecuteScalarAsync<Guid>(InsertScript(obj), obj);

        public async Task<int> BulkInsert(IEnumerable<Users> objList) =>
            await base.ExecuteScalarAsync<int>(BulkInsertScript(), new { Json = Newtonsoft.Json.JsonConvert.SerializeObject(objList) });

        public async Task<IEnumerable<Users>> List() =>
            await base.ExecuteReaderAsync<Users>(ListScript(), null);

        public async Task<IEnumerable<Users>> PagedList(int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            await base.ExecuteReaderAsync<Users>(ListPagedScript(currentPageNumber, itemsPerPage, orderByFields, direction), null);
        
        public async Task<IEnumerable<Users>> Select(Users obj) =>
            await base.ExecuteReaderAsync<Users>(SelectScript(obj), obj);

        public async Task<IEnumerable<Users>> Query(Expression<Func<Users, bool>> filter) =>
            await base.ExecuteQueryable<Users>(filter);

        public async Task<IEnumerable<Users>> PagedSelect(Users obj, int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            await base.ExecuteReaderAsync<Users>(SelectPagedScript(obj, currentPageNumber, itemsPerPage, orderByFields, direction), obj);

        public async Task<bool> Update(Users obj) =>
            await base.ExecuteAsync(UpdateScript(obj), obj);

        public async Task<bool> BulkUpdate(IEnumerable<Users> objList) =>
            await base.ExecuteAsync(BulkUpdateScript(), new { Json = Newtonsoft.Json.JsonConvert.SerializeObject(objList) });

        public async Task<bool> Merge(IEnumerable<Users> objList) =>
            await base.ExecuteAsync(MergeScript(), new { Json = Newtonsoft.Json.JsonConvert.SerializeObject(objList) });



        #endregion Public Methods

        #region Private Methods

        private string ListScript() =>
            string.Empty;

        private string ListPagedScript(int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            string.Empty;

        private string SelectScript(Users obj) =>
            string.Empty;

        private string SelectPagedScript(Users obj, int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            string.Empty;

        private string InsertScript(Users obj) =>
            string.Empty;

        private string UpdateScript(Users obj) =>
            string.Empty;

        private string BulkInsertScript() =>
            string.Empty;

        private string BulkUpdateScript() =>
            string.Empty;

        private string MergeScript() =>
            string.Empty;

        private string DeleteScript(Users obj) =>
            string.Empty;

        private string ReplaceByID_UserScript() =>
            string.Empty;

        #endregion Private Methods
    }
}