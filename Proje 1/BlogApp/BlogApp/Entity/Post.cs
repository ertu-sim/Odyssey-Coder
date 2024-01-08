namespace BlogApp.Entity;
public class Post  //paylaşılan post bilgi ve bağımlılıkları
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Url { get; set; }
    public string? Image { get; set; }
    public DateTime PublishedOn { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; } //referans alınacak  

    public User user { get; set; } = null!;
    public List<Tag> Tags { get; set; } = new List<Tag>();
    public List<Comment> comments { get; set; } = new List<Comment>();
}
