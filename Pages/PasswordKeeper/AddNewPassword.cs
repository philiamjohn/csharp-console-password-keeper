public class AddNewPassword : Printable
{
    public void PrintPage()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("||                 ADD NEW PASSWORD PAGE                  ||");
        Console.WriteLine("||            Provide the following details:              ||");
        Console.WriteLine("||              > Site Name                               ||");
        Console.WriteLine("||              > Site URL                                ||");
        Console.WriteLine("||              > Username                                ||");
        Console.WriteLine("||              > Password (at least 8 characters)        ||");
        Console.WriteLine("||                 ~ Generate random password             ||");
        Console.WriteLine("||                 ~ Enter your own password              ||");
        Console.WriteLine("============================================================");
    }
    public bool AddPassword(string passwordKeeperUsername)
    {
        Console.Write("Site Name: ");
        string? siteName = Console.ReadLine();
            
        Console.Write("Site URL: ");
        string? siteUrl = Console.ReadLine();

        Console.Write("Username: ");
        string? username = Console.ReadLine();

        Console.WriteLine("Password:");
        Console.WriteLine("1 - Generate random password");
        Console.WriteLine("2 - Enter your own password");

        bool successfullyAddedToDB = false;
        int passwordOption = EnterOptions();
        if(passwordOption == 1)
        {
            Console.WriteLine("Generating Random Password");
            string randomPassword = GenerateRandomPassword();
            Console.WriteLine($"Random Generated Password is: {randomPassword}");

            PasswordsModel newPassword = new PasswordsModel(siteName!, siteUrl!, username!, randomPassword);
            successfullyAddedToDB = ReadWriteEncryptDecryptTextFile.AddPasswordToDatabase(newPassword, passwordKeeperUsername);
        }
        else
        {
            while(true)
            {
                Console.Write("Input Password:");
                string? password = MaskPassword.MaskPasswordToAsterisk();

                Console.WriteLine(" ");
                Console.Write("Confirm Password: ");
                string? confirmPassword = MaskPassword.MaskPasswordToAsterisk();
                    
                if(password == confirmPassword)
                {
                    if(password.Length >= 8)
                    {
                        PasswordsModel newPassword = new PasswordsModel(siteName!, siteUrl!, username!, password);
                        successfullyAddedToDB = ReadWriteEncryptDecryptTextFile.AddPasswordToDatabase(newPassword, passwordKeeperUsername);
                        break;
                    }
                    Console.WriteLine("\nERROR: password must be at least 8 characters!!!");
                }
                else
                {
                    Console.WriteLine("\nERROR: Enter the same password!!!");
                }
            }
        }            
        return successfullyAddedToDB;
    }
    private string GenerateRandomPassword()
    {
        string randomPassword = "";
        int asciiSpecialCharacter = new Random().Next(35, 39);
        randomPassword += (char)asciiSpecialCharacter;

        int asciiUpperCaseLetter = new Random().Next(65, 91);
        randomPassword += (char)asciiUpperCaseLetter;

        int asciiNumber = new Random().Next(48, 58);
        randomPassword += (char)asciiNumber;
        asciiNumber = new Random().Next(48, 58);
        randomPassword += (char)asciiNumber;

        int asciiLowerCaseLetter;
        for (int i = 0; i <= 7; i++)
        {
            asciiLowerCaseLetter = new Random().Next(97, 123);
            randomPassword += (char)asciiLowerCaseLetter;
        }

        asciiSpecialCharacter = new Random().Next(35, 39);
        randomPassword += (char)asciiSpecialCharacter;
        return randomPassword;  
    }
    private int EnterOptions()
    {
        while(true)
        {
            Console.Write("Input: ");
            string? optionString = Console.ReadLine();
            int optionInt;

            if (int.TryParse(optionString, out optionInt) && optionInt > 0 && optionInt < 3)
            {
                return optionInt; 
            }
            Console.WriteLine("\nERROR: Enter only '1', '2'");
            Console.WriteLine(" ");
        }
    }
}