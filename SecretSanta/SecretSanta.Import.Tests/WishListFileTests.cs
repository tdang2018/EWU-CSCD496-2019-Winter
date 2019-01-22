using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Import.Tests
{
    [TestClass]
    public class WishListFileTests
    {

        [TestMethod]
        public void CheckHeader_FirstNameSpaceLastName_ReturnsTrue()
        {
            WishListFile wishListFile = new WishListFile();
            Assert.AreEqual(true, wishListFile.IsValidHeader("Name: Tuan Dang"));
        }
        [TestMethod]
        public void CheckHeader_EmptyString_ReturnsFalse()
        {
            WishListFile wishListFile = new WishListFile();
            Assert.AreEqual(false, wishListFile.IsValidHeader(""));
        }
        [TestMethod]
        public void CheckHeader_Null_ReturnsFalse()
        {
            WishListFile wishListFile = new WishListFile();
            Assert.AreEqual(false, wishListFile.IsValidHeader(null));
        }
        [TestMethod]
        public void CheckHeader_ValidFirstLast_ReturnsTrue()
        {
            WishListFile wishListFile = new WishListFile();
            Assert.AreEqual(false, wishListFile.IsValidHeader("Name: Grant Woods"));
        }

        [TestMethod]
        public void CheckHeader_FirstNameCommmaLastName_ReturnsTrue()
        {
            WishListFile wishListFile = new WishListFile();
            Assert.AreEqual(true, wishListFile.IsValidHeader("Name: Tuan, Dang"));
        }

        [TestMethod]
        public void CheckHeader_FirstNameSpaceLastName_ReturnsFalse()
        {
            WishListFile wishListFile = new WishListFile();
            Assert.AreEqual(false, wishListFile.IsValidHeader("Name: TuanDang"));
        }

        [TestMethod]
        public void CheckHeader_FirstNameCommmaLastName_ReturnsFalse()
        {
            WishListFile wishListFile = new WishListFile();
            Assert.AreEqual(false, wishListFile.IsValidHeader("Name: Tuan,,Dang"));
        }
        
        [TestMethod]
        public void CheckFile_Exists_ReturnsTrue()
        {
            WishListFile wishListFile = new WishListFile();
            Assert.AreEqual(true, wishListFile.IsFileExists("C:\\CSCD330\\WebServer.java"));
        }

        [TestMethod]
        public void CheckFile_NotExists_ReturnsFalse()
        {
            WishListFile wishListFile = new WishListFile();
            Assert.AreEqual(false, wishListFile.IsFileExists("C:\\CSCD330\\WeServer.java"));
        }
    }
}
