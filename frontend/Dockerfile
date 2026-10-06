
# Stage 1: Build the Angular application
FROM node AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

# Stage 2: Serve the application with Nginx
FROM nginx:alpine AS final

ARG BUILD_DATE="1970-01-01T00:00:00Z"
ARG VERSION="latest"
ARG VCS_REF="unknown"

LABEL org.opencontainers.image.title="regatta-frontend" \
      org.opencontainers.image.description="Regatta frontend application" \
      org.opencontainers.image.url="https://github.com/Sanifant/regatta-frontend" \
      org.opencontainers.image.source="https://github.com/Sanifant/regatta-frontend" \
      org.opencontainers.image.version="${VERSION}" \
      org.opencontainers.image.created="${BUILD_DATE}" \
      org.opencontainers.image.revision="${VCS_REF}" \
      org.opencontainers.image.vendor="Sanifant" \
      org.opencontainers.image.licenses="MIT"

COPY --from=build /app/dist/regatta-frontend /usr/share/nginx/html
COPY config/proxyconf /etc/nginx/conf.d/default.conf
