using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NID { get; set; }
        public int Age
        {
            get
            {
                if (NID == null)
                {
                    throw new ArgumentNullException(nameof(NID));
                }
                if (NID.Length >= 6 && int.TryParse(NID.Substring(1, 6), out int birthdate))
                {
                    int birthYear = birthdate / 10000;
                    int birthMonth = birthdate / 100 % 100;
                    int birthDay = birthdate % 100;
                    int fullYear;
                    if (birthYear >= 0 && birthYear <= 99)
                    {
                        fullYear = birthYear < 50 ? 2000 + birthYear : 1900 + birthYear;
                    }
                    else
                    {
                        fullYear = birthYear;
                    }
                    int currentYear = DateTime.Now.Year;
                    int calculatedAge = currentYear - fullYear;
                    if (birthMonth > DateTime.Now.Month || birthMonth == DateTime.Now.Month && birthDay > DateTime.Now.Day)
                    {
                        calculatedAge--;
                    }
                    return calculatedAge;
                }
                return 0;
            }
        }
        public Enums.Enums.GenderType? Gender
        {
            get
            {
                if (NID == null)
                {
                    throw new ArgumentNullException(nameof(NID));
                }
                char genderchar = NID[12];
                if (char.IsDigit(genderchar))
                {
                    int GenderNumber = int.Parse(genderchar.ToString());
                    return GenderNumber % 2 == 1 ? Enums.Enums.GenderType.Male : Enums.Enums.GenderType.Female;
                }
                throw new ArgumentException("Enter the NID first");
            }
        }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<ChatBotMessages> ChatMessages { get; set; } = new List<ChatBotMessages>();
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<ChatMember> ChatMembers { get; set; } = new List<ChatMember>();
//        public List<RefreshToken>? RefreshTokens { get; set; }

    }
}
