namespace GrupoNC.DemoProject.API.Controllers.Base
{
    using GrupoNC.DemoProject.Api.Domains;
    using GrupoNC.DemoProject.Api.Services.Interfaces;
    using GrupoNC.DemoProject.Api.Utilities;
    using GrupoNC.DemoProject.Api.Utilities.CustomActionResults;
    using GrupoNC.DemoProject.Contract.Request;
    using GrupoNC.DemoProject.Contract.Response;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route(HttpRoutes.CONTROLLER)]
    public partial class AddressesController : ControllerBase
    {
        private readonly IAddressesService _addressesService;

        public AddressesController(IAddressesService addressesService)
        {
            _addressesService = addressesService;
        }

        #region GET

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IList<AddressesResponseContract>), StatusCodes.Status200OK)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Get()
        {
            var list = await _addressesService.List();
            if (list?.Count() > 0)
                return new OkObjectResult(list.Select(entity => ((AddressesResponseContract)entity)));
            else
                return new NoContentResult();
        }

        [HttpGet]
        [Authorize]
        [Route(HttpRoutes.PAGING_GET)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IList<AddressesResponseContract>), StatusCodes.Status200OK)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> PagedGet
        (
            
            [FromQuery] int currentPageNumber = 1,
            [FromQuery] int itemsPerPage = 10,
            [FromQuery] string orderByFields = "ID",
            [FromQuery] string direction = "ASC"
        )
        {
            var list = await _addressesService.PagedList(
                currentPageNumber,
                itemsPerPage,
                orderByFields,
                direction                
            );

            if (list?.Count() > 0)
                return new OkObjectResult(list.Select(entity => ((AddressesResponseContract)entity)));
            else
                return new NoContentResult();
        }

        [HttpGet]
        [Authorize]
        [Route(HttpRoutes.ID_PARAMETER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IList<AddressesResponseContract>), StatusCodes.Status200OK)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> GetById(
            
            [FromRoute] Guid? iD
        )
        {
            var list = await _addressesService.Select(new Addresses { ID = iD });
            if (list?.Count() > 0)
                return new OkObjectResult((AddressesResponseContract)list.First());
            else
                return new NotFoundResult();
        }

        [HttpGet]
        [Authorize]
        [Route(HttpRoutes.SEARCH)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IList<AddressesResponseContract>), StatusCodes.Status200OK)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Search
        (
            
            [FromQuery] Guid? iD, 
            [FromQuery] Guid? iD_User, 
            [FromQuery] string? addressLine1, 
            [FromQuery] string? addressLine2, 
            [FromQuery] string? state, 
            [FromQuery] bool? isActive
        )
        {
            var list = await _addressesService.Select(
                new Addresses 
                {
                    ID = iD,
                    ID_User = iD_User,
                    AddressLine1 = addressLine1,
                    AddressLine2 = addressLine2,
                    State = state,
                    IsActive = isActive
                }
            );

            if (list?.Count() > 0)
                return new OkObjectResult(list.Select(entity => ((AddressesResponseContract)entity)));
            else
                return new NoContentResult();
        }

        [HttpGet]
        [Authorize]
        [Route(HttpRoutes.PAGING_SEARCH)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IList<AddressesResponseContract>), StatusCodes.Status200OK)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> PagedSearch
        (
            
            [FromQuery] Guid? iD,
            [FromQuery] Guid? iD_User,
            [FromQuery] string? addressLine1,
            [FromQuery] string? addressLine2,
            [FromQuery] string? state,
            [FromQuery] bool? isActive,
            [FromQuery] int currentPageNumber = 1,
            [FromQuery] int itemsPerPage = 10,
            [FromQuery] string orderByFields = "ID",
            [FromQuery] string direction = "ASC"
        )
        {
            var list = await _addressesService.PagedSelect(
                new Addresses 
                {
                    ID = iD,
                    ID_User = iD_User,
                    AddressLine1 = addressLine1,
                    AddressLine2 = addressLine2,
                    State = state,
                    IsActive = isActive
                },
                currentPageNumber,
                itemsPerPage,
                orderByFields,
                direction
            );

            if (list?.Count() > 0)
                return new OkObjectResult(list.Select(entity => ((AddressesResponseContract)entity)));
            else
                return new NoContentResult();
        }

        #endregion GET

        #region POST

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post(
            
            [FromBody] AddressesRequestContract contract
        )
        {
            try
            {
                contract.IsActive = true;

                var result = await _addressesService.Insert(contract);
                if(result != Guid.Empty)
                    return new CreatedResult($"/Addresses/{result}", result);
                else
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch(ConstraintException)
            {
                return new UnprocessableEntityResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route(HttpRoutes.BULK)]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> BulkPost(
            
            [FromBody] IEnumerable<AddressesRequestContract> contractList
        )
        {
            try
            {
                //if(contractList.Any(x => x.ID.HasValue))
                //    return new StatusCodeResult(StatusCodes.Status412PreconditionFailed);

                var entityList = contractList.Select(x => { x.IsActive = true; return (Addresses)x; });

                var result = await _addressesService.BulkInsert(entityList);
                if (result > 0)
                    return new CreatedResult(string.Empty, string.Empty);
                else
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch(ConstraintException)
            {
                return new UnprocessableEntityResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion POST

        #region PUT

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Put(
            
            [FromBody] AddressesRequestContract contract
        )
        {
            try
            {
                if(!contract.ID.HasValue)
                    return new StatusCodeResult(StatusCodes.Status412PreconditionFailed);

                await _addressesService.Update(contract);
                return new OkResult();
            }
            catch(ConstraintException)
            {
                return new UnprocessableEntityResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        
        [HttpPut]
        [Route(HttpRoutes.BULK)]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> BulkPut(
            
            [FromBody] IEnumerable<AddressesRequestContract> contractList
        )
        {
            try
            {
                if(contractList.Any(x => !x.ID.HasValue))
                    return new StatusCodeResult(StatusCodes.Status412PreconditionFailed);

                var entityList = contractList.Select(x => (Addresses)x);

                var result = await _addressesService.BulkUpdate(entityList);
                if (result)
                    return new OkResult();
                else
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch(ConstraintException)
            {
                return new UnprocessableEntityResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route(HttpRoutes.MERGE)]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Merge(
            
            [FromBody] IEnumerable<AddressesRequestContract> contractList
        )
        {
            try
            {
                var entityList = contractList.Select(x => {
                    if (!x.ID.HasValue)
                        x.ID = Guid.NewGuid();

                    return (Addresses)x;
                });

                var result = await _addressesService.Merge(entityList);
                if (result)
                    return new OkResult();
                else
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch(ConstraintException)
            {
                return new UnprocessableEntityResult();
            }
            catch (Exception ex)
            {
                return new InternalServerErrorActionResult(ex.Message);
            }
        }


        [HttpPut]
        [Authorize]
        [Route("Replace/BasedOnUser/{ID_User}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status428PreconditionRequired)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> ReplaceByID_User
        (
            
            [FromRoute] Guid? ID_User,
            [FromBody] IEnumerable<AddressesRequestContract> contractList
        )
        {
            try
            {
                if (contractList == null)
                    return new StatusCodeResult(StatusCodes.Status428PreconditionRequired);

                await _addressesService.ReplaceByID_User(ID_User, contractList.Select(x =>
                {
                    if (!x.ID.HasValue)
                        x.ID = Guid.NewGuid();

                    return (Addresses)x;
                }));

                return new OkResult();
            }
            catch (ArgumentException)
            {
                return new StatusCodeResult(StatusCodes.Status428PreconditionRequired);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        #endregion PUT

        #region DELETE

        [HttpDelete]
        [Route(HttpRoutes.ID_PARAMETER)]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid? iD)
        {
            try
            {
                if(!iD.HasValue)
                    return new StatusCodeResult(StatusCodes.Status412PreconditionFailed);

                await _addressesService.Delete(new Addresses { ID = iD });
                return new OkResult();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion DELETE
    }
}