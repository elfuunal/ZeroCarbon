using NeyeTech.ZeroCarbon.Core.Enums;

namespace NeyeTech.ZeroCarbon.Core.Entities.Concrete
{
    public class User : BaseEntity
    {
        public LoginType LoginType { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string RefreshToken { get; set; }
        public bool Status { get; set; }
        public bool IsEmailActive { get; set; }
        public string ActivationCode { get; set; }
        public DateTime ActivationCodeTime { get; set; }
        public DateTime? ActivationTime { get; set; }
        public DateTime? LastLogin { get; set; }
        public string ForgotPasswordCode { get; set; }
        public DateTime? ForgotPasswordCodeTime { get; set; }
        public DateTime? ForgotPasswordTime { get; set; }
    }
}
