namespace CoPilotDemo_VS.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? Department { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // function to return the full name of the person
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        // function to return the full adress of the person
        public string GetFullAddress()
        {
            return $"{Address}, {City}, {State} {ZipCode}";
        }

        //function that calculates working business days between two dates
        public static int CalculateBusinessDays(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Start date must be earlier than end date.");
            int businessDays = 0;
            DateTime currentDate = startDate;
            while (currentDate <= endDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    businessDays++;
                }
                currentDate = currentDate.AddDays(1);
            }
            return businessDays;
        }

        //function that calculates fays between two dates
        public static int CalculateDaysBetween(DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).Days;
        }

        //q: what is the regex for validation of email address?

        //q:what is the regex for validation of phonenumber? 

        // create a function to validate image extension based on string parameter or it can be a pdf file
        public static bool IsValidImageOrPdfExtension(string fileName)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".pdf" };
            string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
            return Array.Exists(validExtensions, ext => ext == fileExtension);
        }

    }
}
