using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using APISeguraCRUD.Models;
using APISeguraCRUD.Models.ViewModel;

namespace APISeguraCRUD.Controllers
{
    public class LoginController : Controller
    {
        private APISeguraDBEntities dbEntities = new APISeguraDBEntities();

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Index(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = dbEntities.Users.SingleOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password);

                if (user != null)
                {
                    // Usuario válido, redirigir a la página principal o realizar otras acciones
                    ViewBag.ErrorMessage = "Credenciales válidas";
                    return RedirectToAction("Read", "UserProfile");
                }
            }

            // Usuario no válido o datos no válidos, mostrar un mensaje de error
            ViewBag.ErrorMessage = "Credenciales no válidas";
            return View("Login");
        }

        //CRUD - READ
        [HttpGet]
        public ActionResult ReadUser() {
            List<LoginModel> lista = new List<LoginModel>();
            lista = (from up in dbEntities.Users
                     orderby up.UserID descending
                     select new LoginModel
                     {
                         UserId = up.UserID,
                         Username = up.Username,
                         Password = up.Password
                     }).ToList();
            return View(lista);
        }

        //API REST
        [HttpGet]
        [Route("api/users")]
        public JsonResult GetUserApi()
        {
            try
            {
                var users = dbEntities.Users.Select(up => new LoginModel
                {
                    UserId = up.UserID,
                    Username = up.Username,
                    Password = up.Password,
                }).ToList();

                return Json(users, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        //CRUD - CREATE
        public ActionResult New()
        {
              return View();
        }
        [HttpPost]
        public ActionResult Save(LoginModel newUser)
        {
            try
            {
                var user = new Users
                {
                    Username = newUser.Username,
                    Password = newUser.Password
                };

                dbEntities.Users.Add(user);
                dbEntities.SaveChanges();

                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            
        }

        //API post
        [HttpPost]
        [Route("api/users")]
        public JsonResult CreateUserApi(LoginModel newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new Users
                    {
                        Username = newUser.Username,
                        Password = newUser.Password
                    };

                    dbEntities.Users.Add(user);
                    dbEntities.SaveChanges();

                    // Puedes devolver el usuario recién creado si lo deseas
                    var createdUser = new LoginModel
                    {
                        UserId = user.UserID,
                        Username = user.Username,
                        Password = user.Password
                    };

                    return Json(createdUser);
                }
                else
                {
                    // Si los datos no son válidos, devuelve un mensaje de error
                    return Json(new { error = "Datos no válidos" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

    }
}
