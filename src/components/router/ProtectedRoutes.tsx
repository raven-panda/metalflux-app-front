import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";
import { RequireAuthentication } from "../auth/AuthProviders";
import MainHeader from "../layout/Header";
import { rootRoute } from "./Router";
import { createRoute, Outlet } from "@tanstack/react-router";

const protectedRouteRoot = createRoute({
  getParentRoute: () => rootRoute,
  id: "protected",
  component: () => (
    <>
      <RequireAuthentication>
        <MainHeader />
        <Outlet />
      </RequireAuthentication>
      <TanStackRouterDevtools />
    </>
  ),
});

const protectedRoute = protectedRouteRoot.addChildren([]);
export default protectedRoute;
