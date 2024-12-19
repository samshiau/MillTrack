FROM node:alpine AS build 
# use non alpine version for less issues

WORKDIR /usr/src/app
# set working directory in container

COPY package.json package-lock.json ./

RUN npm install

#RUN npm install -g @angular/cli

COPY . .

RUN npm run build --prod

FROM nginx:alpine

WORKDIR /usr/share/nginx/html

RUN rm -rf ./*

COPY --from=build /usr/src/app/dist/mill-frontend/browser /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]



