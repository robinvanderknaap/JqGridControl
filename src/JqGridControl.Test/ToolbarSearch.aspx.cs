﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using JqGridControl.Test.TestData;
using System.Linq.Expressions;

namespace JqGridControl.Test
{
    public partial class ToolbarSearch : System.Web.UI.Page
    {   
        protected void Page_Load(object Sender, EventArgs e)
        {
            foreach (var city in new CustomerRepository().GetAllCities())
            {
                JqGridControl1.Columns[4].SearchOptions.Add(new SearchOption { Text=city, Value=city });
            }
        }
    }
}