using AirBB.Models.DomainModels;

namespace AirBB.Models.Grid
{
    
    public class ClientGridData
    {
        public int ClientId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;

        public ClientGridData(Client client)
        {
            ClientId = client.ClientId;
            FullName = client.FullName;
            PhoneNumber = client.PhoneNumber ?? "N/A";
            Email = client.Email ?? "N/A";
            UserType = client.UserType;
        }

        public ClientGridData() { }
    }
}
