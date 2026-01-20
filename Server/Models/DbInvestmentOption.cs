using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class DbInvestmentOption
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal RequiredAmount { get; set; }
    public decimal ExpectedReturn { get; set; }
    public int DurationSeconds { get; set; }
}