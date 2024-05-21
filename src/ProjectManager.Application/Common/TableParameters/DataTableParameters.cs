using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Linq;

namespace ProjectManager.Application.TableParameters
{
    [Serializable]
    [DataContract]
    public class DataTableParameters
    {
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }

        [DataMember(Name = "draw")]
        public int Draw { get; set; }

        [DataMember(Name = "start")]
        public int Start { get; set; }

        [DataMember(Name = "length")]
        public int Length { get; set; }

        [DataMember(Name = "columns")]
        public List<DataTableColumn> Columns { get; set; }

        [DataMember(Name = "search")]
        public DataTableSearch Search { get; set; }

        [DataMember(Name = "order")]
        public List<DataTableOrder> Order { get; set; }

        /// <summary>
        /// Used for sorting
        /// </summary>
        public void SetColumnName()
        {
            foreach (var item in Order)
            {
                item.Name = Columns[item.Column].Data;
            }
        }
        /// <summary>
        /// Gets the <see cref="DataTableColumn"/> with the specified column name.
        /// </summary>
        /// <value>
        /// The <see cref="DataTableColumn"/>.
        /// </value>
        /// <param name="columnName">The column name.</param>
        /// <returns></returns>
        public DataTableColumn this[string columnName] => Columns.FirstOrDefault(x => x.Data == columnName);
    }
}
