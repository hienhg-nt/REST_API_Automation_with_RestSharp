
public class BookModel
{
    public string? isbn { get; set; }
    public string? title { get; set; }
    public string? subTitle { get; set; }
    public string? author { get; set; }
    public DateTime? publish_Date { get; set; }
    public string? publisher { get; set; }
    public int? pages { get; set; }
    public string? description { get; set; }
    public string? website { get; set; }
}

public class CollectionOfIsbns
{
    public string? isbn { get; set; }
}

public class DeleteAndReplaceBookModel
{
    public string? userId { get; set; }
    public string? isbn { get; set; }
}