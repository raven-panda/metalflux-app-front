import axios from "axios";
import { API_BASE_URL } from "./api/constants/ApiUrls";

// Instance axios avec auth auto
export const api = axios.create({
  baseURL: API_BASE_URL,
});

// Intercept requests made with axios
// Get token from accessToken cookie if it's present, and put it into request headers as Bearer authentication
api.interceptors.request.use(async (config) => {
  config.withCredentials = true;
  return config;
});
