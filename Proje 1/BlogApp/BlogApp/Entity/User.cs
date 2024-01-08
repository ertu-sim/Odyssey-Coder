using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BlogApp.Entity;
public class User //Kullanıcı bilgi ve bağımlılıkları
{
    public int UserId { get; set; }
    public string? UserName { get; set; }    //burada fk hatası aldığım için girişi sadece username ile yapacağız

    public string? Image { get; set; }
    public List<Post> posts { get; set; } = new List<Post>();

    public List<Comment> comments { get; set; } = new List<Comment>();
}
