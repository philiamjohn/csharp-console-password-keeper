public class PasswordKeeper : Printable
{
    private string _username = "";
    public string Username
    {
        get { return _username; }
        set { _username = value; }
    }
    public PasswordKeeper (string username)
    {
        Username = username;
    }
    public void PrintPage()
    {
        Console.WriteLine(" ");
        Console.WriteLine("============================================================");
        Console.WriteLine("                        PASSWORD KEEPER                     ");
        Console.WriteLine($"            Hi {Username}! How I may help you?             ");
        Console.WriteLine("                   1 - Get All Passwords                    ");
        Console.WriteLine("                   2 - Add New Password                     ");
        Console.WriteLine("                   3 - Search Password (by site/username)   ");
        Console.WriteLine("                   4 - Logout                               ");
        Console.WriteLine("============================================================");
    }
    public int EnterOptions()
    {
        while(true)
        {
            Console.Write("Input: ");
            string? optionString = Console.ReadLine();
            int optionInt;

            if (int.TryParse(optionString, out optionInt) && optionInt > 0 && optionInt < 5)
            {
                return optionInt; 
            }
            Console.WriteLine("\nERROR: Enter only '1', '2','3' or '4'");
            Console.WriteLine(" ");
        }
    }
    public void GetAllPasswords()
    {
        GetAllPasswords getAllPasswords = new GetAllPasswords();
        getAllPasswords.PrintPage();
        getAllPasswords.GetPasswords(Username);
    }
    public bool AddNewPassword()
    {
        AddNewPassword addNewPassword = new AddNewPassword();
        addNewPassword.PrintPage();
        bool successfullyAddedToDB = addNewPassword.AddPassword(Username);
        return successfullyAddedToDB;      
    }
    public void SearchPassword()
    {
        SearchPassword searchPassword =  new SearchPassword();
        searchPassword.PrintPage();

        int option = searchPassword.EnterOptions();
        if(option == 1)
        {
            searchPassword.Search("sitename", Username);
        }
        else if(option == 2)
        {
            searchPassword.Search("siteurl", Username);
        }
        else if(option == 3)
        {
            searchPassword.Search("username", Username);
        }
    }
}