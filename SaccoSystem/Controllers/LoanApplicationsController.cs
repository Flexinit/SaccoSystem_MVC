using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using PagedList;
using SaccoSystem.Models;
using Vereyon.Web;

namespace SaccoSystem.Controllers
{
    public class LoanApplicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string LoanRefID = null;
        public string ProductID = "KG0001-60";
        public string Period = "60";
        public DateTime ApplicationDate = DateTime.Now;
        public bool disbursed = false;


        // GET: LoanApplications
        [AllowAnonymous]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var loanApplicationData = InitSorting(sortOrder, currentFilter, searchString, page);

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(loanApplicationData.ToPagedList(pageNumber, pageSize));
        }

        // GET: LoanApplications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }

        // GET: LoanApplications/Create
        public ActionResult Create()
        {
            LoanRefID ="LN"+ GetRandomString(4,false);
            ViewBag.LoanRefID = LoanRefID;
            ViewBag.ProductID = ProductID;
            ViewBag.Period = Period;
            ViewBag.ApplicationDate = ApplicationDate;

            return View();
        }

        // POST: LoanApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LoanRefID,EmployeeNo,EmployerName,CurrentSafPhoneNo,DeductionAccnt,DateOfBirth,LoanValue,Product,LoanPeriod,Instalment,LoanSuccess,DateOfDisbursement,PeriodPaid,TermRemContractual,InstallmntPaid,TotalArrears,Approved")] LoanApplication loanApplication)
        {
            if (ModelState.IsValid)
            {
                db.LoanApplications.Add(loanApplication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loanApplication);
        }

        // GET: LoanApplications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }

        // POST: LoanApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LoanRefID,EmployeeNo,EmployerName,CurrentSafPhoneNo,DeductionAccnt,DateOfBirth,LoanValue,Product,LoanPeriod,Instalment,LoanSuccess,DateOfDisbursement,PeriodPaid,TermRemContractual,InstallmntPaid,TotalArrears, Approved")] LoanApplication loanApplication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanApplication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loanApplication);
        }

        // GET: LoanApplications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }

        // POST: LoanApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            db.LoanApplications.Remove(loanApplication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IQueryable<LoanApplication> InitSorting(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {

                ViewBag.CurrentSort = sortOrder;

                var loanApplicationData = from s in db.LoanApplications
                                    select s;

                //Implement Searching
                if (!String.IsNullOrEmpty(searchString))
                {

                    loanApplicationData = loanApplicationData.Where(s => s.LoanRefID.Contains(searchString)
                                           || s.EmployeeNo.Contains(searchString)
                                           || s.LoanRefID.Contains(searchString)
                                           || s.EmployerName.Contains(searchString)
                                           || s.CurrentSafPhoneNo.Contains(searchString)
                                           || s.DeductionAccnt.Contains(searchString)                                        
                                           || s.EmployeeNo.Contains(searchString)
                                           || s.Product.Contains(searchString)
                                           || s.LoanPeriod.Contains(searchString)
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
                        loanApplicationData = loanApplicationData.OrderBy(s => s.LoanRefID);
                        break;

                    default:
                        loanApplicationData = loanApplicationData.OrderBy(s => s.DateOfApplication);
                        break;
                }


                return loanApplicationData;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public string GetRandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        [HttpPost]
        public ActionResult  Disburse() {

            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var approvedLoans = from s in ctx.LoanApplications
                                        where s.Approved == true && s.LoanSuccess == false
                                        select new
                                        {
                                            phoneNumber = s.CurrentSafPhoneNo,
                                            amount = s.LoanValue
                                        };

                    foreach (var obj in approvedLoans)
                    {

                        string loanValue = obj.amount.ToString("0.##");

                        Console.Write(obj.amount + " " + obj.phoneNumber);

                        MakeApiRequest(obj.phoneNumber, loanValue);
                    }
                

                if (disbursed)
                {

                        (from x in ctx.LoanApplications
                         where x.Approved == true
                         select x).ToList().ForEach(xx => xx.LoanSuccess = true);

                        ctx.SaveChanges();

                        TempData["Success"] = "Disbursement Successful";

                    FlashMessage.Confirmation("Loan Disbursement Successful");

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Success"] = "Disbursement FAILED";

                    FlashMessage.Confirmation("Loan Disbursement Successful");

                    return Redirect(Request.UrlReferrer.ToString());

                }
            }
            }
            catch (Exception ex) {

                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        public bool MakeApiRequest(string phone, string amount) {

            string baseUrl = "https://144.91.82.244:810/pay.aspx";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
          
           // request.ContentType = "application/json";
           // request.Headers.Add("cache-control", "no-cache");
            request.KeepAlive = false;
            request.Method = "POST";


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{\"amount\":" + amount + "," +
                          "\"mobile\":" + phone + "}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }


            try
            {
         ServicePointManager.ServerCertificateValidationCallback =
          delegate (
              object s,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors
          ) {
              return true;
          };

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                string resp = readStream.ReadToEnd().ToString();

                Console.WriteLine(readStream.ReadToEnd());
                response.Close();
                readStream.Close();

                string resp_token = ((JObject.Parse(resp))["status"]).ToString();

                if (resp_token == "1")
                {
                    disbursed = true;
                    FlashMessage.Confirmation("Loan Disbursement Successful");

                }
                else {

                    disbursed = false;
                }
                Logger.WriteLog(DateTime.Now + ":  Response To Main Server: " + resp);

                return disbursed;
            }


            catch (WebException ex)
            {

                disbursed = false;
                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                Console.WriteLine(resp);

                Logger.WriteLog(DateTime.Now + ":  Response To Main Server: " + resp);

                return disbursed;

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
