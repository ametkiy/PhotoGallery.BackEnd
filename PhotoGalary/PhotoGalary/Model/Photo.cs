using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGalary.Model
{
    public class Photo : BaseModel
    {
        //[MaxLength(260)]
        public string FileName { get; set; }
        public string Description { get; set; }

        public int? AlbumId { get; set; }
        public Album Album { get; set; }

        public byte[] PhotoData { get; set; }
        //[Column(TypeName = "DateTime2")]
        public DateTime AddDate { get; set; }
    }
}
