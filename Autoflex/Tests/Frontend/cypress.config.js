const { defineConfig } = require("cypress");

module.exports = defineConfig({
    allowCypressEnv: false,

    e2e: {
        baseUrl: "https://localhost:7193",

        setupNodeEvents(on, config) {
        },
        specPattern: "cypress/e2e/**/*.spec.js", 
    },
});
