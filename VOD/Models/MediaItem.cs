namespace VOD.Models;
public class MediaItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string VideoUrl { get; set; }
    public string ImageUrl { get; set; }
    public int Views { get; set; }
    public int Likes { get; set; }


    public int Part { get; set; } = 1;
    public string Speaker { get; set; } = string.Empty;

    public List<Comment> Comments { get; set; } = new List<Comment>();
}
