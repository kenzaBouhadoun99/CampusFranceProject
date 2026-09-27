using System;
using Reqnroll;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace CampusFranceProject.StepDefinitions
{
    [Binding]
    public class CreationDunCompteCampusFranceStepDefinitions
    {
        private IWebDriver driver;

        // ============================================================
        // AVANT / APRÈS SCÉNARIO
        // ============================================================

        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
            driver.Dispose();
        }

        // ============================================================
        // BACKGROUND
        // ============================================================

        [Given("je suis sur la page Créer un nouveau compte {string} de Campus France")]
        public void GivenJeSuisSurLaPageCreerUnNouveauCompteDeCampusFrance(string url)
        {
            driver.Navigate().GoToUrl(url);
        }


        // ============================================================
        // CHERCHEUR
        // ============================================================

        [When("je renseigne les informations personnelles du chercheur {string}, {string}, {string}")]
        public void WhenJeRenseigneLesInformationsPersonnellesDuChercheur(string civilite,string nom,string prenom)
        {
            var form = driver.FindElement(By.Id("user-form"));

            // Civilité
            if (civilite.Equals("Madame", StringComparison.OrdinalIgnoreCase))
            {
                var madame = form.FindElement(By.Id("edit-field-civilite-mme"));

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", madame);
            }
            else if (civilite.Equals("Monsieur", StringComparison.OrdinalIgnoreCase))
            {
                var monsieur = form.FindElement(By.Id("edit-field-civilite-mr"));

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", monsieur);
            }

            // Nom
            form.FindElement(By.Id("edit-field-nom-0-value")).SendKeys(nom);

            // Prénom
            form.FindElement(By.Id("edit-field-prenom-0-value")).SendKeys(prenom);
        }


        [When("je renseigne les informations de résidence du chercheur {string}, {string}")]
        public void WhenJeRenseigneLesInformationsDeResidenceDuChercheur(string paysResidence,string nationalite)
        {
            var form = driver.FindElement(By.Id("user-form"));

            // Pays de résidence
            var pays = form.FindElement(By.Id("edit-field-pays-concernes-selectized"));

            pays.Click();
            pays.SendKeys(paysResidence);
            pays.SendKeys(Keys.Enter);

            // Nationalité
            var nationaliteInput = form.FindElement(By.Id("edit-field-nationalite-0-target-id"));

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", nationaliteInput);

            nationaliteInput.SendKeys(nationalite);
            nationaliteInput.SendKeys(Keys.ArrowDown);
            nationaliteInput.SendKeys(Keys.Enter);
        }


        [When("je renseigne les coordonnées du chercheur {string}, {string}, {string}")]
        public void WhenJeRenseigneLesCoordonneesDuChercheur(string codePostal,string ville,string telephone)
        {
            var form = driver.FindElement(By.Id("user-form"));

            // Code postal
            var codePostalInput = form.FindElement(By.Id("edit-field-code-postal-0-value"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                codePostalInput,
                codePostal);

            // Ville
            form.FindElement(By.Id("edit-field-ville-0-value")).SendKeys(ville);

            // Téléphone
            form.FindElement(By.Id("edit-field-telephone-0-value")).SendKeys(telephone);
        }


        [When("je renseigne les informations de connexion du chercheur {string}, {string}, {string}")]
        public void WhenJeRenseigneLesInformationsDeConnexionDuChercheur(string email,string motDePasse,string confirmationMotDePasse)
        {
            var form = driver.FindElement(By.Id("user-form"));

            // Email
            var emailInput = form.FindElement(By.CssSelector("input[name='name']"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                emailInput,
                email);

            // Mot de passe
            var passwordInput = form.FindElement(By.Id("edit-pass-pass1"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                passwordInput,
                motDePasse);

            // Confirmation du mot de passe
            var confirmationInput = form.FindElement(By.Id("edit-pass-pass2"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                confirmationInput,
                confirmationMotDePasse);
        }


        [When("je sélectionne le profil de chercheur {string}")]
        public void WhenJeSelectionneLeProfilDeChercheur(string profil)
        {
            var form = driver.FindElement(By.Id("user-form"));

            if (profil.Equals("Chercheur", StringComparison.OrdinalIgnoreCase))
            {
                var chercheur = form.FindElement(By.Id("edit-field-publics-cibles-3"));

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", chercheur);
            }
        }


        [When("je renseigne les informations du chercheur {string}, {string}")]
        public void WhenJeRenseigneLesInformationsDuChercheur(string domaine,string niveauEtudes)
        {
            var form = driver.FindElement(By.Id("user-form"));

            // Domaine d'études
            SelectizeByText(form,"edit-field-domaine-etudes-selectized",domaine);

            // Niveau d'études
            SelectizeByText(form,"edit-field-niveaux-etude-selectized",niveauEtudes);
        }


        [Then("le nom renseigné est {string} pour chercheur")]
        public void ThenLeNomRenseigneEstPourChercheur(string nomAttendu)
        {
            var form = driver.FindElement(By.Id("user-form"));

            var nom = form.FindElement(By.Id("edit-field-nom-0-value")).GetAttribute("value");

            Assert.That(nom, Is.EqualTo(nomAttendu));
        }


        // ============================================================
        // INSTITUTIONNEL
        // ============================================================

        [When("je renseigne les informations personnelles de institutionnel {string}, {string}, {string}")]
        public void WhenJeRenseigneLesInformationsPersonnellesDeInstitutionnel(string civilite,string nom,string prenom)
        {
            var form = driver.FindElement(By.Id("user-form"));

            // Civilité
            if (civilite.Equals("Madame", StringComparison.OrdinalIgnoreCase))
            {
                var madame = form.FindElement(By.Id("edit-field-civilite-mme"));

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", madame);
            }
            else if (civilite.Equals("Monsieur", StringComparison.OrdinalIgnoreCase))
            {
                var monsieur = form.FindElement(By.Id("edit-field-civilite-mr"));

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", monsieur);
            }

            // Nom
            form.FindElement(By.Id("edit-field-nom-0-value")).SendKeys(nom);

            // Prénom
            form.FindElement(By.Id("edit-field-prenom-0-value")).SendKeys(prenom);
        }


        [When("je renseigne les informations de résidence de institutionnel {string}, {string}")]
        public void WhenJeRenseigneLesInformationsDeResidenceDeInstitutionnel(string paysResidence,string nationalite)
        {
            var form = driver.FindElement(By.Id("user-form"));

            // Pays de résidence
            var pays = form.FindElement(By.Id("edit-field-pays-concernes-selectized"));

            pays.Click();
            pays.SendKeys(paysResidence);
            pays.SendKeys(Keys.Enter);

            // Nationalité
            var nationaliteInput = form.FindElement(By.Id("edit-field-nationalite-0-target-id"));

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", nationaliteInput);

            nationaliteInput.SendKeys(nationalite);
            nationaliteInput.SendKeys(Keys.ArrowDown);
            nationaliteInput.SendKeys(Keys.Enter);
        }


        [When("je renseigne les coordonnées de institutionnel {string}, {string}, {string}")]
        public void WhenJeRenseigneLesCoordonneesDeInstitutionnel(string codePostal,string ville,string telephone)
        {
            var form = driver.FindElement(By.Id("user-form"));

            // Code postal
            var codePostalInput = form.FindElement(By.Id("edit-field-code-postal-0-value"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                codePostalInput,
                codePostal);

            // Ville
            var villeInput = form.FindElement(By.Id("edit-field-ville-0-value"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                villeInput,
                ville);

            // Téléphone
            var telephoneInput = form.FindElement(By.Id("edit-field-telephone-0-value"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                telephoneInput,
                telephone);
        }


        [When("je renseigne les informations de connexion de institutionnel {string}, {string}, {string}")]
        public void WhenJeRenseigneLesInformationsDeConnexionDeInstitutionnel(string email,string motDePasse,string confirmationMotDePasse)
        {
            var form = driver.FindElement(By.Id("user-form"));

            // Email
            var emailInput = form.FindElement(By.CssSelector("input[name='name']"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                emailInput,
                email);

            // Mot de passe
            var passwordInput = form.FindElement(By.Id("edit-pass-pass1"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                passwordInput,
                motDePasse);

            // Confirmation du mot de passe
            var confirmationInput = form.FindElement(By.Id("edit-pass-pass2"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                confirmationInput,
                confirmationMotDePasse);
        }


        [When("je sélectionne le profil institutionnel {string}")]
        public void WhenJeSelectionneLeProfilInstitutionnel(string profil)
        {
            var form = driver.FindElement(By.Id("user-form"));

            if (profil.Equals("Institutionnel", StringComparison.OrdinalIgnoreCase))
            {
                var institutionnel = form.FindElement(By.Id("edit-field-publics-cibles-4"));

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", institutionnel);
            }
        }


        [When("je renseigne les informations de l'institutionnel {string}, {string}, {string}")]
        public void WhenJeRenseigneLesInformationsDeLinstitutionnel(
            string fonction,
            string typeOrganisme,
            string nomOrganisme)
        {
            var form = driver.FindElement(By.Id("user-form"));

            // Fonction
            var fonctionInput = form.FindElement(By.Id("edit-field-fonction-0-value"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                fonctionInput,
                fonction);

            // Type organisme
            SelectizeByText(form,"edit-field-type-organisme-selectized",typeOrganisme);

            // Nom organisme
            var nomOrganismeInput = form.FindElement(By.Id("edit-field-nom-organisme-0-value"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                nomOrganismeInput,
                nomOrganisme);
        }


            [Then("le nom renseigné est {string} pour institutionnel")]
        public void ThenLeNomRenseigneEstPourInstitutionnel(string nomAttendu)
        {
            var form = driver.FindElement(By.Id("user-form"));

            var nom = form.FindElement(By.Id("edit-field-nom-0-value")).GetAttribute("value");

            Assert.That(nom, Is.EqualTo(nomAttendu));
        }


        // ============================================================
        // MÉTHODE POUR LES SELECTIZE
        // ============================================================

        private void SelectizeByText(IWebElement form,string inputId,string valeur)
        {
            var input = form.FindElement(By.Id(inputId));

            // Donner le focus au champ
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].focus();", input);

            // Saisir la valeur
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));",
                input,
                valeur);

            System.Threading.Thread.Sleep(1000);

            var wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10)); // temps d'attente 1min

            var option = wait.Until(d =>
            {
                try
                {
                    return d.FindElement(
                        By.XPath(
                            $"//*[contains(@class,'selectize-dropdown-content')]" +
                            $"//*[contains(@class,'option') and normalize-space(.)=\"{valeur}\"]"));
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });

            // Sélection de l'option
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", option);
        }
    }
}