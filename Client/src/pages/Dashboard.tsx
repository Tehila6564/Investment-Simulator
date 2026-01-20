import { useInvestmentData } from "../hooks/useInvestmentData";
import { useInvest } from "../hooks/useInvest";
import { ActiveInvestmentItem } from "../components/ActiveInvestmentItem";
import { AvailableInvestments } from "../components/AvailableInvestments"; // ייבוא הרכיב

export default function Dashboard() {
  const { balance, active, available, loading, error } = useInvestmentData();

  const { invest, processingId, lastError } = useInvest();

  if (loading) return <div>Loading data...</div>;
  if (error) return <div style={{ color: "red" }}>Error: {error}</div>;

  const username = localStorage.getItem("investment_username") || "User";

  return (
    <div style={{ padding: "20px" }}>
      <h1>Investment Dashboard - {username}</h1>

      <div
        style={{
          background: "#f4f4f4",
          padding: "15px",
          borderRadius: "8px",
          marginBottom: "20px",
        }}
      >
        <h2>Current Balance: ${balance.toFixed(2)}</h2>
      </div>

      {lastError && (
        <div style={{ color: "red", marginBottom: "10px" }}>⚠️ {lastError}</div>
      )}

      <section>
        <h3>Active Investments ({active.length})</h3>
        {active.length === 0 ? (
          <p>No active investments at the moment.</p>
        ) : (
          <ul style={{ listStyle: "none", padding: 0 }}>
            {active.map((inv) => (
              <ActiveInvestmentItem key={inv.id} inv={inv} />
            ))}
          </ul>
        )}
      </section>

      <hr style={{ margin: "30px 0" }} />

      <section>
        <h3>Available Opportunities</h3>

        <AvailableInvestments
          options={available}
          activeInvestments={active}
          balance={balance}
          processingId={processingId}
          onInvest={invest}
        />
      </section>
    </div>
  );
}
