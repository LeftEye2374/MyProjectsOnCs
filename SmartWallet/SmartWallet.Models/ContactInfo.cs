using System.Net;

namespace SmartWallet.Models;

public class ContactInfo : BaseEntity
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public virtual Address? Address { get; set; }
    public Guid? AddressId { get; set; }
}