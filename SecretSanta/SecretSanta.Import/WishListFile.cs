using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SecretSanta.Import
{
    public class WishListFile
    {
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
