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

        [Category("Settings")]
        [DefaultValue(false)]
        public bool ToolbarSearchEnabled
        {
            get
            {
                return ViewState["ToolbarSearchEnabled"] != null ? (bool)ViewState["ToolbarSearchEnabled"] : false;
            }
            set
            {
                ViewState["ToolbarSearchEnabled"] = value;
            }
        }

        [Category("Settings")]
        public string Title
        {
            get
            {
                return ViewState["Title"] != null ? ViewState["Title"].ToString() : null;
            }
            set
            {
                ViewState["Title"] = value;
            }
        }

        [Category("Appearance")]
        public int? RowNumber
        {
            get
            {
                return ViewState["RowNumber"] != null ? (int?)ViewState["RowNumber"] : null;
            }
            set
            {
                ViewState["RowNumber"] = value;
            }
        }

        public bool? ViewRecords
        {
            get
            {
                return ViewState["ViewRecords"] != null ? (bool?)ViewState["ViewRecords"] : null;
            }
            set
            {
                ViewState["ViewRecords"] = value;
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

            // Toolbar search section
            if (ToolbarSearchEnabled)
            {
                html.AppendLine("<table id=\"" + ID + "Search\"></table>");
            }

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

            // Pager
            if (PagingEnabled)
            {
                html.AppendLine("pager: '#" + ID + "Pager',");
            }

            // Title
            if (!string.IsNullOrWhiteSpace(Title))
            {
                html.AppendFormat("caption: '{0}',", Title).AppendLine();
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
              
            // Row number
            if (RowNumber.HasValue)
            {
                html.AppendFormat("rowNum: {0},", RowNumber).AppendLine();
            }

            // View record
            if (ViewRecords.HasValue)
            {
                html.AppendFormat("viewrecords: {0},", ViewRecords.ToString().ToLower()).AppendLine();
            }

            // Column names
            html.AppendFormat("colNames:[{0}],", string.Join(",", Columns.Select(x => "'" + x.HeaderText + "'"))).AppendLine();

            // Column model
            html.AppendFormat("colModel:[{0}],", string.Join(",", Columns.Select(x => 
                                "{name:'" + x.DataField + "'" +
                                ", index:'" + x.DataField + "'" +
                                ", title: false" +
                                (x.Width.HasValue ? ", width:" + x.Width : "")+ 
                                "}"
                            ))).AppendLine();

            // Fixed settings
            html.AppendLine(@"
                                serializeGridData: function (postData) {
                                    return JSON.stringify({ jqGridRequest : postData });
                                },
                                jsonReader : { 
                                    root: 'd.rows', 
                                    page: 'd.page', 
                                    total: 'd.total', 
                                    records: 'd.records', 
                                    repeatitems: false, 
                                    id: 'id',
                                    userdata: 'userdata',
                                    subgrid: { 
                                        root:'rows', 
                                        repeatitems: true, 
                                        cell:'cell' 
                                    } 
                                },
                                prmNames: {
                                    page: 'PageIndex',
                                    rows: 'PageSize', 
                                    sort: 'SortIndex', 
                                    order: 'SortOrder', 
                                    search: 'IsSearch'
                                },
                                ajaxGridOptions: {
                                    contentType: 'application/json; charset=utf-8'
                                },
                                datatype: 'json',
                                mtype: 'POST'");

            // End javascript
            html.AppendLine("});");

            // Toolbar search
            if (ToolbarSearchEnabled)
            {
                html.AppendLine("jQuery('#" + ID + "').jqGrid('filterToolbar', {stringResult:true, searchOnEnter:false});");
            }

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
