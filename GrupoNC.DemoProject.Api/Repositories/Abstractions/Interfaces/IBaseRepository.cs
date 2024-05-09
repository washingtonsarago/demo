namespace GrupoNC.DemoProject.Api.Repository.Abstractions.Interfaces
{
    using GrupoNC.DemoProject.Api.Domains;
    using GrupoNC.DemoProject.Api.Domains.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IBaseRepository<Domain> where Domain : BaseDomain<Domain>
    {
        Task<bool> Delete(Domain item);

        Task<Guid> Insert(Domain item);

        Task<int> BulkInsert(IEnumerable<Domain> itemList);

        Task<IEnumerable<Domain>> List();

        Task<IEnumerable<Domain>> Select(Domain item);

        Task<IEnumerable<Domain>> Query(Expression<Func<Domain, bool>> filter);

        Task<bool> Update(Domain item);

        Task<bool> BulkUpdate(IEnumerable<Domain> itemoList);

        Task<bool> Merge(IEnumerable<Domain> itemList);
    }
}