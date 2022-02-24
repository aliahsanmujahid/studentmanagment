using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public int AppUserId { get; set; }
        public ICollection<ClassStudent> ClassStudents { get; set; }

    }
}