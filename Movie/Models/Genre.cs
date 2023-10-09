using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Movie.Models
{
    public static class FileNameExtension
    {
          public static List<FileImage> Images(this string? genre) 
        {         
          List<FileImage> images=new List<FileImage>();
          if (genre == null) return images;

          List <string> genres=genre.Split(',').ToList();
            genres.ForEach(g =>
            {
                if (File.Exists($"./wwwroot/images/{g.Trim()}.png"))
                    images.Add(new FileImage(g, $"/images/{g.Trim()}.png"));
            });
  
            return images;
        }
    }
}
