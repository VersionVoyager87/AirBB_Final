using AirBB.Models.DomainModels;

namespace AirBB.Models.Grid
{
      public class AppUserGridData
    {        public int AppUserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string UserType { get; set; } = string.Empty;

        public AppUserGridData(AppUser user)
        {
            AppUserId = user.AppUserId;
            Name = user.Name;
            Email = user.Email ?? "N/A";
            PhoneNumber = user.PhoneNumber ?? "N/A";
            DOB = user.DOB ?? DateTime.MinValue;
            UserType = user.UserType;
        }

        public AppUserGridData() { }
    }
}
