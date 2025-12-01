import { act, renderHook } from "@testing-library/react";
import { usePersistentState } from "../usePersistentState";

describe("usePersistentState", () => {
  beforeEach(() => {
    localStorage.clear();
  });

  it("returns the default value and persists it when no storage value exists", () => {
    const { result } = renderHook(() => usePersistentState("test-key", "default"));

    expect(result.current[0]).toBe("default");
    expect(localStorage.getItem("test-key")).toBe("\"default\"");
  });

  it("restores persisted state from localStorage", () => {
    localStorage.setItem("test-key", JSON.stringify({ enabled: true }));

    const { result } = renderHook(() => usePersistentState("test-key", { enabled: false }));

    expect(result.current[0]).toEqual({ enabled: true });
  });

  it("falls back to the default value and clears invalid persisted data", () => {
    localStorage.setItem("test-key", "not-json");

    const { result } = renderHook(() => usePersistentState("test-key", 5));

    expect(result.current[0]).toBe(5);
    expect(localStorage.getItem("test-key")).toBeNull();
  });

  it("saves updates to localStorage when the setter is called", () => {
    const { result } = renderHook(() => usePersistentState("test-key", 1));

    act(() => {
      result.current[1](2);
    });

    expect(localStorage.getItem("test-key")).toBe("2");
  });
});
