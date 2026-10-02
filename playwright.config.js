const { defineConfig } = require('@playwright/test');

const externalBaseURL = process.env.PLAYWRIGHT_BASE_URL;
const baseURL = (externalBaseURL || 'http://localhost:54234').replace(/\/+$/, '');

const config = {
  testDir: './tests/integration',
  fullyParallel: true,
  reporter: 'list',
  use: {
    baseURL,
    browserName: 'chromium',
    trace: 'retain-on-failure',
  },
};

if (!externalBaseURL) {
  config.webServer = {
    command: 'powershell -NoProfile -ExecutionPolicy Bypass -File Support\\Utils\\StartLocal.ps1',
    url: `${baseURL}/Views/Account/Login.aspx`,
    reuseExistingServer: !process.env.CI,
    timeout: 180000,
  };
}

module.exports = defineConfig(config);
