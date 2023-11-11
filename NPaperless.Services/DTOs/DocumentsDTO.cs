namespace NPaperless.Services.DTOs;

public class DocumentsDTO
{
    public class NewDocument
    {
        public string Title { get; set; }
        // Include other properties that are needed when creating a new document
    }
        
    public class UpdateDocument
    {
        public string Title { get; set; }
        // Include other properties that are needed when updating an existing document
    }
}