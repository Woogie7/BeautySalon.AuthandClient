using AutoMapper;
using BeautySalon.AuthandClient.Application.DTO;
using BeautySalon.AuthandClient.Application.Interfaces;
using BeautySalon.AuthandClient.Domain;
using BeautySalon.AuthandClient.Domain.Entity;

namespace BeautySalon.AuthandClient.Infrastructure.Auht;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IPasswordHasher _passwordHasher; 
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;

    public AuthService(
        IUserRepository userRepository,
        IClientRepository clientRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _clientRepository = clientRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> RegisterClientAsync(RegisterClientDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new Exception("Email is already registered");

        var passwordHash = _passwordHasher.HashPassword(dto.Password);
        
        var user = User.Create(dto.Email, passwordHash, UserRole.Client);
        await _userRepository.AddAsync(user);
        
        var client = Client.Create(user.Id, dto.FullName, dto.Phone);
        await _clientRepository.AddAsync(client);
        
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Role.Name);

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

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Role.Name);

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
