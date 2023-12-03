using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VOD.Data;
using VOD.Models;
using VOD.Service;

namespace VOD.Controllers;

public class MediaController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IFileUploadService _fileUploadService;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public MediaController(ApplicationDbContext context, IFileUploadService fileUploadService, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _fileUploadService = fileUploadService;
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<IActionResult> VideoList()
    {
        //var mediaItems = await _context.MediaItems
        //    .Include(m => m.Comments)
        //    .ToListAsync();

        var sampleData = SampleData.GetSampleMediaItems();
        return View(sampleData);
    }

    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(MediaUploadViewModel model)
    {
        try
        {
            if (!ModelState.IsValid) return View(model);

            var imageFileName = model?.Image != null ? await _fileUploadService.UploadImageAsync(model.Image) : null;
            var videoFileName = model?.Video != null ? await _fileUploadService.UploadVideoAsync(model.Video) : null;

            if (videoFileName == null || imageFileName == null)
            {
                if (videoFileName == null) ModelState.AddModelError("Video", "Invalid video file.");
                if (imageFileName == null) ModelState.AddModelError("Image", "Invalid image file.");
                return View(model);
            }

            var mediaItem = new MediaItem
            {
                Title = model.Title,
                Description = model.Description,
                VideoUrl = videoFileName != null ? $"{videoFileName}" : null,
                ImageUrl = imageFileName != null ? $"{imageFileName}" : null,
                Views = 0,
                Likes = 0,
                Part = model.VideoPart,
                Speaker = model.VideoSpeaker
            };

            _context.MediaItems.Add(mediaItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("UploadSuccess");
        }
        catch (Exception ex)
        {
            return View("Error");
        }
    }

    // Action for displaying a success page
    public IActionResult UploadSuccess()
    {
        return RedirectToAction("VideoList", "Media");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateLikes(int videoId, bool liked)
    {
        var video = await _context.MediaItems.FindAsync(videoId);
        if (video != null)
        {
            if (liked)
            {
                video.Likes++;
            }
            else
            {
                //add validation to ensure Likes doesn't go negative.
                video.Likes = Math.Max(0, video.Likes - 1);
            }

            await _context.SaveChangesAsync();
            return Json(new { message = $"Likes updated for video with ID {videoId}" });
        }

        return Json(new { message = $"Video with ID {videoId} not found" });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateViews(int videoId)
    {
        var video = await _context.MediaItems.FindAsync(videoId);
        if (video != null)
        {
            video.Views++;
        }
        await _context.SaveChangesAsync();
        return Json(new { message = "Views updated successfully" });
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(int videoId, string name, string text)
    {
        var video = await _context.MediaItems
            .Include(m => m.Comments)
            .FirstOrDefaultAsync(m => m.Id == videoId);

        if (video != null)
        {
            bool requireAuthentication = _configuration.GetSection("Authentication:RequireAuthentication").Get<bool>();
            if (!requireAuthentication && string.IsNullOrEmpty(name))
            {
                return BadRequest("وارد نمودن نام کاربر برای ثبت نظر الزامیست.");
            }

            // Add the new comment
            var newComment = new Comment
            {
                UserName = name,
                Text = text,
                VideoId = videoId
            };

            video.Comments.Add(newComment);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Comment added successfully." });
        }

        return Json(new { success = false, message = "Video not found." });
    }
}

