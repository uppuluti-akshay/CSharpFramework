using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowApiFramework.Models
{
    public class ApiModels
    {
        public Object Header { get; set; }
        public Object Items { get; set; }

       

    }

    public class Header
    {
        public string NameOfUser { get; set; }
        public string DescriptionOfUser { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Iteams
    {
        public decimal age { get; set; }
        public string UserOfItems { get; set; }
        public DateTime timeOfExecution { get; set; }
        public string asOfDate { get; set; }
        public string asOfTime { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }


}
