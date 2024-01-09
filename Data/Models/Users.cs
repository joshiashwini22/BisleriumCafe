// Importing Enum namespace for Role
using BisleriumCafe.Data.Enums;

//User model is provided as BisleriumCafe.Data.Models namespace
namespace BisleriumCafe.Data.Models;


//Class for User model to store attributes
public class User
{
    //Unique identifier for every user as Guid is provided along with username as string,
    //HashedPassword as string, Role is defiened as Enum.Role
    //Date time that stores user created time details and the id of user who create the new user
    public Guid ID { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public Role Role { get; set; }
    //public bool HasInitialPassword { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid CreatedBy { get; set; }
}