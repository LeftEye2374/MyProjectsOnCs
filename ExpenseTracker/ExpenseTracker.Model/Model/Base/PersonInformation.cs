namespace ExpenseTracker.Model.Base 
{
    public class PersonInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Frist Name: {FirstName}, \n Last Name: {LastName}, \n" +
                $"Age: {Age}, \n Phone Number: {PhoneNumber}, \n Email: {Email}";
        }
    }
}