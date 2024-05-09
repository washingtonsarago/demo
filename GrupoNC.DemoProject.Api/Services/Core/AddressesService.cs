namespace GrupoNC.DemoProject.Api.Services
{
    using GrupoNC.DemoProject.Api.Repository.Interfaces;
    using GrupoNC.DemoProject.Api.Domains;
    using GrupoNC.DemoProject.Api.Services.Interfaces;
    using GrupoNC.DemoProject.Api.Utilities.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public partial class AddressesService : IAddressesService
    {
        #region Injected Attributes

        private readonly IAddressesRepository _addressesRepository;

        #endregion Injected Attributes

        #region Constructor(s)

        public AddressesService(IAddressesRepository addressesRepository)
        {
            _addressesRepository = addressesRepository;
        }

        #endregion Constructor(s)

        #region Public Methods

        public async Task<bool> Delete(Addresses entity)
        {
            if(!entity.ID.HasValue)
                throw new ArgumentException();

            return await _addressesRepository.Delete(entity);
        }

        public async Task<Guid> Insert(Addresses entity) =>
            await _addressesRepository.Insert(entity);

        public async Task<int> BulkInsert(IEnumerable<Addresses> entityList) =>
            await _addressesRepository.BulkInsert(entityList);

        public async Task<IEnumerable<Addresses>> List() =>
            await _addressesRepository.List();

        public async Task<IEnumerable<Addresses>> Select(Addresses entity) =>
            await _addressesRepository.Select(entity);

        public async Task<IEnumerable<Addresses>> Query(Expression<Func<Addresses, bool>> filter) =>
            await _addressesRepository.Query(filter);

        public async Task<IEnumerable<Addresses>> PagedList(int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            await _addressesRepository.PagedList(
                currentPageNumber > 0 ? currentPageNumber : 1,
                itemsPerPage > 0 ? itemsPerPage : 10,
                GetFieldOf(orderByFields),
                GetQueryDirection(direction)
            );

        public async Task<IEnumerable<Addresses>> PagedSelect(Addresses entity, int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            await _addressesRepository.PagedSelect(
                entity,
                currentPageNumber > 0 ? currentPageNumber : 1,
                itemsPerPage > 0 ? itemsPerPage : 10,
                GetFieldOf(orderByFields),
                GetQueryDirection(direction)
            );

        public async Task<bool> Update(Addresses entity) =>
            await _addressesRepository.Update(entity);

        public async Task<bool> BulkUpdate(IEnumerable<Addresses> entityList) =>
            await _addressesRepository.BulkUpdate(entityList);

        public async Task<bool> Merge(IEnumerable<Addresses> entityList) =>
            await _addressesRepository.Merge(entityList);

        public async Task<bool> ReplaceByID_User(Guid? ID_User, IEnumerable<Addresses> entityList)
        {
            if (!ID_User.HasValue || entityList == null)
                throw new ArgumentException();

            return await _addressesRepository.ReplaceByID_User(ID_User, entityList);
        }


        #endregion Public Methods

        #region Private Methods

        private string GetFieldOf(string fields)
        {
            var fieldArray = fields.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return string.Join(",", fieldArray.Select(f =>
            {
                switch (f)
                {
                   case "ID": return "ID";
                   case "ID_User": return "ID_User";
                   case "AddressLine1": return "AddressLine1";
                   case "AddressLine2": return "AddressLine2";
                   case "State": return "State";
                   case "IsActive": return "IsActive";
                   default: return "id";
                }
            }).Distinct());
        }

        private string GetQueryDirection(string direction) => 
            direction.ToUpper() == "DESC" ?
            "DESC" :
            string.Empty;

        #endregion Private Methods
    }
}