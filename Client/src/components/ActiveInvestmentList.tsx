import type { FC } from "react";
import type { ActiveInvestment } from "../types/investment.types";

interface Props {
  investments: ActiveInvestment[];
}

export const ActiveInvestmentList: FC<Props> = ({ investments }) => {
  if (investments.length === 0) return <p>No active investments.</p>;

  return (
    <ul>
      {investments.map((inv) => {
        const remaining = Math.max(
          0,
          Math.floor(
            (new Date(inv.secondsRemaining).getTime() - Date.now()) / 1000
          )
        );

        return (
          <li key={inv.id}>
            <strong>{inv.name}</strong> • Principal: $
            {inv.investedAmount.toFixed(2)} • Return: $
            {inv.expectedReturn.toFixed(2)} • Ends in: {remaining}s
          </li>
        );
      })}
    </ul>
  );
};
