namespace GrupoNC.DemoProject.Api.Domains
{
    using GrupoNC.DemoProject.Api.Domains.Abstractions;
    using GrupoNC.DemoProject.Contract.Request;
    using GrupoNC.DemoProject.Contract.Response;
    using System;

    public partial class Users : BaseDomain<Users>
    {
        #region Properties
#nullable enable

        public Guid? ID { get; set; }

        public string? Name { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public DateTimeOffset? BirthDate { get; set; }

        public bool? IsActive { get; set; }

#nullable disable
        #endregion Properties

        #region Operators

        public static implicit operator UsersResponseContract(Users entity) =>
            entity == null ?
            null :
            new UsersResponseContract
            {
                ID = entity.ID,
                Name = entity.Name,
                Login = entity.Login,
                Password = entity.Password,
                Email = entity.Email,
                BirthDate = entity.BirthDate,
                IsActive = entity.IsActive,

            };

        public static implicit operator Users(UsersRequestContract contract) =>
            contract == null ?
            null :
            new Users
            {
                ID = contract.ID,
                Name = contract.Name,
                Login = contract.Login,
                Password = contract.Password,
                Email = contract.Email,
                BirthDate = contract.BirthDate,
                IsActive = contract.IsActive,

            };

        #endregion Operators
    }
}