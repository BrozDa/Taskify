FROM node:24-bullseye AS builder
COPY package.json package-lock.json ./
RUN npm ci
COPY . . 
RUN npm run build

FROM nginx:1.29.1
EXPOSE 80
COPY --from=builder /dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf