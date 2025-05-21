using AutoMapper;
using BeautySalon.AuthandClient.Application.DTO;
using BeautySalon.AuthandClient.Application.Interfaces;
using BeautySalon.AuthandClient.Domain;
using BeautySalon.AuthandClient.Domain.Entity;
using BeautySalon.Contracts;

namespace BeautySalon.AuthandClient.Infrastructure.Auh;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IPasswordHasher _passwordHasher; 
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEventBus _eventBus;

    public AuthService(
        IUserRepository userRepository,
        IClientRepository clientRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IEventBus eventBus)
    {
        _userRepository = userRepository;
        _clientRepository = clientRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _eventBus = eventBus;
    }

    public async Task<AuthResponseDto> RegisterClientAsync(RegisterUserDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new Exception("Email is already registered");

        var passwordHash = _passwordHasher.HashPassword(dto.Password);
        
        var role = dto.Role is not null
            ? Enumeration.FromDisplayName<UserRole>(dto.Role)
            : UserRole.Client;
        
        var user = User.Create(dto.Email, passwordHash, role);
        await _userRepository.AddAsync(user);
        switch (role.Name)
        {
            case "Client":
                var client = Client.Create(user.Id, dto.FirstName + "" + dto.LastName, dto.Phone);
                await _clientRepository.AddAsync(client);
                break;
        
            case "Employee":
                await _eventBus.SendMessageAsync(new EmployeeCreatedEvent
                    {
                        UserId = user.Id,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Phone = dto.Phone,
                        Email = dto.Email
                    });
                break;

            default:
                throw new Exception("Unsupported role");
        }
        
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Role.Name, user.Email);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role.ToString(),
            Token = token
        };
    }

    public async Task<AuthResponseDto> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
            throw new Exception("Invalid email or password");

        var validPassword = _passwordHasher.VerifyPassword(password, user.PasswordHash);
        if (!validPassword)
            throw new Exception("Invalid email or password");

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Role.Name, email);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role.ToString(),
            Token = token
        };
    }

    public Task<bool> ValidateTokenAsync(string token)
    {
        return Task.FromResult(_jwtTokenGenerator.ValidateToken(token));
    }
}
