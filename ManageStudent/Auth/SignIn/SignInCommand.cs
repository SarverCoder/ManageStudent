using ManageStudent.Repository.Interfaces;
using ManageStudent.Services;
using MediatR;

namespace ManageStudent.Auth.SignIn;

public class SignInCommand(SignInRequestDto request) : IRequest<SignInResponseDto>
{
    public SignInRequestDto Request { get; } = request;
}


public class SignInCommandHandler(IUserRepository repository,
    IAuthService authService,
    IPermissionService permissionService,
    IPasswordHasher hasher
) : IRequestHandler<SignInCommand, SignInResponseDto>
{
    public async Task<SignInResponseDto> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = await repository.GetByUsernameAsync(request.Username);

        if (user == null)
        {
            throw new Exception();
        }

        var isValidPassword = hasher.VerifyHash(request.Password, user.PasswordHash);

        var token = await authService.GenerateJwtToken(user.UserId, user.Email,user.Role?.Name);

        return new SignInResponseDto()
        {
            AccessToken = token,
        };
    }
}