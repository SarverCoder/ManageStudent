using ManageStudent.DataAccess.Entities;
using ManageStudent.Repository.Interfaces;
using ManageStudent.Services;
using MediatR;

namespace ManageStudent.Auth.SignUp;

public class SignUpCommand(SignUpRequestDto dto) : IRequest<SignUpResponseDto>
{
    public SignUpRequestDto Dto { get; } = dto;
}


public class SignUpCommandHandler(IUserRepository repository, IPasswordHasher hasher) : IRequestHandler<SignUpCommand, SignUpResponseDto>
{
    public async Task<SignUpResponseDto> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        var request = command.Dto;

        var user = new DataAccess.Entities.User()
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hasher.HashPassword(request.Password),
            IsActive = true,
            RoleId = 1,
        };

        await repository.AddAsync(user);
        await repository.SaveChangesAsync();

        return new SignUpResponseDto()
        {
            Username = user.Username,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
        };


    }
}