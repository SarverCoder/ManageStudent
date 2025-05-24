namespace ManageStudent.Auth.SignUp;

public class SignUpResponseDto
{
    public string Username { get; set; }

    public string PasswordHash { get; set; }

    public string Email { get; set; }
}
