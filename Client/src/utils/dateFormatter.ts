export const formatLastUpdate = (
  dateString: string | null | undefined
): string => {
  if (!dateString) return "Never";

  const date = new Date(dateString);

  return new Intl.DateTimeFormat("he-IL", {
    day: "2-digit",
    month: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
    hour12: false,
  }).format(date);
};
