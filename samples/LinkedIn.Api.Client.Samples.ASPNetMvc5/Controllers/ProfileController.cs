using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LinkedIn.Api.Client.Owin;
using LinkedIn.Api.Client.Owin.Profiles;
using System.Threading.Tasks;

namespace LinkedIn.Api.Client.Samples.ASPNetMvc5.Controllers
{
    public class ProfileController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(this.User.Identity.GetUserId());
            var claim = user.Claims.ToList().Where(m => m.ClaimType == "LinkedIn_AccessToken").SingleOrDefault();

            var client = new LinkedInApiClient(HttpContext.GetOwinContext().Request, claim.ClaimValue);
            var profileApi = new LinkedInProfileApi(client);
            var userProfile = await profileApi.GetFullProfileAsync();

            return View(userProfile);
        }
    }
}