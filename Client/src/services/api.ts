import type { InvestmentOption, UserState } from "../types/investment.types";

const API_BASE = "https://localhost:7049";

export async function getAvailableOptions(): Promise<InvestmentOption[]> {
  const res = await fetch(`${API_BASE}/api/options`);
  if (!res.ok) throw new Error("Failed to fetch options");
  return res.json();
}

export async function getUserState(username: string): Promise<UserState> {
  const res = await fetch(
    `${API_BASE}/api/state?username=${encodeURIComponent(username)}`
  );
  if (!res.ok) throw new Error("Failed to fetch user state");
  return res.json();
}

export async function invest(
  username: string,
  optionId: string
): Promise<void> {
  const res = await fetch(`${API_BASE}/api/invest`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, optionId }),
  });
  if (!res.ok) {
    const errorData = await res.json().catch(() => ({}));
    throw new Error(errorData.error || "Failed to invest");
  }
}
