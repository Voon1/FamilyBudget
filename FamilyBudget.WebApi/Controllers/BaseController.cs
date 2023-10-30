//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using FamilyBudget.Api.Interface;

//namespace FamilyBudget.Api.Controllers
//{
//    public class BaseController<TController> : ControllerBase
//    {
//        protected readonly ILogger<TController> logger;

//        protected BaseController(ILogger<TController> logger)
//        {
//            this.logger = logger;
//        }

//        protected async Task<IActionResult> ProcessAsync<T>(Func<Task<T>> action)
//        {
//            try
//            {
//                var result = await action();

//                if (result is ErrorResponse)
//                {
//                    return UnprocessableEntity(result);
//                }
//                else
//                {
//                    return Ok(result);
//                }
//            }
//            catch (Exception e)
//            {
//                logger.LogError(e, $"fatal exception during processing");

//                return StatusCode((Int32)HttpStatusCode.InternalServerError, new ProblemDetails()
//                {
//                    Status = (Int32)HttpStatusCode.InternalServerError,
//                    Detail = $"Unhandled exception occured while processing request"
//                });
//            }
//        }
//    }
//}

