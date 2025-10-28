import { createRoute, Outlet } from "@tanstack/react-router";
import SignInPage from "../../views/auth/SignInPage";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";
import { rootRoute } from "./Router";

const authRouteRoot = createRoute({
  getParentRoute: () => rootRoute,
  id: "auth",
  component: () => (
    <>
      <Outlet />
      <TanStackRouterDevtools />
    </>
  ),
});

const signInRoute = createRoute({
  getParentRoute: () => authRouteRoot,
  path: "/sign-in",
  component: SignInPage,
});

const authRoute = authRouteRoot.addChildren([signInRoute]);
export default authRoute;
