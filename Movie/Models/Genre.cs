using System.Drawing;

namespace Movie.Models
{
    public class FileImage
    {   
        public string Name { get; set; }
        public byte[] FileName { get; set; }
        public FileImage(string name, string fileName)
        { 
            Name = name;
            FileName = File.ReadAllBytes(fileName);

           // FileName = fileName;
        }
       
    }
    public static class FileNameExtension
    {   
        
        public static List<FileImage> Images(this string? genre) 
        {         
          List<FileImage> images=new List<FileImage>();
          if (genre == null) return images;

          List <string> genres=genre.Split(',').ToList();  
          genres.ForEach(g=> {
                if (File.Exists($"Resources/{g}.png"))
                    images.Add(new FileImage(g, $"../movie/Resources/{g}.png"));
            });      

        return images;
        }
    }
}
