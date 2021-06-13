using Microsoft.AspNetCore.Mvc;
using ESE.Store.MVC.Models;

namespace ESE.Store.MVC.Controllers
{
    public class HomeController : BaseController
    {
        [Route("sistema-indisponivel")]
        public IActionResult SistemaIndisponivel()
        {
            var modelErro = new ErrorViewModel
            {
                Message = "O sistema está temporariamente indisponível, isto pode ocorrer em momentos de sobrecarga de usuários.",
                Title = "Sistema indisponível.",
                ErroCode = 500
            };

            return View("Error", modelErro);
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var vmErro = new ErrorViewModel();

            if (id == 500)
            {
                vmErro.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                vmErro.Title = "Ocorreu um erro!";
                vmErro.ErroCode = id;
            }
            else if (id == 404)
            {
                vmErro.Message =
                    "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                vmErro.Title = "Ops! Página não encontrada.";
                vmErro.ErroCode = id;
            }
            else if (id == 403)
            {
                vmErro.Message = "Você não tem permissão para fazer isto.";
                vmErro.Title = "Acesso Negado";
                vmErro.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", vmErro);
        }
    }
}
