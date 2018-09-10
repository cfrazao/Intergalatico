using Microsoft.VisualStudio.TestTools.UnitTesting;
using RA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Desafio2
{
    /// <summary>
    /// 1° Bloco - Testes com secret key
    /// 2° Bloco - Testes sem secret key
    /// </summary>

    [TestClass]
    public class BinTest
    {
        private const string CHAVEACESSO = "$2a$10$V5XhRYwe.eHm6kpAX7LKruzKUV9jH9Tf67DOUE3TCpYNEoTHN09Pu";
        private string privado = "true";
        private static object uid;

        [TestMethod,Priority(1)]
        public void CriarBin()
        {
            String json =
                     "{\"sample\": \"Desafio\", " +
                     " \"name\": \"TIE Advanced Checkout\"," +
                     "\"model\": \"Twin Ion Engine Advanced Checkout\"," +
                     " \"manufacturer\": \"Sienar Fleet Systems\"," +
                     "\"cost_in_credits\": \"unknown\"," +
                     "\"length\": \"9.8\"," +
                     "\"max_atmosphering_speed\": \"1400\"," +
                     "\"crew\": \"4\"," +
                     "\"passengers\": \"0\"," +
                     "\"cargo_capacity\": \"300\"," +
                     "\"consumables\": \"13 days\"," +
                     "\"hyperdrive_rating\": \"1.9\"," +
                     "\"MGLT\": \"111\"," +
                     "\"starship_class\": \"Starfighter\"," +
                     "\"pilots\": \"[]\"," +
                     "\"films\": \"[]\"," +
                     "\"created\": \"2014-12-12T11:21:32.991000Z\"," +
                     "\"edited\": \"2014-12-22T17:35:44.549047Z\"}";
            uid = new RestAssured()

          
            .Given()
                   .Name("Com chave de acesso")
                   .Header("Content-Type", "application/json")
                   .Header("secret-key", CHAVEACESSO)
                   .Header("private", privado)
                   .Body(json)

                .When()
                     .Post("https://api.jsonbin.io/b")
                .Then()
                     .Debug()
                     .TestStatus("status", x => x == 200)
                     .Assert("status")
                     .Retrieve(a => a.id);

                
        }

        [TestMethod,Priority(2)]
        public void ConsultarBinKey()
        {
            new RestAssured()
                .Given()
                    .Name("Com chave de acesso")
                    .Header("secret-key", CHAVEACESSO)
                .When()
                    .Get($"https://api.jsonbin.io/b/{uid}")
                .Then()
                   .TestStatus("status", x => x == 200)
                   .Assert("status");
        }

        [TestMethod,Priority(3)]
        public void AlterarBinKey()
        {

            String json = "{\"sample\":\"carla caroline frazao\"}";
            
            new RestAssured()
                .Given()
                     .Name("Com chave de acesso")
                     .Header("Content-Type", "application/json")
                     .Header("private", privado)
                     .Header("secret-key", CHAVEACESSO)
                     .Body(json)

                .When()
                    .Put($"https://api.jsonbin.io/b/{uid}")
                .Then()
                   .TestBody("status", x => x.success== "true")
                   .Assert("status");
        }


        [TestMethod,Priority(7)]
        public void DetelatarBinKey()
        {
           
                new RestAssured()
                .Given()
                     .Name("Com chave de acesso")
                     .Header("Content-Type", "application/json")
                     .Header("private", privado)
                     .Header("secret-key", CHAVEACESSO)
                .When()
                    .Delete($"https://api.jsonbin.io/b/{uid}")
                .Then()
                   .TestBody("status", x => x.success == "true")
                   .Assert("status");
        }


        [TestMethod,Priority(4)]
        public void ConsultarBin()
        {
            new RestAssured()
                .Given()
                    .Name("Sem chave de acesso")
                .When()
                    .Get($"https://api.jsonbin.io/b/{uid}")
                .Then()
                   .TestBody("status", x => x.success == "false")
                   .Assert("status");
        }

        [TestMethod,Priority(5)]
        public void AlterarBin()
        {
            String json = "{\"sample\": \"Desafio\"}";

            new RestAssured()
                .Given()
                     .Name("Sem chave de acesso")
                     .Header("Content-Type", "application/json")
                     .Body(json)

                .When()
                    .Put($"https://api.jsonbin.io/b/{uid}")
                .Then()
                   .TestBody("status", x => x.success == "false")
                   .Assert("status");
        }

        [TestMethod,Priority(6)]
        public void DetelatarBin()
        {

            new RestAssured()
            .Given()
                 .Name("Sem chave de acesso")
                 .Header("Content-Type", "application/json")
            .When()
                .Delete($"https://api.jsonbin.io/b/{uid}")
            .Then()
                .TestStatus("status", x => x == 401)
                   .Assert("status");
        }
    }
}
