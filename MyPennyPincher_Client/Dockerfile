# Step 1: Build the app
FROM node:slim AS builder

WORKDIR /app

# Copy only package files first to install deps (better caching)
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the app
COPY . .

# Expose the port Vite dev server runs on (default 5173)
EXPOSE 5173

# Start Vite dev server
CMD ["npm", "run", "dev"]