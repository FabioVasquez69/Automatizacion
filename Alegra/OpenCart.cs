using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FluentAssertions;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Alegra
{
    [TestClass]
    public class OpenCart
    {
        ChromeDriver driver;

        [TestMethod]
        public void BusquedaArticulo()
        {

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);

            string Categoria = "Phones & PDAs";
            string Producto = "iPhone";
            int Cantidad = 2;


            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);


            driver.Navigate().GoToUrl("http://opencart.abstracta.us/");


            driver.FindElement(By.XPath("//a[text()='"+ Categoria+"']")).Click();


            driver.FindElement(By.XPath("//a[text()='"+Producto+"']")).Click();

            driver.FindElement(By.XPath("//input[@name='quantity']")).Clear();
            driver.FindElement(By.XPath("//input[@name='quantity']")).SendKeys(""+Cantidad);

            driver.FindElement(By.XPath("//button[@id='button-cart']")).Click();

            string Confirmacion = driver.FindElement(By.XPath("//div[@class='alert alert-success alert-dismissible']")).Text;
            Confirmacion.Should().Contain(Producto);

            string Valor = driver.FindElement(By.XPath("//h2[contains(text(), '$')]")).Text;
            Valor = Valor.Replace("$", "").Replace(".",",");            
            decimal Valor2 = decimal.Parse(Valor);
            decimal TotalValor = Valor2 * Cantidad;
            // Console.WriteLine(Valor2*Cantidad);

            driver.FindElement(By.XPath("//span[@ id='cart-total']")).Click();
            string mostrarTotal = driver.FindElement(By.XPath("//strong[text()='Total']//following::td")).Text;
            mostrarTotal = mostrarTotal.Replace("$", "").Replace(".", ",");
            decimal mostrarTotal2 = decimal.Parse(mostrarTotal);
            mostrarTotal2.Should().Equals(TotalValor);
            mostrarTotal2.Should().Be(TotalValor);
        }


        [TestCleanup]
        public void Cerrar()
        {
            Thread.Sleep(10000);
            driver.Quit();
        }
    }
}
