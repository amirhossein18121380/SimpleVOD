using VOD.Models;

namespace VOD.Service;
public class SampleData
{

    public static List<MediaItem> GetSampleMediaItems()
    {
        var mediaItems = new List<MediaItem>
        {
            new MediaItem
            {
                Id = 1,
                Title = "Introduction to Programming",
                Description = "Learn the basics of programming with this introductory video.",
                VideoUrl = $"/videos/05371370-3838-47c8-a014-cdc7e0481115_roka-video-2.mp4",
                ImageUrl = $"/images/7cf66ee3-4008-490a-a1b5-9c9ac763e700_green_texture-wallpaper-1920x1080.jpg",
                Views = 1000,
                Likes = 150,
                Part = 1,
                Speaker = "John Doe",
                Comments = new List<Comment>
                {
                    new Comment { Id = 1, VideoId = 1, UserName = "User1", Text = "Great video!" },
                    new Comment { Id = 2, VideoId = 1, UserName = "User2", Text = "I learned a lot." }
                }
            },
            new MediaItem
            {
                Id = 2,
                Title = "Advanced Data Structures",
                Description = "Explore advanced data structures and algorithms.",
                VideoUrl = $"/videos/05371370-3838-47c8-a014-cdc7e0481115_roka-video-2.mp4",
                ImageUrl = $"/images/5fb54077-42b6-4ac5-942a-634408b8ee92_9.jpg",
                Views = 300,
                Likes = 50,
                Part = 2,
                Speaker = "Jane Smith",
                Comments = new List<Comment>
                {
                    new Comment { Id = 3, VideoId = 2, UserName = "User3", Text = "Very informative!" },
                    new Comment { Id = 4, VideoId = 2, UserName = "User4", Text = "I need more explanations on the third topic." }
                }
            },
            new MediaItem
            {
                Id = 3,
                Title = "Advanced Data Structures",
                Description = "Explore advanced data structures and algorithms.",
                VideoUrl = $"/videos/55f4f212-ec06-4db2-a023-c42f374238d7_roka-video-4.mp4",
                ImageUrl = $"/images/e14076be-ffa2-41ff-a4bc-1c0dfb72189b_bungalows-3840x2160-4k-hd-wallpaper-reef-french-polynesia-water-594.jpg",
                Views = 800,
                Likes = 120,
                Part = 3,
                Speaker = "Jane Smith",
                Comments = new List<Comment>
                {
                    new Comment { Id = 3, VideoId = 2, UserName = "User3", Text = "Very informative!" },
                    new Comment { Id = 4, VideoId = 2, UserName = "User4", Text = "I need more explanations on the third topic." }
                }
            }
            // Add more sample media items as needed
        };

        return mediaItems;
    }
}