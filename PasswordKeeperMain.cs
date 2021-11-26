public class PasswordKeeperMain
{
    public static void Main(string[] args)
    {
        while(true)
        {
            WelcomePage welcomePage = new WelcomePage();
            welcomePage.PrintPage();
            int option = welcomePage.EnterOptions();

            //1 - LogIn, 2 - Register, 3 - What is PasswordKeeper?, 4 exit
            if(option == 1)
            {
                LogInPage logInPage = new LogInPage();
                logInPage.PrintPage();
                bool userExist = logInPage.LogIn();
                if(userExist == true)
                {
                    while(true)
                    {
                        PasswordKeeper passwordKeeper = new PasswordKeeper(logInPage.Username);
                        passwordKeeper.PrintPage();
                        int action = passwordKeeper.EnterOptions();
                        if(action == 1)
                        {
                            passwordKeeper.GetAllPasswords();
                        }
                        else if(action == 2)
                        {
                            bool successfullyAddedToDB = passwordKeeper.AddNewPassword();
                            
                            if(successfullyAddedToDB == true)
                            {
                                Console.WriteLine("\nNew Password is successfully added to the Database");
                                Console.WriteLine(" ");
                            }
                            else
                            {
                                Console.WriteLine("\nNew Password was NOT added to the Database");
                                Console.WriteLine(" ");
                            }
                        }
                        else if(action == 3)
                        {
                            passwordKeeper.SearchPassword();
                        }
                        else
                        {
                            Console.WriteLine($"{logInPage.Username}, Thank you for using Password Keeper! Logging out...");
                            Console.WriteLine(" ");
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nUsername does not exist, or password might be wrong.");
                    Console.WriteLine(" ");
                }
            }
            else if(option == 2)
            {
                Register registerPage = new Register();
                registerPage.PrintPage();
                bool isUserRegistered = registerPage.RegisterUser();
                
                if(isUserRegistered)
                {
                    Console.WriteLine("\nUser is successfuly registered!");
                    Console.WriteLine(" ");
                }
                else
                {
                    Console.WriteLine("\nRegistering attempt failed!");
                    Console.WriteLine(" ");
                }
            }
            else if(option == 3)
            {
                PasswordKeeperInfoPage passwordKeeperInfoPage = new PasswordKeeperInfoPage();
                passwordKeeperInfoPage.PrintPage();
            }
            else
            {
                Console.WriteLine("Thank you for using Password Keeper!!!");
                break;
            }
        }
    }
}