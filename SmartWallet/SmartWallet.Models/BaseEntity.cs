namespace SmartWallet.Models;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastUpdate { get; set; }
}