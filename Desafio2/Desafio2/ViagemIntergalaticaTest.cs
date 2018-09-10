using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RA;

namespace Desafio2
{
    [TestClass]
    public class ViagemIntergalaticaTest
    {
        private string apiPlaneta = "https://swapi.co/api/planets/10";
        private string apiNave = "https://swapi.co/api/starships/12";
        /// <summary>
        /// 1° Bloco - Testes refetente a a missão de número 354.
        /// 2° Bloco -  Testes ferente a aeronave x-wing.
        /// </summary>

        [TestMethod]
        public void PlanetaExistenteKamino()
        {
            new RestAssured()
            .Given()
                .Name("Planeta")
                .When()
                    .Get(apiPlaneta)
                .Then()
                   .TestBody("NomePlaneta", x => x.name == "Kamino")
                   .Assert("NomePlaneta");
        }

        [TestMethod]
        public void ValidarDiametroKamino()
        {

            new RestAssured()
            .Given()
                .Name("Planeta")
                .When()
                    .Get(apiPlaneta)
                .Then()
                   .TestBody("Diametro", x => x.diameter <= 20000)
                   .Assert("Diametro");
        }

        [TestMethod]
        public void ValidarGravidadeKamino()
        {
            new RestAssured()
            .Given()
                .Name("Planeta")
                .When()
                    .Get(apiPlaneta)
                .Then()
                   .TestBody("Gravidade", x => x.gravity == "1 standard")
                   .Assert("Graviade");
        }


        [TestMethod]
        public void VerificarNaveExistenteNaLista()
        {
            new RestAssured()
            .Given()
                .Name("Nave")
                .When()
                    .Get(apiNave)
                .Then()
                   .TestBody("Nome", x => x.name == "X-wing")
                   .Assert("Nome");
        }

        [TestMethod]
        public void ValidarMaturidade()
        {
            new RestAssured()
            .Given()
                .Name("Nave")
                .When()
                    .Get(apiNave)
                .Then()
                .Debug()
                .TestBody("films", x => x.films.Count == 3)
                .Assert("films");
        }


    }
}
