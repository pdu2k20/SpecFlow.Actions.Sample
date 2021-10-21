using OpenQA.Selenium;

namespace Example.PageObjects
{
    public class CalculatorElementLocators
    {
        //The URL of the calculator to be opened in the browser
        private protected const string CalculatorUrl = "https://specflowoss.github.io/Calculator-Demo/Calculator.html";
        //private protected const string CalculatorUrl = @"C:/Users/tbolt/src3/SpecFlow.Actions.Sample/Web/Calculator%20Demo.html";

        //Finding elements by ID
        private protected By FirstNumberFieldLocator => By.Id("first-number");
        private protected By SecondNumberFieldLocator => By.Id("second-number");
        private protected By AddButtonLocator => By.Id("add-button");
        private protected By ResultLabelLocator => By.Id("result");
        private protected By ResetButtonLocator => By.Id("reset-button");
    }
}