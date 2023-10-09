namespace Movie.Models
{
    public class FileImage
    {   
        public string Name { get; set; }
        public string FileName { get; set; }
        public FileImage(string name, string fileName)
        { 
            Name = name;
            FileName = fileName;

        }
       
    }
}
