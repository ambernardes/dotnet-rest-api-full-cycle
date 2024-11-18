# Equipment Management App

# Description:

A simple web application for managing a list of equipment, demonstrating skills in .NET Framework, C#, ASP.NET Core, and Vue.

# Technologies Used:

To create this application, the following technologies were used:

- [Node](https://nodejs.org/)
- [Vuetify](https://vuetifyjs.com/en/)
- [Vuejs](https://vuejs.org/)
- [Vite](https://vitejs.dev/)

# Setup Instructions

The version of `Node` used to create the project is `20.3.1`.

If Node is properly installed and configured, it is possible to build and test the application by running the `npm` script. To do this, run the commands in the root directory:

### Project Setup

```sh
npm install
```

### Compile and Hot-Reload for Development

```sh
npm run dev
```

### Run Unit Tests with [Vitest](https://vitest.dev/)

```sh
npm run test:unit
```

The application uses Vuejs. And it was developed using the version 3.

## Recommended IDE Setup

[VSCode](https://code.visualstudio.com/) + [Volar](https://marketplace.visualstudio.com/items?itemName=Vue.volar) (and disable Vetur).

## Type Support for `.vue` Imports in TS

TypeScript cannot handle type information for `.vue` imports by default, so we replace the `tsc` CLI with `vue-tsc` for type checking. In editors, we need [Volar](https://marketplace.visualstudio.com/items?itemName=Vue.volar) to make the TypeScript language service aware of `.vue` types.

## Customize configuration

See [Vite Configuration Reference](https://vitejs.dev/config/).

# Implementation

The approach used to create this application consists of generating a template application using `vuejs 3` and then implementing CRUD actions using `typescript` and `axios` to carry out the requests.

In order to not spend time creating the screen, `Vuetify` components were used.

The test cases were implemented using `vitest`.

# Endpoints

The client will run in [localhost:5173](http://localhost:5173/).
