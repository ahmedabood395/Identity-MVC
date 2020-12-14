using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabIdntity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LabIdntity.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Role
        public ActionResult Index()
        {
            var m = db.Roles.ToList();
            return View(m);
        }
        //Insert Roles
        //GET
        [AllowAnonymous]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(string Name)
        {

            var rolemanger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if (!rolemanger.RoleExists(Name))
            {
                role = new IdentityRole(Name);
                rolemanger.Create(role);
                return RedirectToAction("Index", "Role");
            }
            ViewBag.mess = "The Role is found";
            return View();
        }
        public ActionResult Edit(string id)
        {

            IdentityRole d = db.Roles.Where(n => n.Id == id).FirstOrDefault();
            return View(d);
        }
        [HttpPost]
        public ActionResult Edit(IdentityRole N)
        {
            var rolemanger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if (!rolemanger.RoleExists(N.Name))
            {
                role = db.Roles.Where(n => n.Id == N.Id).FirstOrDefault();
                if (role != null)
                {
                    role.Name = N.Name;
                    db.SaveChanges();
                }

                //rolemanger.Update(role);
                return RedirectToAction("Index", "Role");

            }
            ViewBag.mess = "The Role is found";
            return View();
        }
        public ActionResult Delete(string id)
        {
               IdentityRole role = db.Roles.Where(n => n.Id == id).FirstOrDefault();
            if (role != null)
            {
                db.Roles.Remove(role);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Role");
        }

    }
}