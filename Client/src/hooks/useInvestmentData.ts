import { useState, useEffect, useCallback } from "react";
import * as api from "../services/api";
import { subscribeToUserState } from "../services/signalr";
import type {
  ActiveInvestment,
  InvestmentOption,
  UserState,
} from "../types/investment.types";

export function useInvestmentData() {
  const [balance, setBalance] = useState(0);
  const [active, setActive] = useState<ActiveInvestment[]>([]);
  const [available, setAvailable] = useState<InvestmentOption[]>([]);
  const [lastBalanceUpdate, setLastBalanceUpdate] = useState<string | null>(
    null
  );

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const username = localStorage.getItem("investment_username") || "";

  const loadAllData = useCallback(async () => {
    if (!username) return;
    try {
      setLoading(true);
      const [options, state] = await Promise.all([
        api.getAvailableOptions(),
        api.getUserState(username),
      ]);

      setAvailable(options);
      setBalance(state.balance);
      setActive(state.activeInvestments);
      setLastBalanceUpdate(state.lastBalanceUpdate);
      setError(null);
    } catch (err: any) {
      setError(err.message || "Failed to load data");
    } finally {
      setLoading(false);
    }
  }, [username]);

  useEffect(() => {
    loadAllData();

    let activeCleanup: (() => void) | null = null;

    const setupRealtime = async () => {
      if (!username) return;

      const cleanup = await subscribeToUserState(
        username,
        (newData: UserState) => {
          setBalance(newData.balance);
          setActive(newData.activeInvestments);
          setLastBalanceUpdate(newData.lastBalanceUpdate);
        }
      );

      activeCleanup = cleanup;
    };

    setupRealtime();

    return () => {
      if (activeCleanup) {
        activeCleanup();
      }
    };
  }, [username, loadAllData]);

  return {
    balance,
    active,
    available,
    loading,
    error,

    lastBalanceUpdate,
    refresh: loadAllData,
  };
}
