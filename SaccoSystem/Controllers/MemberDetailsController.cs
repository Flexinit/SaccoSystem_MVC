using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SaccoSystem.Models;

namespace SaccoSystem.Controllers
{
    public class MemberDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MemberDetails
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var memberDetails = InitSorting(sortOrder, currentFilter, searchString, page);

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(memberDetails.ToPagedList(pageNumber, pageSize));
        }

        // GET: MemberDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDetails memberDetails = db.MemberDetails.Find(id);
            if (memberDetails == null)
            {
                return HttpNotFound();
            }
            return View(memberDetails);
        }

        // GET: MemberDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Country,Branch,LoanRefID,ClientRefID,Surname,FirstName,IDNumber,EmployeeNo,PhoneNo,EmployerGroup,PaymentMethod,Product")] MemberDetails memberDetails)
        {
            if (ModelState.IsValid)
            {
                db.MemberDetails.Add(memberDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberDetails);
        }

        // GET: MemberDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDetails memberDetails = db.MemberDetails.Find(id);
            if (memberDetails == null)
            {
                return HttpNotFound();
            }
            return View(memberDetails);
        }

        // POST: MemberDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Country,Branch,LoanRefID,ClientRefID,Surname,FirstName,IDNumber,EmployeeNo,PhoneNo,EmployerGroup,PaymentMethod,Product")] MemberDetails memberDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberDetails);
        }

        // GET: MemberDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDetails memberDetails = db.MemberDetails.Find(id);
            if (memberDetails == null)
            {
                return HttpNotFound();
            }
            return View(memberDetails);
        }

        // POST: MemberDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberDetails memberDetails = db.MemberDetails.Find(id);
            db.MemberDetails.Remove(memberDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IQueryable<MemberDetails> InitSorting(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {

                ViewBag.CurrentSort = sortOrder;

                var memberDetails = from s in db.MemberDetails
                                     select s;

                //Implement Searching
                if (!String.IsNullOrEmpty(searchString))
                {

                    memberDetails = memberDetails.Where(s => s.Country.Contains(searchString)
                                           || s.Branch.Contains(searchString)
                                           || s.LoanRefID.Contains(searchString)
                                           || s.ClientRefID.Contains(searchString)
                                           || s.Surname.Contains(searchString)
                                           || s.FirstName.Contains(searchString)
                                           || s.IDNumber.Contains(searchString)
                                           || s.EmployeeNo.Contains(searchString)
                                           || s.PhoneNo.Contains(searchString)
                                           || s.EmployerGroup.Contains(searchString)
                                           || s.PaymentMethod.Contains(searchString)
                                           || s.Product.Contains(searchString));
                                          
                }

                ////Paging
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;


                //Implement Sorting
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                switch (sortOrder)
                {

                    case "name_desc":
                        memberDetails = memberDetails.OrderByDescending(s => s.FirstName);
                        break;                

                    default:
                        memberDetails = memberDetails.OrderBy(s => s.FirstName);
                        break;
                }


                return memberDetails;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
