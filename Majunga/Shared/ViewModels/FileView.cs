using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majunga.Shared.ViewModels
{
    public class FileView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShareLink { get; set; }
        public void CreateShareLink()
        {
            this.ShareLink = $"{Guid.NewGuid():N}{Guid.NewGuid():N}";
        }
    }
}
