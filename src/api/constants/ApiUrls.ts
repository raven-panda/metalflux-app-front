export const API_BASE_URL = import.meta.env.VITE_API_URL;

const ApiUrls = {
  AUTH_LOGIN: `/auth/login`,

  UPLOAD_MEDIA: `/media/:id/upload`,
  GET_MEDIA_CHUNK_STREAM: `/media/:id/stream/:chunkId`,
};

export default ApiUrls;
