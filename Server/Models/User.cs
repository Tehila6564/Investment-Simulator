using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class User
{
    [Key]
    public string Username { get; set; } = string.Empty;

    public decimal Balance { get; set; } = 5000m;

    public List<DbActiveInvestment> ActiveInvestments { get; set; } = new();
}