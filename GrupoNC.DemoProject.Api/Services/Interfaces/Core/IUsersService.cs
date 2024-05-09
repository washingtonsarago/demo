namespace GrupoNC.DemoProject.Api.Services.Interfaces
{
    using GrupoNC.DemoProject.Api.Domains;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public partial interface IUsersService
    {
        Task<bool> Delete(Users entity);

        Task<Guid> Insert(Users entity);

        Task<int> BulkInsert(IEnumerable<Users> entityList);

        Task<IEnumerable<Users>> List();

        Task<IEnumerable<Users>> Select(Users entity);

        Task<IEnumerable<Users>> Query(Expression<Func<Users, bool>> filter);

        Task<IEnumerable<Users>> PagedList(int currentPageNumber, int itemsPerPage, string orderByFields, string direction);

        Task<IEnumerable<Users>> PagedSelect(Users entity, int currentPageNumber, int itemsPerPage, string orderByFields, string direction);

        Task<bool> Update(Users entity);

        Task<bool> BulkUpdate(IEnumerable<Users> entityList);

        Task<bool> Merge(IEnumerable<Users> entityList);


    }
}