export interface InvestmentOption {
  id: string;
  name: string;
  requiredAmount: number;
  expectedReturn: number;
  durationSeconds: number;
}

export interface ActiveInvestment {
  id: string;
  name: string;
  investedAmount: number;
  expectedReturn: number;
  secondsRemaining: number;
}

export interface UserState {
  balance: number;
  activeInvestments: ActiveInvestment[];
  lastBalanceUpdate: string;
}
