using Infrastructure.CrossCutting.Helpers;
using Infrastructure.CrossCutting.Helpers.Enums;
using Infrastructure.CrossCutting.Helpers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Services.Controllers.Base
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        public static Response CreateResponse(ResponseEnum status, string alternativeDescription = null, object result = null)
        {
            var response = new Response()
            {
                Status = (int)status,
                Message = string.IsNullOrEmpty(alternativeDescription) ? Utils.GetEnumDescription(status) : alternativeDescription,
                Result = result
            };

            return response;
        }

        public static IActionResult CreateAnyResponse(object result, string failMessage)
        {
            if (result == null || Utils.ValidateResultIsBoolean(result))
                return CreateInsuccessResponse(failMessage);

            return CreateSuccessResponse(result);
        }

        public static IActionResult CreateSuccessResponse(object result)
        {
            return new OkObjectResult(CreateResponse(ResponseEnum.Sucesso, null, result));
        }

        public static IActionResult CreateInsuccessResponse(string alternativeDescription)
        {
            return new BadRequestObjectResult(CreateResponse(ResponseEnum.Insucesso, alternativeDescription));
        }

        public static IActionResult CreateErroResponse(string alternativeDescription)
        {
            var InternalObjectResult = new ObjectResult(CreateResponse(ResponseEnum.ErroDeSistema, alternativeDescription, null));
            InternalObjectResult.StatusCode = 500;

            return InternalObjectResult;
        }

        public static IActionResult CreateInsuccessModelStateResponse(ModelStateDictionary modelState)
        {
            var messages = "";
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                messages += (!string.IsNullOrEmpty(messages)) ? $";{errorMsg}" : errorMsg;
            }

            return CreateInsuccessResponse(messages);
        }
    }
}
