namespace SmartWallet.Models;

public class User : BaseEntity
{
    public virtual Person Person { get; set; } = default!;
    public Guid PersonId { get; set; }
    public virtual ContactInfo ContactInfo { get; set; } = default!;
    public Guid ContactInfoId { get; set; }
    public virtual Verification? Verification { get; set; }
    public Guid? VerificationId { get; set; }
    public virtual Role Role { get; set; } = default!;
    public Guid RoleId { get; set; }

    public ICollection<Project> Projects { get; set; } = [];
}