
# Stage 1: Build the Angular application
FROM node as build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

# Stage 2: Serve the application with Nginx
FROM nginx:alpine
COPY --from=build /app/dist/buero.frontend /usr/share/nginx/html
COPY proxyconf /etc/nginx/conf.d/default.conf
