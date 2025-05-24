using ManageStudent.Repository.Interfaces;
using MediatR;

namespace ManageStudent.Auth.GetUser;

public class GetUserQuery(int id) : IRequest<ResponseQueryDto>
{
    public int Id { get; } = id;
}


public class GetUSerQueryHandler(IUserRepository repository) : IRequestHandler<GetUserQuery, ResponseQueryDto>
{
    public async Task<ResponseQueryDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {request.Id} not found.");
        }
        return new ResponseQueryDto()
        {
            Id = user.UserId,
            Username = user.Username,
            Email = user.Email,
            IsActive = user.IsActive,
        };
    }
}
