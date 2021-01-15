using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majunga.Shared.ViewModels
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] FileBytes { get; set; }
    }
}
