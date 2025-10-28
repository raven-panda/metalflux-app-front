import { RouterProvider } from "@tanstack/react-router";
import router from "./components/router/Router";

function App() {
  return <RouterProvider router={router} />;
}

export default App;
