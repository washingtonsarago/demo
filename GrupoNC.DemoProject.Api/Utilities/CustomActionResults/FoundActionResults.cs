namespace GrupoNC.DemoProject.Api.Utilities.CustomActionResults
{
    using Microsoft.AspNetCore.Mvc;

    public class FoundActionResult : ActionResult
    {
        private readonly object _result;

        public FoundActionResult(object result)
        {
            _result = result;
        }

        public override void ExecuteResult(ActionContext context)
        {
            var objectResult = new ObjectResult(_result)
            {
                StatusCode = StatusCodes.Status302Found
            };

            objectResult.ExecuteResult(context);
        }
    }
}