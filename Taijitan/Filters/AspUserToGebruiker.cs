﻿using Microsoft.AspNetCore.Mvc.Filters;
using Taijitan.Models.Domain;

namespace Taijitan.Filters {
    public class AspUserToGebruiker : ActionFilterAttribute {
        private readonly IGebruikerRepository _gebruikerRepository;

        public AspUserToGebruiker(IGebruikerRepository gebruikerRepository) {
            _gebruikerRepository = gebruikerRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            context.ActionArguments["gebruiker"] = context.HttpContext.User.Identity.IsAuthenticated ? _gebruikerRepository.GetBy(context.HttpContext.User.Identity.Name) : null;
            base.OnActionExecuting(context);
        }
    }
}
