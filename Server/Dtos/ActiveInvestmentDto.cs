namespace Server.Dtos
{
   
    public record ActiveInvestmentDto(
      Guid Id, 
      string Name,
      decimal InvestedAmount, 
      decimal ExpectedReturn,
      int SecondsRemaining); 

   
}



