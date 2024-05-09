namespace GrupoNC.DemoProject.Contract.Response
{
    using GrupoNC.DemoProject.Abstractions;
    using System;

    public partial class AddressesResponseContract : BaseResponseContract<AddressesResponseContract>
    {
        #region Properties

        public Guid? ID { get; set; }

        public Guid? ID_User { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? State { get; set; }

        public bool? IsActive { get; set; }

        #endregion Properties
    }
}