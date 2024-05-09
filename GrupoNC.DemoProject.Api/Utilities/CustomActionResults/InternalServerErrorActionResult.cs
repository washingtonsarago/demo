namespace GrupoNC.DemoProject.Api.Utilities.CustomActionResults
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class InternalServerErrorActionResult : IActionResult
    {
        private readonly object _result;

        public InternalServerErrorActionResult(object result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}