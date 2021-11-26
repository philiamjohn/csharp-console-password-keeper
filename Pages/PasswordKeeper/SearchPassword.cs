public class SearchPassword : Printable
{
    public void PrintPage()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("||                 SEARCH PASSWORD PAGE                   ||");
        Console.WriteLine("||                 You can search by:                     ||");
        Console.WriteLine("||                    1 - Site Name                       ||");
        Console.WriteLine("||                    2 - Site URL                        ||");
        Console.WriteLine("||                    3 - Username                        ||");
        Console.WriteLine("============================================================");
    }
    public int EnterOptions()
    {
        while(true)
        {
            Console.Write("Input: ");
            string? optionString = Console.ReadLine();
            int optionInt;

            if (int.TryParse(optionString, out optionInt) && optionInt > 0 && optionInt < 4)
            {
                return optionInt; 
            }
            Console.WriteLine("\nERROR: Enter only '1', '2' or '3'");
            Console.WriteLine(" ");
        }
    }
    public void Search(string searchMethod, string passwordKeeperUsername)
    {
        Console.WriteLine($"SEARCH USING: {searchMethod}");
        while(true)
        {
            Console.Write($"Input {searchMethod}:");
            string? searchKeyword = Console.ReadLine();
            Console.WriteLine(" ");
            Console.WriteLine("Searching...");

            ReadWriteEncryptDecryptTextFile.SearchPassword(searchMethod, passwordKeeperUsername, searchKeyword!);

            Console.WriteLine($"Do you want to continue searching by {searchMethod}?");
            Console.WriteLine("1 - Yes");
            Console.WriteLine("2 - No");

            bool continueSearch = false;
            while(true)
            {
                Console.Write("Input: ");
                string? optionString = Console.ReadLine();
                int optionInt;

                if(int.TryParse(optionString, out optionInt))
                {
                    if(optionInt == 1)
                    {
                        continueSearch = true;
                        break;
                    }
                    else if(optionInt == 2)
                    {
                        break;
                    }
                }
                Console.WriteLine("\nERROR: Enter only '1', or '2'");
                Console.WriteLine(" ");
            }
            if(continueSearch == false)
            {
                Console.WriteLine($"Exiting search by {searchMethod}");
                break;
            }
        }
    }
}