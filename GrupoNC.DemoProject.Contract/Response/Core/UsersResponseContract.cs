namespace GrupoNC.DemoProject.Contract.Response
{
    using GrupoNC.DemoProject.Abstractions;
    using System;

    public partial class UsersResponseContract : BaseResponseContract<UsersResponseContract>
    {
        #region Properties

        public Guid? ID { get; set; }

        public string? Name { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public DateTimeOffset? BirthDate { get; set; }

        public bool? IsActive { get; set; }

        #endregion Properties
    }
}