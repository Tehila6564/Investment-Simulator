import { useCountdown } from "../hooks/useCountdown";
import type { ActiveInvestment } from "../types/investment.types";

interface Props {
  inv: ActiveInvestment;
}

export function ActiveInvestmentItem({ inv }: Props) {
  const remaining = useCountdown(inv.secondsRemaining);

  return (
    <li>
      <strong>{inv.name}</strong> | Investment: ${inv.investedAmount} |
      Expected: <span style={{ color: "green" }}>${inv.expectedReturn}</span> |
      {remaining > 0 ? (
        <span style={{ fontWeight: "bold", color: "blue", marginLeft: "10px" }}>
          ⏳ Ends in: {remaining} seconds
        </span>
      ) : (
        <span
          style={{ fontWeight: "bold", color: "orange", marginLeft: "10px" }}
        >
          ⚙️ Processing...
        </span>
      )}
    </li>
  );
}
