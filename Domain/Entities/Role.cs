using Domain.Common;

namespace Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;

    public List<User> Users { get; set; } = new();
}