namespace TaskMgr.WebApi.ViewModels;

public class RegisterVM
{
    public RegisterVM(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}