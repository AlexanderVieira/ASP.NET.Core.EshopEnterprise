using ESE.Store.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ESE.Store.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        protected bool HasResponseErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
            {
                foreach (var message in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

                return true;
            }

            return false;
        }

        protected void AddValidationError(string mensagem)
        {
            ModelState.AddModelError(string.Empty, mensagem);
        }

        protected bool ValidOperation()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}
