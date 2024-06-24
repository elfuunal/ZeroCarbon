using NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.ValidationRules;
using NeyeTech.ZeroCarbon.Core.Aspects.Autofac.Validation;
using NeyeTech.ZeroCarbon.Core.CrossCuttingConcerns.Caching;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.Core.Utilities.Security.Hashing;
using NeyeTech.ZeroCarbon.Core.Utilities.Security.Jwt;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.Entities.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace NeyeTech.ZeroCarbon.Business.Handlers.Authorizations.Queries
{
    public class LoginUserQuery : IRequest<ResponseMessage<UserDto>>
    {
        public LoginUserDto LoginModel { get; set; }

        public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ResponseMessage<UserDto>>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly IMediator _mediator;
            private readonly ICacheManager _cacheManager;

            public LoginUserQueryHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IMediator mediator, ICacheManager cacheManager)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _mediator = mediator;
                _cacheManager = cacheManager;
            }

            public async Task<ResponseMessage<UserDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.Email == request.LoginModel.Email && u.Status && u.IsEmailActive);

                if (user == null)
                    return ResponseMessage<UserDto>.Fail(StatusCodes.Status200OK, "Kullanıcı adı ya da şifre hatalı");

                if (!HashingHelper.VerifyPasswordHash(request.LoginModel.Password, user.PasswordSalt, user.PasswordHash))
                    return ResponseMessage<UserDto>.Fail(StatusCodes.Status200OK, "Kullanıcı adı ya da şifre hatalı");

                var claims = _userRepository.GetClaims(user.Id);
                

                var accessToken = _tokenHelper.CreateToken<AccessToken>(user);
                accessToken.Claims = claims.Select(x => x.Name).ToList();

                user.RefreshToken = accessToken.RefreshToken;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                _cacheManager.Add($"{CacheKeys.UserIdForClaim}={user.Id}", claims.Select(x => x.Name), 3600);

                UserDto userDto = new UserDto()
                {
                    Avatar = user.Avatar,
                    Expiration = accessToken.Expiration,
                    FirstName = user.Firstname,
                    LastName = user.Lastname,
                    RefreshToken = accessToken.RefreshToken,
                    Token = accessToken.Token,
                    UserName = user.Username,
                    Email = user.Email
                };

                return ResponseMessage<UserDto>.Success(userDto);
            }
        }
    }
}
