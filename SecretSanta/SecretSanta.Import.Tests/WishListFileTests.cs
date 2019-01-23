using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Import.Tests
{
    [TestClass]
    public class WishListFileTests
    {
        public string[] Lines;
        public WishListFile WishListFile=null;
        public string TempFileName;

        [TestInitialize]
        public void TestInitialize()
        {
            WishListFile = new WishListFile();
            //use to create test file
            TempFileName = WishListFile.CreateTempFile();
            //Lines = WishListFile.OpenFile("wishlistuser.txt");           
            Lines = WishListFile.OpenFile(TempFileName);
        }
       
        [TestMethod]
        public void CheckHeader_FirstNameSpaceLastName_ReturnsTrue()
        {
            WishListFile.InsertLineToTempFile(TempFileName, "Name: Tuan Dang");
            Lines = WishListFile.OpenFile(TempFileName);
            Assert.AreEqual<bool>(true, WishListFile.IsValidHeader(Lines[0]));
        }

        [TestMethod]
        public void CheckHeader_EmptyString_ReturnsFalse()
        {
            WishListFile.InsertLineToTempFile(TempFileName, "");
            Lines = WishListFile.OpenFile(TempFileName);
            Assert.AreEqual<bool>(false, WishListFile.IsValidHeader(""));
        }

        [TestMethod]
        public void CheckHeader_Null_ReturnsFalse()
        {
            WishListFile.InsertLineToTempFile(TempFileName, "");
            Lines = WishListFile.OpenFile(TempFileName);
            Assert.AreEqual<bool>(false, WishListFile.IsValidHeader(null));
        }

        [TestMethod]
        public void CheckHeader_ValidFirstLast_ReturnsTrue()
        {
            WishListFile.InsertLineToTempFile(TempFileName, "Name: Grant Woods");
            Lines = WishListFile.OpenFile(TempFileName);
            Assert.AreEqual<bool>(false, WishListFile.IsValidHeader(Lines[0]));
        }

        [TestMethod]
        public void CheckHeader_FirstNameCommmaLastName_ReturnsTrue()
        {
            WishListFile.InsertLineToTempFile(TempFileName, "Name: Tuan, Dang");
            Lines = WishListFile.OpenFile(TempFileName);
            Assert.AreEqual<bool>(true, WishListFile.IsValidHeader(Lines[0]));
        }

        [TestMethod]
        public void CheckHeader_FirstNameSpaceLastName_ReturnsFalse()
        {
            WishListFile.InsertLineToTempFile(TempFileName, "Name: TuanDang");
            Lines = WishListFile.OpenFile(TempFileName);
            Assert.AreEqual<bool>(false, WishListFile.IsValidHeader(Lines[0]));
        }

        [TestMethod]
        public void CheckHeader_FirstNameCommmaLastName_ReturnsFalse()
        {
            WishListFile.InsertLineToTempFile(TempFileName, "Name: Tuan,,Dang");
            Lines = WishListFile.OpenFile(TempFileName);
            Assert.AreEqual<bool>(false, WishListFile.IsValidHeader(Lines[0]));
        }
        [TestMethod]
        public void CheckHeader_FirstNamePlusLastName_ReturnsFalse()
        {
            WishListFile.InsertLineToTempFile(TempFileName, "Name: Tuan+Dang");
            Lines = WishListFile.OpenFile(TempFileName);
            Assert.AreEqual<bool>(false, WishListFile.IsValidHeader(Lines[0]));
        }
        [TestMethod]
        public void CheckHeader_FirstNameMinusLastName_ReturnsFalse()
        {
            WishListFile.InsertLineToTempFile(TempFileName, "Name: Tuan-Dang");
            Lines = WishListFile.OpenFile(TempFileName);
            Assert.AreEqual<bool>(false, WishListFile.IsValidHeader(Lines[0]));
        }

    }
}
