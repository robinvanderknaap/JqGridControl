using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace JqGridControl
{
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class JqGridColumn
    {
        private List<SearchOption> _searchOptions = new List<SearchOption>();
        
        public string HeaderText { get; set; }
        public string DataField { get; set; }
        public int? Width { get; set; }
        public bool DatePicker { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<SearchOption> SearchOptions
        {
            get { return _searchOptions; }
            set { _searchOptions = value; }
        }
        
    }
}
