import { useInvestmentData } from "../hooks/useInvestmentData";
import { useInvest } from "../hooks/useInvest";
import { ActiveInvestmentItem } from "../components/ActiveInvestmentItem";
import { AvailableInvestments } from "../components/AvailableInvestments";
import { Box, Typography, Container } from "@mui/material";
import { formatLastUpdate } from "../utils/dateFormatter";

export default function Dashboard() {
  const { balance, active, available, loading, error, lastBalanceUpdate } =
    useInvestmentData();
  const { invest, processingId } = useInvest();

  if (loading) return <Typography sx={{ p: 4 }}>Loading...</Typography>;
  if (error)
    return (
      <Typography color="error" sx={{ p: 4 }}>
        Error: {error}
      </Typography>
    );

  const username = localStorage.getItem("investment_username") || "User";

  return (
    <Container maxWidth={false} sx={{ py: 4, width: "100vw" }}>
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          width: "100%",
          mb: 4,
          p: 3,
          border: "1px solid black",
          boxSizing: "border-box",
        }}
      >
        <Typography variant="h4" sx={{ fontWeight: "bold" }}>
          Hello, {username}
        </Typography>

        <Box sx={{ textAlign: "right" }}>
          <Typography variant="h5" sx={{ fontWeight: "bold" }}>
            Current Balance: ${balance.toFixed(2)}
          </Typography>
          <Typography variant="body2" color="textSecondary">
            Last Update: {formatLastUpdate(lastBalanceUpdate)}
          </Typography>
        </Box>
      </Box>

      <Box
        sx={{
          width: "100%",
          border: "1px solid black",
          mb: 4,
          p: 2,
          boxSizing: "border-box",
        }}
      >
        <Typography
          variant="h6"
          align="center"
          sx={{ mb: 2, borderBottom: "1px solid #ccc", pb: 1 }}
        >
          Current Investments
        </Typography>

        <Box sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
          {active.length === 0 ? (
            <Typography align="center">No active investments</Typography>
          ) : (
            active.map((inv) => (
              <Box
                key={inv.id}
                sx={{
                  width: "100%",
                  border: "1px solid #eee",
                  p: 2,
                  boxSizing: "border-box",
                }}
              >
                <ActiveInvestmentItem inv={inv} />
              </Box>
            ))
          )}
        </Box>
      </Box>

      <Box
        sx={{
          width: "100%",
          border: "1px solid black",
          p: 2,
          boxSizing: "border-box",
        }}
      >
        <Typography
          variant="h6"
          align="center"
          sx={{ mb: 2, borderBottom: "1px solid #ccc", pb: 1 }}
        >
          Available Investments
        </Typography>

        <AvailableInvestments
          options={available}
          activeInvestments={active}
          balance={balance}
          processingId={processingId}
          onInvest={invest}
        />
      </Box>
    </Container>
  );
}
