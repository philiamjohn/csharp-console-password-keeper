public class MaskPassword
{
    public static string MaskPasswordToAsterisk()  
    {   string password = "";
        try  
        {   
            ConsoleKey key;
            do
            {
                //when intercept is true, the typed character is not displayed in the console
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    //erase the displayed asterisk when backspace is pressed
                    Console.Write("\b \b");
                    //remove the last character of the string
                    password = password.Remove(password.Length-1);
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    //print asterisk, "*" instead
                    Console.Write("*");
                    //append the input character to password
                    password += keyInfo.KeyChar;
                }
            } 
            while (key != ConsoleKey.Enter); 
        }  
        catch (Exception e)  
        {  
            Console.WriteLine("Exception" + e);
        }
        return password;
    } 
}