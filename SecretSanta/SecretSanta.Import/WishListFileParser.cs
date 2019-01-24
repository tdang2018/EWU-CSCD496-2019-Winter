using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SecretSanta.Import
{
    public class WishListFileParser : IDisposable
    {
        public StreamReader StreamReader { get; set; }            

        public void OpenImportFile(string fileName)
        {
            if(fileName == null)
            {
                throw new ArgumentNullException("File is null");
            }
            else
            {
                if (!File.Exists(fileName))
                {
                    throw new ArgumentException("File does not exist");
                }
                else
                {
                    StreamReader = new StreamReader(fileName);
                }
            }
        }

        public void CloseImportFile()
        {
            if(StreamReader != null)
            {
                StreamReader.Close();
                StreamReader = null;
            }
        }
     
        public string[] SplitHeader(string header)
        {
            if(header == null)
            {
                throw new ArgumentNullException();
            }
            else if(header ==String.Empty)
            {
                throw new ArgumentException("Header is empty");
            }
            else
            {
                header = header.Trim();
                if(!header.Contains(":") || !header.StartsWith("Name"))
                {
                    throw new ArgumentException("Invalid Header!");         
                }
                string headerNameWithColon = header.Substring(0, header.IndexOf(":"));
                string remainedHeader = header.Substring(header.IndexOf(":") + 1);
                remainedHeader = remainedHeader.Trim();
                //Check format <LastName>, <FirstName>
                if(remainedHeader.Contains(","))
                {
                    string[] names = remainedHeader.Split(",");
                    if(names.Length != 2)
                    {
                        throw new ArgumentException("Missing first name or last name!");
                    }
                    string lastName = remainedHeader.Substring(0, remainedHeader.IndexOf(","));
                    string firstName = remainedHeader.Substring(remainedHeader.IndexOf(",") + 2);              

                    return new string[] { headerNameWithColon, firstName, lastName };
                }
                else //<FirstName> <LastName>
                {
                    string[] names = remainedHeader.Split(" ");
                    if (names.Length != 2)
                    {
                        throw new ArgumentException("Missing first name or last name!");
                    }

                    string firstName = remainedHeader.Substring(0, remainedHeader.IndexOf(" "));
                    string lastName = remainedHeader.Substring(remainedHeader.IndexOf(" ") + 1);                  
                    return new string[] { headerNameWithColon, firstName, lastName };
                }              
            }
        }

        public List<string> ReadListOfGifts()
        {
            List<string> giftNames = new List<string>();

            while (!StreamReader.EndOfStream)
            {
                string line = StreamReader.ReadLine();
                if (line != "")
                {
                    giftNames.Add(line);
                }
            }

            return giftNames;
        }    
            
        public  bool IsFileExists(string curFile)
        {
            return File.Exists(curFile) ? true : false;
        }

        public void Dispose()
        {
            if (StreamReader != null)
            {
                StreamReader.Dispose();
            }
        }
    }
}
