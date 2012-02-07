﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using JqGridControl.Test.TestData;

namespace JqGridControl.Test
{
    public partial class ToolbarSearch : System.Web.UI.Page
    {
        [WebMethod]
        public static object GetGridData(JqGridRequest jqGridRequest)
        {
            var repo = new CustomerRepository();

            var criteria = new List<Func<Customer, bool>>();
            if (jqGridRequest.IsSearch)
            {
                foreach (var rule in jqGridRequest.Where.rules)
                {
                    if (rule.field == "Id") criteria.Add(x => x.Id.ToString() == rule.data.ToLower());
                    if (rule.field == "Firstname") criteria.Add(x => x.Firstname.ToLower().Contains(rule.data.ToLower()));
                    if (rule.field == "Lastname") criteria.Add(x => x.Lastname.ToLower().Contains(rule.data.ToLower()));
                    if (rule.field == "Email") criteria.Add(x => x.Email.ToLower().Contains(rule.data.ToLower()));
                    if (rule.field == "DateOfBirth") criteria.Add(x => x.DateOfBirth.ToString().ToLower().Contains(rule.data.ToLower()));
                    if (rule.field == "City") criteria.Add(x => x.City.ToLower().Contains(rule.data.ToLower()));
                }
            }

            IEnumerable<Customer> customers;

            switch (jqGridRequest.SortIndex)
            {
                case "Id":
                    customers = repo.GetAll(criteria, jqGridRequest.PageIndex - 1, jqGridRequest.PageSize, x => x.Id, jqGridRequest.SortOrder);
                    break;
                case "Firstname":
                    customers = repo.GetAll(criteria, jqGridRequest.PageIndex - 1, jqGridRequest.PageSize, x => x.Firstname, jqGridRequest.SortOrder);
                    break;
                case "Lastname":
                    customers = repo.GetAll(criteria, jqGridRequest.PageIndex - 1, jqGridRequest.PageSize, x => x.Lastname, jqGridRequest.SortOrder);
                    break;
                case "Email":
                    customers = repo.GetAll(criteria, jqGridRequest.PageIndex - 1, jqGridRequest.PageSize, x => x.Email, jqGridRequest.SortOrder);
                    break;
                case "DateOfBirth":
                    customers = repo.GetAll(criteria, jqGridRequest.PageIndex - 1, jqGridRequest.PageSize, x => x.DateOfBirth, jqGridRequest.SortOrder);
                    break;
                case "City":
                    customers = repo.GetAll(criteria, jqGridRequest.PageIndex - 1, jqGridRequest.PageSize, x => x.City, jqGridRequest.SortOrder);
                    break;
                default:
                    customers = repo.GetAll(criteria, jqGridRequest.PageIndex - 1, jqGridRequest.PageSize, x => x.Id, jqGridRequest.SortOrder);
                    break;
            }

            var totalCustomers = repo.GetAll(criteria).Count();

            return new
            {
                total = (totalCustomers / jqGridRequest.PageSize) + ((totalCustomers % jqGridRequest.PageSize > 0) ? 1 : 0),
                page = jqGridRequest.PageIndex,
                records = totalCustomers,
                rows = (
                    from c in customers
                    select new Dictionary<string, string>
                    {
                        {"Id", c.Id.ToString() }, // Id must always be specified!!
                        {"Firstname", c.Firstname},
                        {"Lastname", c.Lastname},
                        
                        {"DateOfBirth", c.DateOfBirth.ToShortDateString()},
                        {"City", c.City}
                        
                    }).ToArray()
            };
        } 
    }
}