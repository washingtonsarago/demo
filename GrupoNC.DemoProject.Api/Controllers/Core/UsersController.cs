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
    public partial class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        #region GET

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IList<UsersResponseContract>), StatusCodes.Status200OK)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Get()
        {
            var list = await _usersService.List();
            if (list?.Count() > 0)
                return new OkObjectResult(list.Select(entity => ((UsersResponseContract)entity)));
            else
                return new NoContentResult();
        }

        [HttpGet]
        [Authorize]
        [Route(HttpRoutes.PAGING_GET)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IList<UsersResponseContract>), StatusCodes.Status200OK)]
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
            var list = await _usersService.PagedList(
                currentPageNumber,
                itemsPerPage,
                orderByFields,
                direction                
            );

            if (list?.Count() > 0)
                return new OkObjectResult(list.Select(entity => ((UsersResponseContract)entity)));
            else
                return new NoContentResult();
        }

        [HttpGet]
        [Authorize]
        [Route(HttpRoutes.ID_PARAMETER)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IList<UsersResponseContract>), StatusCodes.Status200OK)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> GetById(
            
            [FromRoute] Guid? iD
        )
        {
            var list = await _usersService.Select(new Users { ID = iD });
            if (list?.Count() > 0)
                return new OkObjectResult((UsersResponseContract)list.First());
            else
                return new NotFoundResult();
        }

        [HttpGet]
        [Authorize]
        [Route(HttpRoutes.SEARCH)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IList<UsersResponseContract>), StatusCodes.Status200OK)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Search
        (
            
            [FromQuery] Guid? iD, 
            [FromQuery] string? name, 
            [FromQuery] string? login, 
            [FromQuery] string? password, 
            [FromQuery] string? email, 
            [FromQuery] DateTimeOffset? birthDate, 
            [FromQuery] bool? isActive
        )
        {
            var list = await _usersService.Select(
                new Users 
                {
                    ID = iD,
                    Name = name,
                    Login = login,
                    Password = password,
                    Email = email,
                    BirthDate = birthDate,
                    IsActive = isActive
                }
            );

            if (list?.Count() > 0)
                return new OkObjectResult(list.Select(entity => ((UsersResponseContract)entity)));
            else
                return new NoContentResult();
        }

        [HttpGet]
        [Authorize]
        [Route(HttpRoutes.PAGING_SEARCH)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IList<UsersResponseContract>), StatusCodes.Status200OK)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> PagedSearch
        (
            
            [FromQuery] Guid? iD,
            [FromQuery] string? name,
            [FromQuery] string? login,
            [FromQuery] string? password,
            [FromQuery] string? email,
            [FromQuery] DateTimeOffset? birthDate,
            [FromQuery] bool? isActive,
            [FromQuery] int currentPageNumber = 1,
            [FromQuery] int itemsPerPage = 10,
            [FromQuery] string orderByFields = "ID",
            [FromQuery] string direction = "ASC"
        )
        {
            var list = await _usersService.PagedSelect(
                new Users 
                {
                    ID = iD,
                    Name = name,
                    Login = login,
                    Password = password,
                    Email = email,
                    BirthDate = birthDate,
                    IsActive = isActive
                },
                currentPageNumber,
                itemsPerPage,
                orderByFields,
                direction
            );

            if (list?.Count() > 0)
                return new OkObjectResult(list.Select(entity => ((UsersResponseContract)entity)));
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
            
            [FromBody] UsersRequestContract contract
        )
        {
            try
            {
                contract.IsActive = true;

                var result = await _usersService.Insert(contract);
                if(result != Guid.Empty)
                    return new CreatedResult($"/Users/{result}", result);
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
            
            [FromBody] IEnumerable<UsersRequestContract> contractList
        )
        {
            try
            {
                //if(contractList.Any(x => x.ID.HasValue))
                //    return new StatusCodeResult(StatusCodes.Status412PreconditionFailed);

                var entityList = contractList.Select(x => { x.IsActive = true; return (Users)x; });

                var result = await _usersService.BulkInsert(entityList);
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
            
            [FromBody] UsersRequestContract contract
        )
        {
            try
            {
                if(!contract.ID.HasValue)
                    return new StatusCodeResult(StatusCodes.Status412PreconditionFailed);

                await _usersService.Update(contract);
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
            
            [FromBody] IEnumerable<UsersRequestContract> contractList
        )
        {
            try
            {
                if(contractList.Any(x => !x.ID.HasValue))
                    return new StatusCodeResult(StatusCodes.Status412PreconditionFailed);

                var entityList = contractList.Select(x => (Users)x);

                var result = await _usersService.BulkUpdate(entityList);
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
            
            [FromBody] IEnumerable<UsersRequestContract> contractList
        )
        {
            try
            {
                var entityList = contractList.Select(x => {
                    if (!x.ID.HasValue)
                        x.ID = Guid.NewGuid();

                    return (Users)x;
                });

                var result = await _usersService.Merge(entityList);
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

                await _usersService.Delete(new Users { ID = iD });
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