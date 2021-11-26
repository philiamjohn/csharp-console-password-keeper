public class GetAllPasswords : Printable
{
    public void PrintPage()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("||                 GET ALL PASSWORDS PAGE                 ||");
        Console.WriteLine("||            You can get the following details:          ||");
        Console.WriteLine("||                    > Site Name                         ||");
        Console.WriteLine("||                    > Site URL                          ||");
        Console.WriteLine("||                    > Username                          ||");
        Console.WriteLine("||                    > Password                          ||");
        Console.WriteLine("============================================================");
    }
    public void GetPasswords(string passwordKeeperUsername)
    {
        ReadWriteEncryptDecryptTextFile.GetAllPasswords(passwordKeeperUsername);
    }
}