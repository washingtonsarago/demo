namespace GrupoNC.DemoProject.Api.Utilities.CustomActionResults
{
    using Microsoft.AspNetCore.Mvc;

    public class PaymentRerquiredObjectActionResult : IActionResult
    {
        private readonly object _result;

        public PaymentRerquiredObjectActionResult(object result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result)
            {
                StatusCode = StatusCodes.Status402PaymentRequired
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}