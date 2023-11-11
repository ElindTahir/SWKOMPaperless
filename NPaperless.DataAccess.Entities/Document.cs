namespace NPaperless.DataAccess.Entities;

public class Document
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string OriginalFileName { get; set; }
}