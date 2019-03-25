using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Taijitan.Models.Domain;

namespace Taijitan.Filters {
    public class GebruikerFilter : ActionFilterAttribute {
        private readonly IGebruikerRepository _gebruikerRepository;

        public GebruikerFilter(IGebruikerRepository gebruikerRepository) {
            _gebruikerRepository = gebruikerRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            try
            {
                context.ActionArguments["gebruiker"] = context.HttpContext.User.Identity.IsAuthenticated ? _gebruikerRepository.GetBy(context.HttpContext.Session.GetString("Gebruiker")) : null;
                base.OnActionExecuting(context);
            }
            catch
            {
                context.ActionArguments["gebruiker"] = null;
                base.OnActionExecuting(context);
            }
        }
    }
}
