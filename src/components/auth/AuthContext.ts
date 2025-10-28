import { createContext } from "react";
import type { AuthState } from "./AuthProviders";

type AuthContextType = AuthState & {
  isAuthenticated: boolean;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
};

export const AuthContext = createContext<AuthContextType | undefined>(
  undefined,
);
