using BisleriumCafe.Data.Models;
using BisleriumCafe.Data.Enums;
using BisleriumCafe.Data.Utils;
using System.ComponentModel.DataAnnotations;

namespace BisleriumCafe.Data.Forms
{
    public class UserAdditionForm
    {
        [Required] public string Username { get; set; }
        [Required] public Role Role { get; set; }
        [Required] public string Password { get; set; }

        public User GetUser()
        {
            return new User { Username = this.Username, Role = this.Role, HashedPassword = Hasher.HashedSecret(this.Password) };
        }
    }
}