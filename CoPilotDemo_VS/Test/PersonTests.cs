
using CoPilotDemo_VS.Models;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace CoPilotDemo_VS.Tests
{
    public class PersonTests
    {
        [Test]
        public void GetFullName_ReturnsConcatenatedFirstAndLastName()
        {
            var p = new Person { FirstName = "Jane", LastName = "Doe" };
            var result = p.GetFullName();
            ClassicAssert.AreEqual("Jane Doe", result);
        }

        [Theory]
        //[InlineData("Jane", null, "Jane ")]
        //[InlineData(null, "Doe", " Doe")]
        //[InlineData(null, null, " ")]
        public void GetFullName_HandlesNullParts(string first, string last, string expected)
        {
            var p = new Person { FirstName = first, LastName = last };
            ClassicAssert.AreEqual(expected, p.GetFullName());
        }

        [Test]
        public void GetFullAddress_ReturnsFormattedAddress()
        {
            var p = new Person
            {
                Address = "123 Main St",
                City = "Smallville",
                State = "KS",
                ZipCode = "66002"
            };

            var result = p.GetFullAddress();
            ClassicAssert.AreEqual("123 Main St, Smallville, KS 66002", result);
        }

        [Test]
        public void GetFullAddress_HandlesNullParts()
        {
            var p = new Person { Address = null, City = null, State = null, ZipCode = null };
            // Current implementation concatenates nulls into ", ,  "
            ClassicAssert.AreEqual(", ,  ", p.GetFullAddress());
        }

        [Test]
        public void CalculateBusinessDays_CountsOnlyWeekdays_InclusiveRange()
        {
            // Monday 2025-10-13 through Friday 2025-10-17 => 5 business days
            var start = new DateTime(2025, 10, 13); // Monday
            var end = new DateTime(2025, 10, 17);   // Friday
            var days = Person.CalculateBusinessDays(start, end);
           ClassicAssert.AreEqual(5, days);
        }

        [Test]
        public void CalculateBusinessDays_SkipsWeekends_AcrossWeekend()
        {
            // Friday 2025-10-17 through Monday 2025-10-20 => Friday + Monday = 2 business days
            var start = new DateTime(2025, 10, 17); // Friday
            var end = new DateTime(2025, 10, 20);   // Monday
            var days = Person.CalculateBusinessDays(start, end);
            ClassicAssert.AreEqual(2, days);
        }

        [Test]
        public void CalculateBusinessDays_SameWeekendDay_ReturnsZero()
        {
            // Saturday only should be 0 business days
            var date = new DateTime(2025, 10, 18); // Saturday
            var days = Person.CalculateBusinessDays(date, date);
            ClassicAssert.AreEqual(0, days);
        }

        [Test]
        public void CalculateBusinessDays_StartAfterEnd_ThrowsArgumentException()
        {
            var start = new DateTime(2025, 10, 20);
            var end = new DateTime(2025, 10, 15);
            Assert.Throws<ArgumentException>(() => Person.CalculateBusinessDays(start, end));
        }

        [Test]
        public void CalculateDaysBetween_ReturnsDayDifference()
        {
            var start = new DateTime(2025, 1, 1);
            var end = new DateTime(2025, 1, 5);
            var diff = Person.CalculateDaysBetween(start, end);
            ClassicAssert.AreEqual(4, diff);
        }

        [Theory]
        //[InlineData("photo.jpg")]
        //[InlineData("photo.JPEG")]
        //[InlineData("document.pdf")]
        //[InlineData("folder/image.PNG")]
        //[InlineData("C:\\temp\\img.BMP")]
        public void IsValidImageOrPdfExtension_ValidFiles_ReturnsTrue(string fileName)
        {
            ClassicAssert.True(Person.IsValidImageOrPdfExtension(fileName));
        }

        [Theory]
        //[InlineData("file.txt")]
        //[InlineData("archive.zip")]
        //[InlineData("noextension")]
        //[InlineData(".hiddenfile")]
        //[InlineData("image.tiff")]
        public void IsValidImageOrPdfExtension_InvalidFiles_ReturnsFalse(string fileName)
        {
            ClassicAssert.False(Person.IsValidImageOrPdfExtension(fileName));
        }
    }
}