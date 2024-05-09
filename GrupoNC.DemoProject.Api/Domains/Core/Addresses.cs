namespace GrupoNC.DemoProject.Api.Domains
{
    using GrupoNC.DemoProject.Api.Domains.Abstractions;
    using GrupoNC.DemoProject.Contract.Request;
    using GrupoNC.DemoProject.Contract.Response;
    using System;

    public partial class Addresses : BaseDomain<Addresses>
    {
        #region Properties
#nullable enable

        public Guid? ID { get; set; }

        public Guid? ID_User { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? State { get; set; }

        public bool? IsActive { get; set; }

#nullable disable
        #endregion Properties

        #region Operators

        public static implicit operator AddressesResponseContract(Addresses entity) =>
            entity == null ?
            null :
            new AddressesResponseContract
            {
                ID = entity.ID,
                ID_User = entity.ID_User,
                AddressLine1 = entity.AddressLine1,
                AddressLine2 = entity.AddressLine2,
                State = entity.State,
                IsActive = entity.IsActive,

            };

        public static implicit operator Addresses(AddressesRequestContract contract) =>
            contract == null ?
            null :
            new Addresses
            {
                ID = contract.ID,
                ID_User = contract.ID_User,
                AddressLine1 = contract.AddressLine1,
                AddressLine2 = contract.AddressLine2,
                State = contract.State,
                IsActive = contract.IsActive,

            };

        #endregion Operators
    }
}