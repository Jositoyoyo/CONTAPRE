const { test, expect } = require('@playwright/test');

test('login and password recovery pages navigate without submitting data', async ({ page }) => {
  await page.goto('/Views/Account/Login.aspx');

  await expect(page).toHaveTitle('Contabilidad Presupuestaria');
  await expect(page.locator('#form1')).toBeVisible();
  await expect(page.getByRole('button', { name: 'Entrar' })).toBeVisible();

  await page.getByRole('link', { name: /Olvidaste tu contraseña/ }).click();

  await expect(page).toHaveURL(/\/Views\/Account\/ForgotPassword\.aspx$/);
  await expect(page.locator('#formForgotPassword')).toBeVisible();
  await expect(page.getByText(/introduce tu nombre de usuario/i)).toBeVisible();

  await page.getByRole('link', { name: 'Volver' }).click();

  await expect(page).toHaveURL(/\/Views\/Account\/Login\.aspx$/);
  await expect(page.locator('#form1')).toBeVisible();
});
