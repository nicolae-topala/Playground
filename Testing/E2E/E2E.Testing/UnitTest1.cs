using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Text.RegularExpressions;

namespace E2E.Testing
{
    public class Tests : PageTest

    {
        [Test]
        public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
        {
            await Page.GotoAsync("https://playwright.dev");

            // Expect a title "to contain" a substring.
            await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

            // create a locator
            var getStarted = Page.GetByRole(AriaRole.Link, new() { Name = "Get started" });

            // Expect an attribute "to be strictly equal" to the value.
            await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

            // Click the get started link.
            await getStarted.ClickAsync();

            // Expects page to have a heading with the name of Installation.
            await Expect(Page
                .GetByRole(AriaRole.Heading, new() { Name = "Installation" }))
                .ToBeVisibleAsync();
        }

        [Test]
        public async Task TestFunctionalityUsingCodegen()
        {
            using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 1000,
            });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://demo.playwright.dev/todomvc/#/");

            await page.GetByPlaceholder("What needs to be done?").ClickAsync();

            await page.GetByPlaceholder("What needs to be done?").FillAsync("create test");

            await page.GetByPlaceholder("What needs to be done?").PressAsync("Enter");

            await Expect(page.GetByTestId("todo-title")).ToContainTextAsync("create test");

            await Expect(page.GetByLabel("Toggle Todo")).Not.ToBeCheckedAsync();

            await page.GetByPlaceholder("What needs to be done?").ClickAsync();

            await page.GetByPlaceholder("What needs to be done?").FillAsync("run test");

            await page.GetByPlaceholder("What needs to be done?").PressAsync("Enter");

            await Expect(page.Locator("body")).ToContainTextAsync("run test");

            await Expect(page.Locator("li").Filter(new() { HasText = "run test" }).GetByLabel("Toggle Todo")).Not.ToBeCheckedAsync();

            await page.GetByPlaceholder("What needs to be done?").ClickAsync();

            await page.GetByPlaceholder("What needs to be done?").FillAsync("finish test");

            await page.GetByPlaceholder("What needs to be done?").PressAsync("Enter");

            await Expect(page.Locator("body")).ToContainTextAsync("finish test");

            await Expect(page.Locator("li").Filter(new() { HasText = "finish test" }).GetByLabel("Toggle Todo")).Not.ToBeCheckedAsync();

            await page.Locator("li").Filter(new() { HasText = "create test" }).GetByLabel("Toggle Todo").CheckAsync();

            await Expect(page.Locator("li").Filter(new() { HasText = "create test" }).GetByLabel("Toggle Todo")).ToBeCheckedAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Completed" }).ClickAsync();

            await Expect(page.GetByLabel("Toggle Todo")).ToBeCheckedAsync();

            await Expect(page.GetByTestId("todo-title")).ToContainTextAsync("create test");

            await page.GetByRole(AriaRole.Link, new() { Name = "All" }).ClickAsync();

            await page.Locator("li").Filter(new() { HasText = "run test" }).GetByLabel("Toggle Todo").CheckAsync();

            await Expect(page.Locator("li").Filter(new() { HasText = "run test" }).GetByLabel("Toggle Todo")).ToBeCheckedAsync();

            await page.Locator("li").Filter(new() { HasText = "finish test" }).GetByLabel("Toggle Todo").CheckAsync();

            await Expect(page.Locator("li").Filter(new() { HasText = "finish test" }).GetByLabel("Toggle Todo")).ToBeCheckedAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Clear completed" }).ClickAsync();

        }
    }
}