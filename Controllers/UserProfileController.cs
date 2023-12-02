using APISeguraCRUD.Models;
using APISeguraCRUD.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APISeguraCRUD.Controllers
{
    public class UserProfileController : Controller
    {
        private APISeguraDBEntities dbEntities = new APISeguraDBEntities();
        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read()
        {
            List<UserProfileModel> lista = new List<UserProfileModel>();
            lista = (from up in dbEntities.UserProfile
                     orderby up.UserProfileID descending
                     select new UserProfileModel
                     {
                         UserProfileId = up.UserProfileID,
                         FirstName = up.FirstName,
                         LastName = up.LastName,
                         Email = up.Email
                     }).ToList();
            return View(lista);
        }

        //API REST
        [HttpGet]
        [Route("api/userprofile")]
        public JsonResult GetUserProfilesApi()
        {
            try
            {
                var userProfiles = dbEntities.UserProfile.Select(up => new UserProfileModel
                {
                    UserProfileId = up.UserProfileID,
                    FirstName = up.FirstName,
                    LastName = up.LastName,
                    Email = up.Email
                }).ToList();

                return Json(userProfiles, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }


    }
}