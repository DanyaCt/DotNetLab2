using System;

namespace DotNetLab2.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string RegistrationCode { get; set; }

        public override bool Equals(object? obj)
        {
            var client = obj as Client;
            return client.Id == Id && client.FullName == FullName && client.PhoneNumber == PhoneNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FullName, PhoneNumber, RegistrationCode);
        }
    }
}
