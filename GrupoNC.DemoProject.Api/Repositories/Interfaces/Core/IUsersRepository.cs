namespace GrupoNC.DemoProject.Api.Repository.Interfaces
{
    using GrupoNC.DemoProject.Api.Domains;
    using GrupoNC.DemoProject.Api.Repository.Abstractions.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public partial interface IUsersRepository : IBaseRepository<Users>
    {


        Task<IEnumerable<Users>> PagedList(int currentPageNumber, int itemsPerPage, string orderByFields, string direction);

        Task<IEnumerable<Users>> PagedSelect(Users obj, int currentPageNumber, int itemsPerPage, string orderByFields, string direction);
    }
}