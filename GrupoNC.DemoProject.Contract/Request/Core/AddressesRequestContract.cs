namespace GrupoNC.DemoProject.Contract.Request
{
    using GrupoNC.DemoProject.Abstractions;
    using System;

    public partial class AddressesRequestContract : BaseRequestContract<AddressesRequestContract>
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