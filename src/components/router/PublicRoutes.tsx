import { createRoute, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";
import MainHeader from "../layout/Header";
import { rootRoute } from "./Router";
import BrowsePage from "../../views/videos/BrowsePage";

const publicRouteRoot = createRoute({
  getParentRoute: () => rootRoute,
  id: "public",
  component: () => (
    <>
      <MainHeader />
      <Outlet />
      <TanStackRouterDevtools />
    </>
  ),
});

const browseRoute = createRoute({
  getParentRoute: () => publicRouteRoot,
  path: "/",
  component: BrowsePage,
});
const mediaRoute = createRoute({
  getParentRoute: () => publicRouteRoot,
  path: "/media/{id}",
  component: BrowsePage,
});

const publicRoute = publicRouteRoot.addChildren([browseRoute, mediaRoute]);
export default publicRoute;
