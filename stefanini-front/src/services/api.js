import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:6450/",
});

export default api;
