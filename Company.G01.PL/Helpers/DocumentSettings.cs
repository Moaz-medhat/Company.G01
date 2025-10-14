namespace Company.G01.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {

            //string folderPath = "C:\\Users\\AHMED\\source\\repos\\Company.G01\\Company.G01.PL\\wwwroot\\files\\"+ FolderName;
            //string folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + FolderName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory() , @"wwwroot\files\" , FolderName);

            var fileName=$"{Guid.NewGuid()}{file.FileName}";
            var filePath=Path.Combine(folderPath, fileName);
           using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;


        }


        public static void DeleteFile(string fileName,string FolderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files\", FolderName, fileName);
            if(File.Exists(filePath)) 
                File.Delete(filePath);


        }


    }
}
