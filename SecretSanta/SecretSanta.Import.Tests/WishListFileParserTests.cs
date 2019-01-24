using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace SecretSanta.Import.Tests
{
    [TestClass]
    public class WishListFileParserTests
    {        
        public WishListFileParser WishListFileParser { get; set; }
        public string TempFileName { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            WishListFileParser = new WishListFileParser();            
            TempFileName = CreateTempFile();                       
        }

        public string CreateTempFile()
        {
            string fileName = "";
            try
            {
                fileName = Path.GetTempFileName();
                FileInfo fileInfo = new FileInfo(fileName);
                fileInfo.Attributes = FileAttributes.Temporary;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return fileName;
        }

        public void InsertLinesToTempFile(string tempFile, string[] lineToInsert)
        {
            try
            {
                // Write to the temp file.                
                StreamWriter streamWriter = File.AppendText(tempFile);

                foreach (string s in lineToInsert)
                {
                    streamWriter.WriteLine(s);
                }
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to TEMP file: " + ex.Message);
            }
        }

        private void DeleteTempFile()
        {
            if (File.Exists(TempFileName))
            {
                File.Delete(TempFileName);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteTempFile();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OpenFile_NullPath_ArgumentNullException()
        {
            using (WishListFileParser = new WishListFileParser())
            {
                WishListFileParser.OpenImportFile(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenFile_DoesNotExists_ArgumentException()
        {
            using (WishListFileParser = new WishListFileParser())
            {
                WishListFileParser.OpenImportFile("nonExistingFile.txt");
            }
        }

        [TestMethod]
        public void CheckHeader_FirstNameSpaceLastName_Valid()
        {
            using (WishListFileParser = new WishListFileParser())
            {
                string[] headers = WishListFileParser.SplitHeader("Name: Tuan Dang");
                Assert.AreEqual<string>("Name", headers[0]);
                Assert.AreEqual<string>("Tuan", headers[1]);
                Assert.AreEqual<string>("Dang", headers[2]);
            }
        }

        [TestMethod]
        public void CheckHeader_LastNameCommaFirstName_Valid()
        {
            using (WishListFileParser = new WishListFileParser())
            {
                string[] headers = WishListFileParser.SplitHeader("Name: Dang, Tuan");
                Assert.AreEqual<string>("Name", headers[0]);
                Assert.AreEqual<string>("Tuan", headers[1]);
                Assert.AreEqual<string>("Dang", headers[2]);
            }
        }

        [TestMethod]
        public void CheckHeader_SpaceLastNameSpaceFirstNameSpace_Valid()
        {
            using (WishListFileParser = new WishListFileParser())
            {
                string[] headers = WishListFileParser.SplitHeader(" Name: Tuan Dang ");
                Assert.AreEqual<string>("Name", headers[0]);
                Assert.AreEqual<string>("Tuan", headers[1]);
                Assert.AreEqual<string>("Dang", headers[2]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckHeader_MissingNameTypeCollon_ThrowsArgumentException()
        {
            string[] headers = WishListFileParser.SplitHeader("Tuan Dang");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckHeader_MissingNameType_ThrowsArgumentException()
        {
            string[] headers = WishListFileParser.SplitHeader(":Tuan Dang");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckHeader_MissingCollon_ThrowsArgumentException()
        {
            string[] headers = WishListFileParser.SplitHeader("Name Tuan Dang");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckHeader_MissingFirstName_ThrowsArgumentException()
        {
            string[] headers = WishListFileParser.SplitHeader("Name: Dang");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckHeader_MissingLastName_ThrowsArgumentException()
        {
            string[] headers = WishListFileParser.SplitHeader("Name: Tuan");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckHeader_NullLine_ArgumentNullException()
        {
            using (WishListFileParser = new WishListFileParser())
            {
                string[] header = WishListFileParser.SplitHeader(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckHeader_Empty_ArgumentException()
        {
            using (WishListFileParser = new WishListFileParser())
            {
                string[] headers = WishListFileParser.SplitHeader("");
            }
        }    

        [TestMethod]
        public void CheckWishGifts_IsValid_ReturnsListOfGifts()
        {
            string[] lines = new string[] { "", "Gift1", "", "Gift2", "", "Gift3", "" };

            using (WishListFileParser = new WishListFileParser())
            {                
                InsertLinesToTempFile(TempFileName, lines);
                WishListFileParser.OpenImportFile(TempFileName);
                List<string> gifts = WishListFileParser.ReadListOfGifts();                
                Assert.AreEqual<string>("Gift1", gifts[0]);
                Assert.AreEqual<string>("Gift2", gifts[1]);
                Assert.AreEqual<string>("Gift3", gifts[2]);
            }
            DeleteTempFile();
        }      
    }
}
