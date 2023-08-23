using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

namespace DerivcoAssessment.Tests
{
    public class Tests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {

            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Manage().Window.Maximize();

        }

        [Test]       
        public void SearchCocktail()
        {
            driver.Url = "https://www.thecocktaildb.com/";
            var searchBox = driver.FindElement(By.CssSelector("input[placeholder='Search for a Cocktail...']"));
            searchBox.SendKeys("Cocktail");
            var searchButton = driver.FindElement(By.XPath("//*[@id=\"feature\"]/div[1]/div/table[2]/tbody/tr/td/form/div/div/button"));
            searchButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            GetCocktail();            
        }

        public void GetCocktail()
        {
            var getCocktail = driver.FindElement(By.LinkText("Hawaiian Cocktail"));
            getCocktail.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            BrowseByName();
        }

        public void BrowseByName()
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0,500)", "");
                IList<IWebElement> namesByCocktail = driver.FindElements(By.XPath("//*[@id=\"feature\"]/div/div/div/h2[2]/a"));
               
                for (int i = 0; i < namesByCocktail.Count; i++)
                {

                    Actions action = new Actions(driver);
                    action.KeyDown(Keys.Control).MoveToElement(namesByCocktail[i]).Click().Perform();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

                }
                Console.WriteLine("list of the BrowserNames : "+namesByCocktail.Count);
            }
            catch (NoSuchElementException ex)
            {
                throw ex;
            }
            NoCocktail();
        }
        public void NoCocktail()
        {
          
            var searchBox = driver.FindElement(By.ClassName("search-form"));
            searchBox.SendKeys("@1231");
            searchBox.Click();
            String text = driver.FindElement(By.XPath("//*[@id=\"feature\"]/div/div/div[1]")).Text;
            Console.WriteLine("No Cocktails :"+text);

            var homePage = driver.FindElement(By.XPath("//*[@id=\"header\"]/nav/div/div[2]/ul/li[1]/a"));
            homePage.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

         }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}