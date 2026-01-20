namespace Server.Dtos
{
    public record UserStateDto(
        decimal Balance,
        List<ActiveInvestmentDto> ActiveInvestments,
        DateTime LastBalanceUpdate 
    );
}