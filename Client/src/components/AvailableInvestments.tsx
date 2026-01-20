import type { FC } from "react";
import type {
  InvestmentOption,
  ActiveInvestment,
} from "../types/investment.types";

interface Props {
  options: InvestmentOption[];
  activeInvestments: ActiveInvestment[];
  balance: number;
  processingId: string | null; 
  onInvest: (id: string) => void;
}

export const AvailableInvestments: FC<Props> = ({
  options,
  activeInvestments,
  balance,
  processingId,
  onInvest,
}) => {
  return (
    <div
      style={{
        display: "grid",
        gap: "1rem",
        gridTemplateColumns: "repeat(auto-fill, minmax(280px, 1fr))",
        marginTop: "10px",
      }}
    >
      {options.map((opt) => {
   
        const alreadyActive = activeInvestments.some(
          (a) => a.name === opt.name
        );

     
        const canAfford = balance >= opt.requiredAmount;

       
        const isThisOneProcessing = processingId === opt.id;


        const isAnythingProcessing = processingId !== null;

        return (
          <div
            key={opt.id}
            style={{
              border: "1px solid #ccc",
              padding: "1.5rem",
              borderRadius: "12px",
              backgroundColor: alreadyActive ? "#f9f9f9" : "#fff",
              boxShadow: "0 2px 4px rgba(0,0,0,0.05)",
            }}
          >
            <h4 style={{ margin: "0 0 10px 0", color: "#333" }}>{opt.name}</h4>

            <p style={{ margin: "5px 0" }}>
              <strong>Cost:</strong> ${opt.requiredAmount.toFixed(2)}
            </p>

            <p style={{ margin: "5px 0", color: "green" }}>
              <strong>Potential Return:</strong> $
              {opt.expectedReturn.toFixed(2)}
            </p>

            <p style={{ margin: "5px 0", fontSize: "0.9em", color: "#666" }}>
              <strong>Duration:</strong> {opt.durationSeconds}s
            </p>

            <button
              onClick={() => onInvest(opt.id)}
              disabled={alreadyActive || !canAfford || isAnythingProcessing}
              style={{
                marginTop: "15px",
                width: "100%",
                padding: "10px",
                cursor:
                  alreadyActive || !canAfford || isAnythingProcessing
                    ? "not-allowed"
                    : "pointer",
                backgroundColor: isThisOneProcessing
                  ? "#ffc107"
                  : alreadyActive
                  ? "#e0e0e0"
                  : "#007bff",
                color: alreadyActive ? "#666" : "#fff",
                border: "none",
                borderRadius: "6px",
                fontWeight: "bold",
                transition: "background-color 0.2s",
              }}
            >
              {isThisOneProcessing
                ? "Processing..."
                : alreadyActive
                ? "Already Active"
                : !canAfford
                ? "Insufficient Funds"
                : "Invest Now"}
            </button>
          </div>
        );
      })}
    </div>
  );
};
