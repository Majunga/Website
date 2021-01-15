using System;
namespace Majunga.Shared.ViewModels
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public byte[] FileBytes { get; set; }
        public string ShareLink { get; set; }
    }
}
