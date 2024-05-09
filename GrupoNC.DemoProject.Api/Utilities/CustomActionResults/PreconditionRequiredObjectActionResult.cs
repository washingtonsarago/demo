namespace GrupoNC.DemoProject.Api.Utilities.CustomActionResults
{
    using Microsoft.AspNetCore.Mvc;

    public class PreconditionRequiredObjectActionResult : IActionResult
    {
        private readonly object _result;

        public PreconditionRequiredObjectActionResult(object result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result)
            {
                StatusCode = StatusCodes.Status412PreconditionFailed
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}