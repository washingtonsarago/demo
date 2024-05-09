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

    public partial class AddressesRepository : BaseRepository, IAddressesRepository
    {
        #region Constructor

        public AddressesRepository(IOptions<ConnectionStringsModel> connectionStringModel) : base(connectionStringModel)
        {
        }

        #endregion Constructor

        #region Public Methods

        public async Task<bool> Delete(Addresses obj) =>
            await base.ExecuteAsync(DeleteScript(obj), obj);

        public async Task<Guid> Insert(Addresses obj) =>
            await base.ExecuteScalarAsync<Guid>(InsertScript(obj), obj);

        public async Task<int> BulkInsert(IEnumerable<Addresses> objList) =>
            await base.ExecuteScalarAsync<int>(BulkInsertScript(), new { Json = Newtonsoft.Json.JsonConvert.SerializeObject(objList) });

        public async Task<IEnumerable<Addresses>> List() =>
            await base.ExecuteReaderAsync<Addresses>(ListScript(), null);

        public async Task<IEnumerable<Addresses>> PagedList(int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            await base.ExecuteReaderAsync<Addresses>(ListPagedScript(currentPageNumber, itemsPerPage, orderByFields, direction), null);
        
        public async Task<IEnumerable<Addresses>> Select(Addresses obj) =>
            await base.ExecuteReaderAsync<Addresses>(SelectScript(obj), obj);

        public async Task<IEnumerable<Addresses>> Query(Expression<Func<Addresses, bool>> filter) =>
            await base.ExecuteQueryable<Addresses>(filter);

        public async Task<IEnumerable<Addresses>> PagedSelect(Addresses obj, int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            await base.ExecuteReaderAsync<Addresses>(SelectPagedScript(obj, currentPageNumber, itemsPerPage, orderByFields, direction), obj);

        public async Task<bool> Update(Addresses obj) =>
            await base.ExecuteAsync(UpdateScript(obj), obj);

        public async Task<bool> BulkUpdate(IEnumerable<Addresses> objList) =>
            await base.ExecuteAsync(BulkUpdateScript(), new { Json = Newtonsoft.Json.JsonConvert.SerializeObject(objList) });

        public async Task<bool> Merge(IEnumerable<Addresses> objList) =>
            await base.ExecuteAsync(MergeScript(), new { Json = Newtonsoft.Json.JsonConvert.SerializeObject(objList) });

 
        public async Task<bool> ReplaceByID_User(Guid? ID_User, IEnumerable<Addresses> objList) => 
            await base.ExecuteAsync(ReplaceByID_UserScript(), new { ID_User = ID_User, Json = Newtonsoft.Json.JsonConvert.SerializeObject(objList) });

        #endregion Public Methods

        #region Private Methods

        private string ListScript() =>
            string.Empty;

        private string ListPagedScript(int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            string.Empty;

        private string SelectScript(Addresses obj) =>
            string.Empty;

        private string SelectPagedScript(Addresses obj, int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            string.Empty;

        private string InsertScript(Addresses obj) =>
            string.Empty;

        private string UpdateScript(Addresses obj) =>
            string.Empty;

        private string BulkInsertScript() =>
            string.Empty;

        private string BulkUpdateScript() =>
            string.Empty;

        private string MergeScript() =>
            string.Empty;

        private string DeleteScript(Addresses obj) =>
            string.Empty;

        private string ReplaceByID_UserScript() => 
            string.Empty;

        #endregion Private Methods
    }
}