# frontend/Dockerfile

# Build stage
FROM node:22-alpine AS builder
WORKDIR /app

RUN apk add --no-cache build-base python3

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build

# Production stage
FROM node:22-alpine
RUN npm install -g http-server

WORKDIR /app
COPY --from=builder /app/dist ./dist

EXPOSE 8080
CMD ["http-server", "dist"]