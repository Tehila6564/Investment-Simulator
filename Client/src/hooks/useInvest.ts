import { useState } from "react";
import * as api from "../services/api";

export function useInvest() {
  const [processingId, setProcessingId] = useState<string | null>(null);
  const [lastError, setLastError] = useState<string | null>(null);

  const invest = async (optionId: string) => {
    const username = localStorage.getItem("investment_username");
    if (!username) {
      setLastError("No username found");
      return;
    }

    setProcessingId(optionId);
    setLastError(null);

    try {
      await api.invest(username, optionId);
    } catch (err: any) {
      setLastError(err.message || "The investment failed");
    } finally {
      setProcessingId(null);
    }
  };

  return {
    invest,
    isProcessing: processingId !== null,
    processingId,
    lastError,
  };
}
