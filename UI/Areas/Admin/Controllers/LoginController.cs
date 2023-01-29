using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        UserBLL userBll = new UserBLL();
        // GET: Admin/Login
        public ActionResult Index()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }
        [HttpPost]
        public ActionResult Index(UserDTO model)
        {
            if(ModelState.IsValid)
            {
                UserDTO user = userBll.GetUserWithUsernameAndPassword(model);
                if(user.ID != 0)
                {
                    //before redirecting we have to fill the static classe
                    UserStatic.UserID = user.ID;
                    UserStatic.isAdmin = user.isAdmin;
                    UserStatic.Namesurname = user.Name;
                    UserStatic.Imagepath = user.Imagepath;
                    LogBLL.AddLog(General.ProcessType.Login, General.TableName.Login, 12);
                    return RedirectToAction("Index","Post");
                }
                else
                    return View(model);
            }
            else
                return View(model);
            
        }
    }
}