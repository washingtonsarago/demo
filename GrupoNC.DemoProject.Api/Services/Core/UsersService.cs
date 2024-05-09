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

    public partial class UsersService : IUsersService
    {
        #region Injected Attributes

        private readonly IUsersRepository _usersRepository;

        #endregion Injected Attributes

        #region Constructor(s)

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        #endregion Constructor(s)

        #region Public Methods

        public async Task<bool> Delete(Users entity)
        {
            if(!entity.ID.HasValue)
                throw new ArgumentException();

            return await _usersRepository.Delete(entity);
        }

        public async Task<Guid> Insert(Users entity) =>
            await _usersRepository.Insert(entity);

        public async Task<int> BulkInsert(IEnumerable<Users> entityList) =>
            await _usersRepository.BulkInsert(entityList);

        public async Task<IEnumerable<Users>> List() =>
            await _usersRepository.List();

        public async Task<IEnumerable<Users>> Select(Users entity) =>
            await _usersRepository.Select(entity);

        public async Task<IEnumerable<Users>> Query(Expression<Func<Users, bool>> filter) =>
            await _usersRepository.Query(filter);

        public async Task<IEnumerable<Users>> PagedList(int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            await _usersRepository.PagedList(
                currentPageNumber > 0 ? currentPageNumber : 1,
                itemsPerPage > 0 ? itemsPerPage : 10,
                GetFieldOf(orderByFields),
                GetQueryDirection(direction)
            );

        public async Task<IEnumerable<Users>> PagedSelect(Users entity, int currentPageNumber, int itemsPerPage, string orderByFields, string direction) =>
            await _usersRepository.PagedSelect(
                entity,
                currentPageNumber > 0 ? currentPageNumber : 1,
                itemsPerPage > 0 ? itemsPerPage : 10,
                GetFieldOf(orderByFields),
                GetQueryDirection(direction)
            );

        public async Task<bool> Update(Users entity) =>
            await _usersRepository.Update(entity);

        public async Task<bool> BulkUpdate(IEnumerable<Users> entityList) =>
            await _usersRepository.BulkUpdate(entityList);

        public async Task<bool> Merge(IEnumerable<Users> entityList) =>
            await _usersRepository.Merge(entityList);



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
                   case "Name": return "Name";
                   case "Login": return "Login";
                   case "Password": return "Password";
                   case "Email": return "Email";
                   case "BirthDate": return "BirthDate";
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