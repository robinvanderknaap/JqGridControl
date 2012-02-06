using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

namespace JqGridControl
{
    public class JqGridRequest
    {
        private Filter _where;
        
        public bool IsSearch { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string SortIndex { get; set; }
        public string SortOrder { get; set; }
        public string filters { get; set; }
        public Filter Where
        {
            get 
            {
                if (_where == null && !string.IsNullOrWhiteSpace(filters))
                {
                    _where = Filter.Create(filters);
                }
                return _where; 
            }
        }        
    }

    [DataContract]
    public class Filter
    {
        [DataMember]
        public string groupOp { get; set; }
        [DataMember]
        public Rule[] rules { get; set; }

        public static Filter Create(string jsonData)
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(Filter));
                var ms = new System.IO.MemoryStream(Encoding.Default.GetBytes(jsonData));
                return serializer.ReadObject(ms) as Filter;
            }
            catch
            {
                return null;
            }
        }
    }

    [DataContract]
    public class Rule
    {
        [DataMember]
        public string field { get; set; }
        [DataMember]
        public string op { get; set; }
        [DataMember]
        public string data { get; set; }
    }
}
