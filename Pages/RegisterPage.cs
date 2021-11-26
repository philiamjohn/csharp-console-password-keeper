public class Register : Printable
{
    public void PrintPage()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("||                      REGISTER PAGE                     ||");
        Console.WriteLine("||            Provide the following details:              ||");
        Console.WriteLine("||              > Last Name                               ||");
        Console.WriteLine("||              > First Name                              ||");
        Console.WriteLine("||              > Username                                ||");
        Console.WriteLine("||              > Password (at least 8 characters)        ||");
        Console.WriteLine("============================================================");
    }

    public bool RegisterUser()
    {
        Console.Write("Last Name: ");
        string? lastName = Console.ReadLine();
            
        Console.Write("First Name: ");
        string? firstName = Console.ReadLine();

        Console.Write("Username: ");
        string? username = Console.ReadLine();

        while(true)
        {
            Console.Write("Password: ");
            string? password = MaskPassword.MaskPasswordToAsterisk();

            Console.WriteLine(" ");

            Console.Write("Confirm Password: ");
            string? confirmPassword = MaskPassword.MaskPasswordToAsterisk();
                
            if(password == confirmPassword)
            {
                if(password.Length >= 8)
                {
                    UserModel newUser = new UserModel(lastName!, firstName!, username!, password);

                    return ReadWriteEncryptDecryptTextFile.AddUserToDatabase(newUser);
                }
                Console.WriteLine("\nERROR: password must be at least 8 characters!!!");
            }
            else
            {
                Console.WriteLine("\nERROR: Enter the same password!!!");
            }
        }
    }
}