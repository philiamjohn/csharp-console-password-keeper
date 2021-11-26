public class UserModel
{
    private string _lastName = "";
    private string _firstName = "";
    private string _username = "";
    private string _password = "";


    public UserModel(string lastName, string firstName, string username, string password)
    {
        LastName = lastName;
        FirstName = firstName;
        Username = username;
        Password =  password;
    }

    public string LastName 
    {
        get { return _lastName; }
        set { _lastName = value; } 
    }
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }
    public string Username
    {
        get { return _username; }
        set { _username = value; }
    }
    public string Password 
    {
        get { return _password; }
        set { _password = value; }
    }

}