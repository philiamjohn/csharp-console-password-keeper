public class LogInPage : Printable
{
    private string _username = "";
    public string Username
    {
        get { return _username; }
        set { _username = value; }
    }
    public void PrintPage()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("||                        LOGIN PAGE                      ||");
        Console.WriteLine("||            Provide the following details:              ||");
        Console.WriteLine("||                      > Username                        ||");
        Console.WriteLine("||                      > Password                        ||");
        Console.WriteLine("============================================================");
    }

    public bool LogIn()
    {
        Console.Write("Username: ");
        string? username = Console.ReadLine();

        Console.Write("Password: ");
        string? password = MaskPassword.MaskPasswordToAsterisk();

        Username = username!;
        bool userExist = ReadWriteEncryptDecryptTextFile.IsUserAlreadyRegistered(username!, password!, "login");
        return userExist;
    }
}