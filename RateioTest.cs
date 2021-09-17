using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rateio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RateioTest
{
    [TestClass]
    public class RateioTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void RatearNotaCorretagem1()
        {
            TestContext.WriteLine("Adicionando valores da Nota de Corretagem 1");
            RateioVO rateioVO = new RateioVO();
            rateioVO.TotalCompras = 13673.0;
            rateioVO.ValorLiquido = 1011.21;
            rateioVO.TotalVendas = 14750.0;
            rateioVO.Corretagem = 10.0;

            ListView.ListViewItemCollection items = new ListView.ListViewItemCollection(new ListView());
            string[] array = { "ITUB4", "R$ 14.750,00", "" };
            items.Add(new ListViewItem(array));
            array = new string[3] { "B3SA3", "R$ 6.884,00", "" };
            items.Add(new ListViewItem(array));
            array = new string[3] { "ENBR3", "R$ 1.865,00", "" };
            items.Add(new ListViewItem(array));
            array = new string[3] { "HYPE3", "R$ 3.599,00", "" };
            items.Add(new ListViewItem(array));
            array = new string[3] { "BRSR6", "R$ 1.325,00", "" };
            items.Add(new ListViewItem(array));
            rateioVO.Items = items;

            Dictionary<string, string> expected = new Dictionary<string, string>();
            expected.Add("ITUB4", "R$ 18,19");
            expected.Add("B3SA3", "R$ 13,82");
            expected.Add("ENBR3", "R$ 11,04");
            expected.Add("HYPE3", "R$ 12,00");
            expected.Add("BRSR6", "R$ 10,74");

            TestContext.WriteLine("Testando rateio para Nota de Corretagem 1");
            RateioService rateioService = new Rateio.RateioService();
            rateioService.RatearCustos(rateioVO);

            TestContext.WriteLine("Verificando resultado do teste para Nota de Corretagem 1");
            Assert.IsTrue(IsTaxasIguais(rateioVO.Items, expected));
        }

        [TestMethod]
        public void RatearNotaCorretagem2()
        {
            TestContext.WriteLine("Adicionando valores da Nota de Corretagem 2");
            RateioVO rateioVO = new RateioVO();
            rateioVO.TotalCompras = 7591.0;
            rateioVO.ValorLiquido = 1550.93;
            rateioVO.TotalVendas = 6090.0;
            rateioVO.Corretagem = 10.0;

            ListView.ListViewItemCollection items = new ListView.ListViewItemCollection(new ListView());
            string[] array = { "SAPR11", "R$ 6.090,00", "" };
            items.Add(new ListViewItem(array));
            array = new string[3] { "ALUP11", "R$ 2.793,00", "" };
            items.Add(new ListViewItem(array));
            array = new string[3] { "WEGE3", "R$ 3.360,00", "" };
            items.Add(new ListViewItem(array));
            array = new string[3] { "BRSR6", "R$ 1.438,00", "" };
            items.Add(new ListViewItem(array));
            rateioVO.Items = items;

            Dictionary<string, string> expected = new Dictionary<string, string>();
            expected.Add("SAPR11", "R$ 14,42");
            expected.Add("ALUP11", "R$ 12,03");
            expected.Add("WEGE3", "R$ 12,44");
            expected.Add("BRSR6", "R$ 11,04");

            TestContext.WriteLine("Testando rateio para Nota de Corretagem 2");
            RateioService rateioService = new Rateio.RateioService();
            rateioService.RatearCustos(rateioVO);

            TestContext.WriteLine("Verificando resultado do teste para Nota de Corretagem 2");        
            Assert.IsTrue(IsTaxasIguais(rateioVO.Items, expected));
        }

        [TestMethod]
        public void RatearNotaCorretagem3()
        {
            TestContext.WriteLine("Adicionando valores da Nota de Corretagem 3");
            RateioVO rateioVO = new RateioVO();
            rateioVO.TotalCompras = 0.0;
            rateioVO.ValorLiquido = 8461.94;
            rateioVO.TotalVendas = 8464.47;
            rateioVO.Corretagem = 0.0;

            ListView.ListViewItemCollection items = new ListView.ListViewItemCollection(new ListView());
            string[] array = { "HGLG11", "R$ 1.352,80", "" };
            items.Add(new ListViewItem(array));
            array = new string[3] { "HGRU11", "R$ 3.282,24", "" };
            items.Add(new ListViewItem(array));
            array = new string[3] { "RBRP11", "R$ 1.437,28", "" };
            items.Add(new ListViewItem(array));
            array = new string[3] { "XPML22", "R$ 2.392,15", "" };
            items.Add(new ListViewItem(array));
            rateioVO.Items = items;

            Dictionary<string, string> expected = new Dictionary<string, string>();
            expected.Add("HGLG11", "R$ 0,40");
            expected.Add("HGRU11", "R$ 0,98");
            expected.Add("RBRP11", "R$ 0,43");
            expected.Add("XPML22", "R$ 0,72");

            TestContext.WriteLine("Testando rateio para Nota de Corretagem 3");
            RateioService rateioService = new Rateio.RateioService();
            rateioService.RatearCustos(rateioVO);

            TestContext.WriteLine("Verificando resultado do teste para Nota de Corretagem 3");
            Assert.IsTrue(IsTaxasIguais(rateioVO.Items, expected));
        }

        private bool IsTaxasIguais(ListView.ListViewItemCollection items, Dictionary<string, string> expected)
        {
            bool match = true;
            foreach (ListViewItem item in items)
            {
                string ticket = item.SubItems[0].Text;
                string taxa = item.SubItems[2].Text;
                if (!expected[ticket].Equals(taxa))
                {
                    TestContext.WriteLine($"Para o ticket {ticket} era esperado {expected[ticket]} mas encontrou {taxa}");
                    match = false;
                    break;
                }
            }
            return match;
        }
    }
}
