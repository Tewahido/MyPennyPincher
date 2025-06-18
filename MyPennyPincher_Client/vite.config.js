import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";

export default defineConfig({
  plugins: [react(), tailwindcss()],
  test: {
    globals: true,
    environment: "jsdom",
    css: true,
    server: {
      port: 0,
      host: 'localhost', 
    },
  },
  server: {
    host: "0.0.0.0",
    port: 5173,
  },
});
