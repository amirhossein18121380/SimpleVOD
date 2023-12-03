using System.ComponentModel.DataAnnotations;

namespace VOD.Models;

public class MediaUploadViewModel
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Video file is required")]
    [DataType(DataType.Upload)]
    public IFormFile Video { get; set; }

    [Required(ErrorMessage = "Video file is required")]
    [DataType(DataType.Upload)]
    public IFormFile Image { get; set; }

    public int VideoPart { get; set; } = 1;
    public string VideoSpeaker { get; set; } = string.Empty;
}
