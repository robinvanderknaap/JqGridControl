using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using JqGridControl.Test.TestData;

namespace JqGridControl.Test
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [WebMethod]
        public static object GetGridData(bool _search, int rows, int page, string sidx, string sord)
        {
            var repo = new Repository();
            
            IList<Customer> customers;

            switch (sidx)
            {   
                case "Id":
                    customers = sord == "asc" ? 
                        repo.Customers.OrderBy(x=>x.Id).Skip((page - 1) * rows).Take(rows).ToList() :
                        repo.Customers.OrderByDescending(x => x.Id).Skip((page - 1) * rows).Take(rows).ToList();
                    break;
                case "Firstname":
                    customers = sord == "asc" ?
                        repo.Customers.OrderBy(x => x.Firstname).Skip((page - 1) * rows).Take(rows).ToList() :
                        repo.Customers.OrderByDescending(x => x.Firstname).Skip((page - 1) * rows).Take(rows).ToList();
                    break;
                case "Lastname":
                    customers = sord == "asc" ?
                        repo.Customers.OrderBy(x => x.Lastname).Skip((page - 1) * rows).Take(rows).ToList() :
                        repo.Customers.OrderByDescending(x => x.Lastname).Skip((page - 1) * rows).Take(rows).ToList();
                    break;
                case "Email":
                    customers = sord == "asc" ?
                        repo.Customers.OrderBy(x => x.Email).Skip((page - 1) * rows).Take(rows).ToList() :
                        repo.Customers.OrderByDescending(x => x.Email).Skip((page - 1) * rows).Take(rows).ToList();
                    break;
                case "DateOfBirth":
                    customers = sord == "asc" ?
                        repo.Customers.OrderBy(x => x.DateOfBirth).Skip((page - 1) * rows).Take(rows).ToList() :
                        repo.Customers.OrderByDescending(x => x.DateOfBirth).Skip((page - 1) * rows).Take(rows).ToList();
                    break;
                case "City":
                    customers = sord == "asc" ?
                        repo.Customers.OrderBy(x => x.City).Skip((page - 1) * rows).Take(rows).ToList() :
                        repo.Customers.OrderByDescending(x => x.City).Skip((page - 1) * rows).Take(rows).ToList();
                    break;
                default:
                    customers = repo.Customers.OrderBy(x => x.Id).Skip((page - 1) * rows).Take(rows).ToList();
                    break;
            }

            var totalCustomers = repo.Customers.Count;
                        
            return new
            {
                total = (totalCustomers / rows) + ((totalCustomers % rows > 0) ? 1 : 0),
                page = page,
                records = totalCustomers,
                rows = (
                    from c in customers
                    select new Dictionary<string, string>
                    {
                        {"Id", c.Id.ToString() }, // Id must always be specified!!
                        {"Firstname", c.Firstname},
                        {"Lastname", c.Lastname},
                        {"Email", c.Email},
                        {"DateOfBirth", c.DateOfBirth.ToShortDateString()},
                        {"City", c.City}
                        
                    }).ToArray()
            };
        }
    }
}
