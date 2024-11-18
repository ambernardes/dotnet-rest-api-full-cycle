# Stage 1: Build the Vue application
FROM node:14 AS build

WORKDIR /app

# Copy package.json and package-lock.json (if available) to Docker environment
COPY package*.json ./

# Install npm dependencies
RUN npm install

# Copy the project files into the Docker image
COPY . .

# Build the Vue application
RUN npm run build

# Stage 2: Serve the built Vue application with Nginx
FROM nginx:alpine

# Copy the built Vue static files from the build stage to the Nginx public directory
COPY --from=build /app/dist /usr/share/nginx/html

# Copy custom Nginx configuration if needed
# COPY nginx.conf /etc/nginx/nginx.conf

# Expose port 80 to the Docker host, so you can access it from the outside
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]
