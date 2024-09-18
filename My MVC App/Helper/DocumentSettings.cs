using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace My_MVC_App.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1. Get location folder path
            //string folderPath = "C:\\Users\\Youssef Ahmed\\Desktop\\Route\\Assignments\\MVC\\My MVC App\\My MVC App\\wwwroot\\Files\\" + folderName;
            //string folderPath = Directory.GetCurrentDirectory() + "wwwroot\\Files\\" + folderName;

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName);

            //2. Get file name make it UNIQUE
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. Get file path --> folder path + file name
            string filePath = Path.Combine(folderPath, fileName);

            //4. Save file as stream : data per time
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
             
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName, fileName);

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
