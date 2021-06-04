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
    public class UnitTest1
    {
        ChromeDriver driver;

        [TestMethod]
        public void InicioSeccion()
        {

            string correo = "fabio.vaz93@gmail.com";
            string contrasena = "Hector123";

            string NombreCategoria = "Prueba005";
            string DescripcionCategoria = "Datos de categoria pruebas 005";


            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
            //
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);


            driver.Navigate().GoToUrl("https://app.alegra.com/user/login");

            driver.FindElement(By.XPath("//input[@placeholder='Correo']")).SendKeys(correo);
            driver.FindElement(By.XPath("//input[@placeholder='Contraseña']")).SendKeys(contrasena);
            driver.FindElement(By.XPath("//input[@value='INGRESAR']")).Click();

            driver.FindElement(By.XPath("//span[text()='Inventario']")).Click();
            driver.FindElement(By.XPath("//a[text()='Categorías']")).Click();

            driver.FindElement(By.XPath("//span[text()='Nueva categoría']")).Click();


            driver.FindElement(By.XPath("//span[text()='Nombre']//following::input")).SendKeys(NombreCategoria);
            driver.FindElement(By.XPath("//span[text()='Descripción']//following::textarea")).SendKeys(DescripcionCategoria);
            driver.FindElement(By.XPath("//div[not(@class='footer-content')]/child::button/span[text()='Guardar']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(16));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[text()=' Ítems asociados ']")));

            driver.FindElement(By.XPath("//a[text()='Categorías']")).Click();


            driver.FindElement(By.XPath("//span[text()= ' Filtrar ']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder ='Nombre']")).SendKeys(NombreCategoria);
            driver.FindElement(By.XPath("//span[text()= ' Filtrar ']")).Click();
            bool mostrardo = driver.FindElement(By.XPath("//span[contains( text(), '" + NombreCategoria + "')]")).Displayed;
            mostrardo.Should().BeTrue();

        }

        [TestCleanup]
        public void Cerrar()
        {
            Thread.Sleep(10000);
            driver.Quit();
        }
    }
}
