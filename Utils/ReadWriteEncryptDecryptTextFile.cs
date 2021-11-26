using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
public class ReadWriteEncryptDecryptTextFile
{
    private static string DecryptAndReadJsonFile(string fileDirectory)
    {
        // encryption key for encryption/decryption 
        byte[] key =
        {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };
        try
        {
            // create file stream
            using FileStream fileStream = new FileStream(fileDirectory, FileMode.Open);
             
            // create instance
            using Aes aes = Aes.Create();
 
            // reads IV value
            byte[] iv = new byte[aes.IV.Length];
            fileStream.Read(iv, 0, iv.Length);
 
            // decrypt data
            using CryptoStream cryptoStream = new CryptoStream(
               fileStream,
               aes.CreateDecryptor(key, iv),
               CryptoStreamMode.Read);
 
            // read stream
            using StreamReader streamReader = new StreamReader(cryptoStream);
 
            string jsonString = streamReader.ReadToEnd();
            return jsonString;
        }
        catch
        {
            Console.WriteLine("Decryption Failed!!!");
            throw;
        }
    }
    private static void EncryptAndWriteJsonFile(string fileDirectory, string json)
    {
        // encryption key for encryption/decryption 
        byte[] key =
        {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };
        try
        {
            // create file stream
            using FileStream fileStream = new FileStream(fileDirectory, FileMode.OpenOrCreate);
 
            // configure encryption key.  
            using Aes aes = Aes.Create();
            aes.Key = key;
 
            // store IV
            byte[] iv = aes.IV;
            fileStream.Write(iv, 0, iv.Length);
 
            // encrypt filestream  
            using CryptoStream cryptoStream = new CryptoStream(
                fileStream,
                aes.CreateEncryptor(),
                CryptoStreamMode.Write);
 
            // write to filestream
            using StreamWriter streamWriter = new StreamWriter(cryptoStream);
            streamWriter.Write(json);
        }
        catch
        { 
            Console.WriteLine("Encryption Failed!!!");
            throw;
        }
    }
    public static void SearchPassword(string searchMethod, string passwordKeeperUsername, string searchKeyword)
    {
        try
        {
            Dictionary<string, List<PasswordsModel>> allPasswords = ReadPasswordsJsonFile();
            if(allPasswords.ContainsKey(passwordKeeperUsername))
            {
               List<PasswordsModel> thisUserPasswords = allPasswords[passwordKeeperUsername];
               List<PasswordsModel> thisUserPasswordsCopy = thisUserPasswords.ToList();

               Console.WriteLine("Search Results:");
               Console.WriteLine(" ");
               bool found = false;
               foreach(PasswordsModel password in thisUserPasswords)
               {
                   if((searchMethod == "sitename" && password.SiteName.ToLower().Contains(searchKeyword.ToLower()))
                   || (searchMethod == "siteurl" && password.SiteUrl.ToLower().Contains(searchKeyword.ToLower()))
                   || (searchMethod == "username" && password.Username.ToLower().Contains(searchKeyword.ToLower())))
                   {
                        Console.WriteLine($"Site Name: {password.SiteName}");
                        Console.WriteLine($"Site URL: {password.SiteUrl}");
                        Console.WriteLine($"Username: {password.Username}");
                        Console.WriteLine($"Password: {password.Password}");
                        Console.WriteLine(" ");
                        found = true;

                        Console.WriteLine("Do you want to DELETE this entry?");
                        Console.WriteLine("1 - YES");
                        Console.WriteLine("2 - NO");

                        while(true)
                        {
                            Console.Write("Input: ");
                            string? optionString = Console.ReadLine();
                            int optionInt;
                            Console.Write(" ");


                            if(int.TryParse(optionString, out optionInt))
                            {
                                if(optionInt == 1)
                                {
                                    thisUserPasswordsCopy.Remove(password);
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
                   }
               }
               if(found == false)
               {
                    Console.WriteLine($"Sorry {passwordKeeperUsername}, No Results found, Try a different keyword.");
                    Console.WriteLine(" ");
               }
               else
               {
                    if(thisUserPasswordsCopy.Count <= 0)
                    {
                        allPasswords.Remove(passwordKeeperUsername);
                    }
                    else
                    {
                        allPasswords[passwordKeeperUsername] = thisUserPasswordsCopy;
                    }
                    string json = JsonSerializer.Serialize(allPasswords);
                    string fileName = "Passwords.json";
                    string fileDirectory = $"{Directory.GetCurrentDirectory()}\\JsonFiles\\{fileName}";

                    //empty Passwords.json bc there would be an error if not emptied
                    File.WriteAllText(fileDirectory, "");
                    EncryptAndWriteJsonFile(fileDirectory, json);
               }
            }
            else
            {
                Console.WriteLine($"Sorry {passwordKeeperUsername}, you don't have any saved password in the DB!");
                Console.WriteLine(" ");
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    public static void GetAllPasswords(string passwordKeeperUsername)
    {
        try
        {
            Dictionary<string, List<PasswordsModel>> allPasswords = ReadPasswordsJsonFile();
            if(allPasswords.ContainsKey(passwordKeeperUsername))
            {
               Console.WriteLine(" ");
               Console.WriteLine($"{passwordKeeperUsername}'s PASSWORDS:");
               List<PasswordsModel> thisUserPasswords = allPasswords[passwordKeeperUsername];
               foreach(PasswordsModel password in thisUserPasswords)
               {
                   Console.WriteLine($"Site Name: {password.SiteName}");
                   Console.WriteLine($"Site URL: {password.SiteUrl}");
                   Console.WriteLine($"Username: {password.Username}");
                   Console.WriteLine($"Password: {password.Password}");
                   Console.WriteLine(" ");
               }
            }
            else
            {
                Console.WriteLine($"Sorry {passwordKeeperUsername}, you don't have any saved password in the DB!");
                Console.WriteLine(" ");
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    public static bool AddPasswordToDatabase(PasswordsModel newPassword, string passwordKeeperUsername)
    {
        bool succesfullyAddedToDB = false;
        try
        {
            Dictionary<string, List<PasswordsModel>> allPasswords = ReadPasswordsJsonFile();
            List<PasswordsModel> thisUserPasswords = new List<PasswordsModel>();
            if(allPasswords.ContainsKey(passwordKeeperUsername))
            {
                thisUserPasswords = allPasswords[passwordKeeperUsername];
                thisUserPasswords.Add(newPassword);
                allPasswords[passwordKeeperUsername] = thisUserPasswords;
            }
            else
            {
                thisUserPasswords.Add(newPassword);
                allPasswords.Add(passwordKeeperUsername, thisUserPasswords);
            }
            string json = JsonSerializer.Serialize(allPasswords);
            string fileName = "Passwords.json";
            string fileDirectory = $"{Directory.GetCurrentDirectory()}\\JsonFiles\\{fileName}";

            EncryptAndWriteJsonFile(fileDirectory, json);

            succesfullyAddedToDB = true;
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        return succesfullyAddedToDB;
    }
    private static Dictionary<string, List<PasswordsModel>> ReadPasswordsJsonFile()
    {
        string fileName = "Passwords.json";
        string fileDirectory = $"{Directory.GetCurrentDirectory()}\\JsonFiles\\{fileName}";

        Dictionary<string, List<PasswordsModel>> allPasswords = new Dictionary<string, List<PasswordsModel>>();

        string jsonString = DecryptAndReadJsonFile(fileDirectory);
        if(jsonString.Length > 0)
        {
            allPasswords = JsonSerializer.Deserialize<Dictionary<string, List<PasswordsModel>>>(jsonString)!;
        }
        return allPasswords;
    }
    public static bool AddUserToDatabase(UserModel newUser)
    {
        bool succesfullyAddedToDB = false;
        try
        {
            List<UserModel> allUsers = ReadUsersJsonFile();
            bool userAlreadyExist = IsUserAlreadyRegistered(newUser.Username, " ", "register");
            if(userAlreadyExist)
            {
                Console.WriteLine("\nERROR: Username already exists!");
                return succesfullyAddedToDB;
            }
            allUsers.Add(newUser);

            string json = JsonSerializer.Serialize(allUsers);
            string fileName = "Users.json";
            string fileDirectory = $"{Directory.GetCurrentDirectory()}\\JsonFiles\\{fileName}";

            EncryptAndWriteJsonFile(fileDirectory, json);
            
            succesfullyAddedToDB = true;
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        return succesfullyAddedToDB;
    }
    private static List<UserModel> ReadUsersJsonFile()
    {
        string fileName = "Users.json";
        string fileDirectory = $"{Directory.GetCurrentDirectory()}\\JsonFiles\\{fileName}";

        List<UserModel> allUsers = new List<UserModel>();

        string jsonString = DecryptAndReadJsonFile(fileDirectory);
        if(jsonString.Length > 0)
        {
            allUsers = JsonSerializer.Deserialize<List<UserModel>>(jsonString)!;
        }
        return allUsers;
    }
    public static bool IsUserAlreadyRegistered(string username, string password, string page)
    {
        bool userExist = false;
        List<UserModel> allUsers = ReadUsersJsonFile();
        
        foreach(UserModel user in allUsers)
        {
            if(page == "login" && user.Username == username && user.Password == password )
            {
                userExist = true;
                break;
            }
            else if(page == "register" && user.Username == username)
            {
                userExist = true;
                break;
            }
        }
        return userExist;
    }
}