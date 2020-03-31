using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanityArchiver.Application.Models
{
    /// <summary>
    /// Contains the neccessary information about the files
    /// </summary>
    public class FileProp
    {
        #region Getters and Setters
        public string Name { get; set; }

        public string Created { get; set; }

        public string Size { get; set; }

        public bool IsHidden { get; set; }

        public string Extension { get; set; }
        #endregion
    }
}
