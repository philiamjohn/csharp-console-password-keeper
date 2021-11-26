public class PasswordsModel
{
    private string _siteName = "";
    private string _siteUrl = "";
    private string _username = "";
    private string _password = "";


    public PasswordsModel(string siteName, string siteUrl, string username, string password)
    {
        SiteName = siteName;
        SiteUrl = siteUrl;
        Username = username;
        Password =  password;
    }

    public string SiteName 
    {
        get { return _siteName; }
        set { _siteName = value; } 
    }
    public string SiteUrl
    {
        get { return _siteUrl; }
        set { _siteUrl = value; }
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