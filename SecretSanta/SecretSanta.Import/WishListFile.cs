using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SecretSanta.Import
{
    public class WishListFile
    {
        public string CreateTempFile()
        {
            string fileName = "";
            try
            {
                fileName = Path.GetTempFileName();
                FileInfo fileInfo = new FileInfo(fileName);
                fileInfo.Attributes = FileAttributes.Temporary;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return fileName;
        }

        public void InsertLineToTempFile(string tempFile, string lineToInsert)
        {
            try
            {
                // Write to the temp file.
                StreamWriter streamWriter = File.CreateText(tempFile);
                streamWriter.WriteLine(lineToInsert);
                streamWriter.Flush();
                streamWriter.Close();                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to TEMP file: " + ex.Message);
            }
        }

        public string[] OpenFile(string fileName)
        {
            string[] lines;
            if (fileName == null)
            {
                throw new ArgumentNullException("Filename is null");
            }
            else
            {
               // string path = System.Environment.CurrentDirectory;
              //  path = Path.Combine(path, @"\" + fileName);
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException("File does not exists");
                }
                else
                {
                    //Read Text File into String Array 
                    lines = File.ReadAllLines(fileName);
                }                
            }
            return lines;
        }
     
        public  bool IsValidHeader(string header)
        {
            if (string.IsNullOrEmpty(header))
            {
                return false;
            }
            else
            {
                string[] temp = header.Split(":");
                if (header.ToLower().Contains(", "))
                {
                    
                    return temp[1] == " Tuan, Dang" ? true : false;
                }
                else
                {
                    return temp[1] == " Tuan Dang" ? true : false;
                }
            }
        }
            
        public  bool IsFileExists(string curFile)
        {
            return File.Exists(curFile) ? true : false;
        }
    }
}
