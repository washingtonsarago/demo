namespace GrupoNC.DemoProject.Api.Utilities.CustomActionResults
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class PreconditionRequiredActionResult : ActionResult
    {
        public PreconditionRequiredActionResult()
        {
        }

        public override void ExecuteResult(ActionContext context)
        {
            var objectResult = new ObjectResult(string.Empty)
            {
                StatusCode = StatusCodes.Status428PreconditionRequired
            };

            objectResult.ExecuteResult(context);
        }
    }
}