namespace Majunga.Shared.ViewModels
{
    public class Link
    {
        public int Id { get; set; }
        public string LinkId { get; set; }

        public int FileId { get; set; }
        public File File { get; set; }
    }
}
