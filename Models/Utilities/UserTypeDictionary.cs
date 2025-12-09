namespace AirBB.Models.Utilities
{
    public static class UserTypeDictionary
    {
        public static readonly Dictionary<string, string> Types =
            new Dictionary<string, string>
            {
                { "Owner", "Owner" },
                { "Admin", "Admin" },
                { "Client", "Client" }
            };
    }
}
