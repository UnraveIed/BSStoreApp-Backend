using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Kontroller bilgisi alinir (BooksController)
            var controller = context.RouteData.Values["controller"];
            // Action metot bilgisi alinir (CreateBook)
            var action = context.RouteData.Values["action"];

            // Parametrelerinin icinde "Dto" ifadesi gecen parametre alinir (BookDtoForInsertion)
            var param = context.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

            if (param == null)
            {
                context.Result = new BadRequestObjectResult($"Object is null. "+ $"Controller: {controller, -10} " + $"Action: {action,-10}"); // 400 firlatildi
                return;
            }

            if(!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState); // 422 firlatildi
                return;
            }
        }
    }
}
