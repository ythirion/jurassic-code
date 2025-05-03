import { test, expect } from '@playwright/test';

test.describe('Jurassic Park UI Smoke Tests', () => {
  test('should load the dashboard page', async ({ page }) => {
    await page.goto('/');
    
    await expect(page.locator('h1')).toContainText('Park Dashboard');
    await expect(page).toHaveTitle(/Jurassic Park/);
  });

  test('should navigate between main pages', async ({ page }) => {
    await page.goto('/');
    
    // Navigation menu should be visible
    const navigation = page.locator('nav');
    await expect(navigation).toBeVisible();
    
    // Navigate to Zones page
    await page.getByRole('link', { name: 'Zones' }).click();
    await expect(page.locator('h1')).toContainText('Zones');
    
    // Navigate to Dinosaurs page
    await page.getByRole('link', { name: 'Dinosaurs' }).click();
    await expect(page.locator('h1')).toContainText('Dinosaurs');
    
    // Navigate to Compatibility page
    await page.getByRole('link', { name: 'Compatibility' }).click();
    await expect(page.locator('h1')).toContainText('Compatibility');
    
    // Navigate back to Dashboard
    await page.getByRole('link', { name: 'Dashboard' }).click();
    await expect(page.locator('h1')).toContainText('Park Dashboard');
  });
  
  test('should display core UI components', async ({ page }) => {
    await page.goto('/zones');
    
    // Check if add zone form exists
    const addZoneForm = page.locator('form').filter({ hasText: 'Add Zone' });
    await expect(addZoneForm).toBeVisible();
    
    // Go to dinosaurs page and check if add dinosaur form exists
    await page.goto('/dinosaurs');
    const addDinoForm = page.locator('form').filter({ hasText: 'Add Dinosaur' });
    await expect(addDinoForm).toBeVisible();
    
    // Check if move dinosaur form exists
    const moveDinoForm = page.locator('form').filter({ hasText: 'Move Dinosaur' });
    await expect(moveDinoForm).toBeVisible();
  });
});