
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MVCDemo.Controllers
{
    //[Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        public List<Member> lstMember
        {
            get
            {
                return (Session["ListMember"] != null) ? (List<Member>)Session["ListMember"] : new List<Member>();
            }
            set
            {
                Session["ListMember"] = value;
            }
        }

        public ActionResult Index()
        {
            return View(lstMember);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Member member)
        {
            List<Member> tempMember = lstMember;

            tempMember.Add(new Member()
            {
                Id = member.Id,
                Name = member.Name,
                Address = member.Address,
                Age = member.Age
            });

            lstMember = tempMember;

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var member = lstMember.Where(x => x.Id == id).First();

            return View(member);
        }

        [HttpPost]
        public ActionResult Edit(Member member)
        {
            List<Member> EditMember = lstMember;

            for (int i = 0; i < EditMember.Count; i++)
            {
                if (EditMember[i].Id == member.Id)
                {
                    EditMember[i].Name = member.Name;
                    EditMember[i].Address = member.Address;
                    EditMember[i].Age = member.Age;
                    break;
                }
            }

            lstMember = EditMember;

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    var DeleteMember = lstMember.Where(x => x.Id == id).First();
        //    return View(DeleteMember);
        //}

        //[HttpPost]
        public ActionResult Delete(int id)
        {
            lstMember = lstMember.Where(y => y.Id != id).ToList();

            return RedirectToAction("Index");
        }
        
        public ActionResult Logout()
        {
            WebSecurity.Logout();

            return RedirectToAction("Login","Account");
        }
    }





}