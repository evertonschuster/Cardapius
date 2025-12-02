import { type Dispatch, type SetStateAction, useEffect, useState } from "react";

export function usePersistentState<T>(key: string, defaultValue: T): [T, Dispatch<SetStateAction<T>>] {
  const [value, setValue] = useState<T>(() => {
    if (typeof globalThis.window === "undefined") return defaultValue;

    const saved = localStorage.getItem(key);
    if (!saved) return defaultValue;

    try {
      return JSON.parse(saved) as T;
    } catch {
      localStorage.removeItem(key);
      return defaultValue;
    }
  });

  useEffect(() => {
    try {
      localStorage.setItem(key, JSON.stringify(value));
    } catch {
      // Ignore write errors (e.g., storage full) to avoid breaking rendering
    }
  }, [key, value]);

  return [value, setValue];
}
