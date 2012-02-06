using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JqGridControl
{
    [DefaultProperty("Url")]
    [ToolboxData("<{0}:JqGridControl runat=server></{0}:JqGridControl>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class JqGrid : Control
    {
        [Category("Settings")]
        [DefaultValue("")]
        public string Url
        {
            get
            {
                String s = (String)ViewState["Url"];
                return ((s == null) ? "" : s);
            }

            set
            {
                ViewState["Url"] = value;
            }
        }

        [Category("Appearance")]
        [DefaultValue("")]
        public int? Width
        {
            get
            {
                return ViewState["Width"] != null ? (int?)ViewState["Width"] : null;               
            }
            set
            {
                ViewState["Width"] = value;
            }
        }

        [Category("Appearance")]
        [DefaultValue("")]
        public int? Height
        {
            get
            {
                return ViewState["Height"] != null ? (int?)ViewState["Height"] : null;
            }
            set
            {
                ViewState["Height"] = value;
            }
        }

        [Category("Settings")]
        [DefaultValue(false)]
        public bool PagingEnabled
        {
            get
            {
                return ViewState["PagingEnabled"] != null ? (bool)ViewState["PagingEnabled"] : false;
            }
            set
            {
                ViewState["PagingEnabled"] = value;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<JqGridColumn> Columns { get; set; }

        /// <summary>
        /// Override Render base method to avoid creation of <span> elements surrounding the controls output
        /// </summary>
        /// <param name="output"></param>
        protected override void Render(HtmlTextWriter output)
        {
            var html = new StringBuilder();

            // Create table which will hold grid
            html.AppendLine("<table id=\"" + ID + "\"></table>");

            // Pager section
            if (PagingEnabled)
            {
                html.AppendLine("<div id=\"" + ID + "Pager\"></div>");
            }

            // Start javascript
            html.AppendLine("<script type=\"text/javascript\">");             
            html.AppendLine("$(document).ready(function(){");
            html.AppendLine("jQuery('#" + ID + "').jqGrid({");

            // Url
            html.AppendFormat("url: '{0}',", Url).AppendLine();

            // Datatype
            html.AppendLine("datatype: 'json',");

            // Pager
            if (PagingEnabled)
            {
                html.AppendLine("pager: '#" + ID + "',");
            }
            // Width
            if (Width.HasValue)
            {
                html.AppendFormat("width: {0},", Width).AppendLine();
            }
            else
            {
                html.AppendFormat("autowidth: true,").AppendLine();
            }

            // Height
            html.AppendFormat("height: {0},", Height.HasValue ? Height.ToString() : "'100%'").AppendLine();
                        
            // Column names
            html.AppendFormat("colNames:[{0}],", string.Join(",", Columns.Select(x => "'" + x.HeaderText + "'"))).AppendLine();

            // Column model
            html.AppendFormat("colModel:[{0}],", string.Join(",", Columns.Select(x => 
                                "{name:'" + x.DataField + 
                                "', index:'" + x.DataField + "'" +
                                (x.Width.HasValue ? ", width:" + x.Width : "")+ 
                                "}"
                            ))).AppendLine();

            // Postdata
            html.AppendLine(@"serializeGridData: function (postData) {
                                return JSON.stringify(postData);
                            },");

            // Jsonreader
            html.AppendLine(@"
                                jsonReader : { 
                                      root: 'd.rows', 
                                      page: 'd.page', 
                                      total: 'd.total', 
                                      records: 'd.records', 
                                      repeatitems: true, 
                                      cell: 'cell', 
                                      id: 'id',
                                      userdata: 'userdata',
                                      subgrid: { 
                                         root:'rows', 
                                         repeatitems: true, 
                                         cell:'cell' 
                                      } 
                                   },
                            ");

            // Ajax grid options
            html.AppendLine(@"ajaxGridOptions: {
                                contentType: 'application/json; charset=utf-8'
                            },");

            // Request type
            html.AppendLine("mtype: 'POST'");

            // End javascript
            html.AppendLine("});");
            html.AppendLine("});");            
            html.AppendLine("</script>");

            output.Write(html.ToString());
        }

        protected override void AddParsedSubObject(object obj)
        {
            if (obj is JqGridColumn)
            {
                this.Columns.Add((JqGridColumn)obj);
                return;
            }
        }
    }
}
