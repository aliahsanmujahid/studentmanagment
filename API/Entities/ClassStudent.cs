using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ClassStudent
    {
        public int ClassId { get; set; }
        public int AppUserId { get; set; }

        public Class Class { get; set; }
        public AppUser AppUser { get; set; }
        
    }
}