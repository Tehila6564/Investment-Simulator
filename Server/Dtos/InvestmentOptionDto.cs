namespace Server.Dtos
{
    public record InvestmentOptionDto(
     string Id,
     string Name,
     decimal RequiredAmount,
     decimal ExpectedReturn, 
     int DurationSeconds);

    
}