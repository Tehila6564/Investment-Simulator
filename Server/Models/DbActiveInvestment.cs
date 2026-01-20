using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class DbActiveInvestment
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    public string Name { get; set; } = string.Empty;
    public decimal InvestedAmount { get; set; }
    public decimal ExpectedReturn { get; set; }
    public DateTime EndsAt { get; set; }


    public string Username { get; set; } = string.Empty;

   
    [ForeignKey(nameof(Username))]
    public virtual User User { get; set; } = null!;
}