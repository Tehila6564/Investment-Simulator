import type { FC } from "react";

interface Props {
  balance: number;
}

export const BalanceDisplay: FC<Props> = ({ balance }) => (
  <h2>Balance: ${balance.toFixed(2)}</h2>
);
