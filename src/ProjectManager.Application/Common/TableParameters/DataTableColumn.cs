using System;
using System.Runtime.Serialization;

namespace ProjectManager.Application.TableParameters
{
    [Serializable]
    [DataContract]
    public class DataTableColumn
    {
        [DataMember(Name = "data")]
        public string Data { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "searchable")]
        public bool Searchable { get; set; }

        [DataMember(Name = "orderable")]
        public bool Orderable { get; set; }

        [DataMember(Name = "search")]
        public DataTableSearch Search { get; set; }
    }
}
