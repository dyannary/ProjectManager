using ProjectManager.Domain.Entities;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ProjectManager.Infrastructure.DataSeeder.Seeds
{
    public class UsersSeed
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static List<User> Seed(Role adminRole, Role userRole)
        {
            return new List<User>()
            {
                new User
                {
                    Id = 0,
                    FirstName = "user", LastName = "user", Email = "user@example.com",
                    Password = HashPassword("user"), UserName = "user",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 1,
                    FirstName = "admin", LastName = "admin", Email = "admin@example.com",
                    Password = HashPassword("admin"), UserName = "admin",
                    IsEnabled = true,
                    Role = adminRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Chris", LastName = "Williams", Email = "chris.williams@example.com",
                    Password = HashPassword("ChrisWilliams456$"), UserName = "chriswilliams",
                    IsEnabled = true,
                    Role = adminRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 3,
                    FirstName = "Ashley", LastName = "Jones", Email = "ashley.jones@example.com",
                    Password = HashPassword("AshleyJones789#"), UserName = "ashleyjones",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 4,
                    FirstName = "David", LastName = "Miller", Email = "david.miller@example.com",
                    Password = HashPassword("DavidMiller123!"), UserName = "davidmiller",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 5,
                    FirstName = "Emma", LastName = "Anderson", Email = "emma.anderson@example.com",
                    Password = HashPassword("EmmaAnderson456@"), UserName = "emmaanderson",
                    IsEnabled = true,
                    Role = adminRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 6,
                    FirstName = "Matthew", LastName = "Taylor", Email = "matthew.taylor@example.com",
                    Password = HashPassword("MatthewTaylor789$"), UserName = "matthewtaylor",
                    Role = adminRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 7,
                    FirstName = "Alex", LastName = "Brown", Email = "alex.brown@example.com",
                    Password = HashPassword("AlexBrown123!"), UserName = "alexbrown",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 8,
                    FirstName = "Emily", LastName = "Johnson", Email = "emily.johnson@example.com",
                    Password = HashPassword("EmilyJohnson456#"), UserName = "emilyjohnson",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 9,
                    FirstName = "Ryan", LastName = "Garcia", Email = "ryan.garcia@example.com",
                    Password = HashPassword("RyanGarcia789$"), UserName = "ryangarcia",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 10,
                    FirstName = "Sophia", LastName = "Martinez", Email = "sophia.martinez@example.com",
                    Password = HashPassword("SophiaMartinez123#"), UserName = "sophiamartinez",
                    IsEnabled = true,
                    Role = adminRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 11,
                    FirstName = "Jacob", LastName = "Hernandez", Email = "jacob.hernandez@example.com",
                    Password = HashPassword("JacobHernandez456!"), UserName = "jacobhernandez",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 12,
                    FirstName = "Emma", LastName = "Smith", Email = "emma.smith@example.com",
                    Password = HashPassword("EmmaSmith123!"), UserName = "emmasmith",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 13,
                    FirstName = "Liam", LastName = "Davis", Email = "liam.davis@example.com",
                    Password = HashPassword("LiamDavis456#"), UserName = "liamdavis",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 14,
                    FirstName = "Olivia", LastName = "Wilson", Email = "olivia.wilson@example.com",
                    Password = HashPassword("OliviaWilson789$"), UserName = "oliviawilson",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 15,
                    FirstName = "Noah", LastName = "Thomas", Email = "noah.thomas@example.com",
                    Password = HashPassword("NoahThomas123!"), UserName = "noahthomas",
                    IsEnabled = true,
                    Role = adminRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 16,
                    FirstName = "Ava", LastName = "Martinez", Email = "ava.martinez@example.com",
                    Password = HashPassword("AvaMartinez456#"), UserName = "avamartinez",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 17,
                    FirstName = "Mia", LastName = "Anderson", Email = "mia.anderson@example.com",
                    Password = HashPassword("MiaAnderson789$"), UserName = "miaanderson",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 18,
                    FirstName = "William", LastName = "Jackson", Email = "william.jackson@example.com",
                    Password = HashPassword("WilliamJackson123!"), UserName = "williamjackson",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 19,
                    FirstName = "Sophia", LastName = "White", Email = "sophia.white@example.com",
                    Password = HashPassword("SophiaWhite456#"), UserName = "sophiawhite",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 20,
                    FirstName = "James", LastName = "Brown", Email = "james.brown@example.com",
                    Password = HashPassword("JamesBrown789$"), UserName = "jamesbrown",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 21,
                    FirstName = "Oliver", LastName = "Garcia", Email = "oliver.garcia@example.com",
                    Password = HashPassword("OliverGarcia123!"), UserName = "olivergarcia",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 22,
                    FirstName = "Charlotte", LastName = "Miller", Email = "charlotte.miller@example.com",
                    Password = HashPassword("CharlotteMiller456#"), UserName = "charlottemiller",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                }, 
                new User
                {
                    Id = 23,
                    FirstName = "Ethan", LastName = "Taylor", Email = "ethan.taylor@example.com",
                    Password = HashPassword("EthanTaylor789$"), UserName = "ethantaylor",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 24,
                    FirstName = "Amelia", LastName = "Thomas", Email = "amelia.thomas@example.com",
                    Password = HashPassword("AmeliaThomas123!"), UserName = "ameliathomas",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 25,
                    FirstName = "Logan", LastName = "Hernandez", Email = "logan.hernandez@example.com",
                    Password = HashPassword("LoganHernandez456#"), UserName = "loganhernandez",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 27,
                    FirstName = "Harper", LastName = "Young", Email = "harper.young@example.com",
                    Password = HashPassword("HarperYoung789$"), UserName = "harperyoung",
                    IsEnabled = true,
                    Role = userRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                },
                new User
                {
                    Id = 28,
                    FirstName = "Mason", LastName = "Martin", Email = "mason.martin@example.com",
                    Password = HashPassword("MasonMartin123!"), UserName = "masonmartin",
                    IsEnabled = true,
                    Role = adminRole,
                    PhotoPath = $"/Content/Images/Default/default_avatar.jpg"
                }
            };
        }
    }
}
