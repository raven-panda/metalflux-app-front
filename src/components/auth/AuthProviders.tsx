import { useEffect, useMemo, useState, type ReactNode } from "react";
import LoaderIndicator from "../layout/LoaderIndicator";
import { Navigate } from "@tanstack/react-router";
import { AuthContext } from "./AuthContext";
import { useAuth } from "./useAuth";
import { loginApi } from "../../api/auth/AuthService";

export type AuthState = {
  loading: boolean;
};

export function RequireAuthentication({ children }: { children: ReactNode }) {
  const { loading, isAuthenticated, logout } = useAuth();

  useEffect(() => {
    if (!loading && !isAuthenticated) {
      logout();
    }
  }, [loading, isAuthenticated, logout]);

  if (loading) {
    return <LoaderIndicator />;
  } else if (!isAuthenticated) {
    return <Navigate to="/sign-in" />;
  }

  return children;
}

export function AuthProvider({ children }: { children: ReactNode }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  const login = async (email: string, password: string) => {
    try {
      setIsLoading(true);
      await loginApi(email, password);
      setIsAuthenticated(true);
    } catch (e) {
      console.error(e);
    } finally {
      setIsLoading(false);
    }
  };

  const logout = () => {
    window.cookieStore.delete("access_token");
  };

  const value = useMemo(
    () => ({
      loading: isLoading,
      isAuthenticated: isAuthenticated,
      login,
      logout,
    }),
    [isAuthenticated, isLoading],
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
