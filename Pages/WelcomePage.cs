public class WelcomePage : Printable
{
    public void PrintPage()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("||            WELCOME TO PASSWORD KEEPER!!!               ||");
        Console.WriteLine("||                  1 - Log-in                            ||");
        Console.WriteLine("||                  2 - Register                          ||");
        Console.WriteLine("||                  3 - What is Password Keeper?          ||");
        Console.WriteLine("||                  4 - Exit                              ||");
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
}