using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanityArchiver.Application.Models
{
#pragma warning disable SA1600 // Elements should be documented
    public class FileProp
    {
        public string Name { get; set; }

        public string Created { get; set; }

        public string Size { get; set; }

        public bool IsHidden { get; set; }

        public string Extension { get; set; }
    }
#pragma warning restore SA1600 // Elements should be documented
}
