using System;

namespace Majunga.Shared.ViewModels
{
    public class FileView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShareLink { get; set; }
        public void CreateShareLink()
        {
            this.ShareLink = $"{Guid.NewGuid():N}{Guid.NewGuid():N}";
        }
    }
}
