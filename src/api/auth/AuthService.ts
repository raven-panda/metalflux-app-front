import { api } from "../../queryClient";
import ApiUrls from "../constants/ApiUrls";

export async function loginApi(email: string, password: string) {
  const response = await api.post(ApiUrls.AUTH_LOGIN, {
    email,
    password,
  });
  return response.data;
}
