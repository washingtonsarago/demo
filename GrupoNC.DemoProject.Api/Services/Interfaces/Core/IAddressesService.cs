namespace GrupoNC.DemoProject.Api.Services.Interfaces
{
    using GrupoNC.DemoProject.Api.Domains;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public partial interface IAddressesService
    {
        Task<bool> Delete(Addresses entity);

        Task<Guid> Insert(Addresses entity);

        Task<int> BulkInsert(IEnumerable<Addresses> entityList);

        Task<IEnumerable<Addresses>> List();

        Task<IEnumerable<Addresses>> Select(Addresses entity);

        Task<IEnumerable<Addresses>> Query(Expression<Func<Addresses, bool>> filter);

        Task<IEnumerable<Addresses>> PagedList(int currentPageNumber, int itemsPerPage, string orderByFields, string direction);

        Task<IEnumerable<Addresses>> PagedSelect(Addresses entity, int currentPageNumber, int itemsPerPage, string orderByFields, string direction);

        Task<bool> Update(Addresses entity);

        Task<bool> BulkUpdate(IEnumerable<Addresses> entityList);

        Task<bool> Merge(IEnumerable<Addresses> entityList);


        Task<bool> ReplaceByID_User(Guid? ID_User, IEnumerable<Addresses> entityList);
    }
}