namespace GrupoNC.DemoProject.Api.Repository.Interfaces
{
    using GrupoNC.DemoProject.Api.Domains;
    using GrupoNC.DemoProject.Api.Repository.Abstractions.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public partial interface IAddressesRepository : IBaseRepository<Addresses>
    {
        Task<bool> ReplaceByID_User(Guid? ID_User, IEnumerable<Addresses> objList);

        Task<IEnumerable<Addresses>> PagedList(int currentPageNumber, int itemsPerPage, string orderByFields, string direction);

        Task<IEnumerable<Addresses>> PagedSelect(Addresses obj, int currentPageNumber, int itemsPerPage, string orderByFields, string direction);
    }
}