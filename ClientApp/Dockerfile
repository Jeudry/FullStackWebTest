FROM node:20.13.1-alpine as angular


WORKDIR /app

COPY . .

RUN npm i --force
RUN npm run build

FROM nginx:1.19.6-alpine

WORKDIR /usr/share/nginx/html
COPY --from=angular /app/dist/client-app .
