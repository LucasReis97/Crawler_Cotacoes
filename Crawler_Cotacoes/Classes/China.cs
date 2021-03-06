﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Crawler_Cotacoes
{
    class China
    {
        private string CotacaoVenda { get; set; }
        private string CotacaoCompra { get; set; }

        public China(HtmlElementCollection tables,string nome_cotacao)
        {
            var result = new List<String>();

            foreach (HtmlElement table in tables)
            {
                var tableRows = table.GetElementsByTagName("tr");
                foreach (HtmlElement tr in tableRows)
                {
                    var tableRowCells = tr.GetElementsByTagName("td");
                    foreach (HtmlElement td in tableRowCells)
                    {
                        result.Add(td.InnerHtml);

                    }
                }
            }
            result.ToArray();
            string[] result_array = result.ToArray();
            var index = Array.IndexOf(result_array, nome_cotacao);
            CotacaoCompra = result[index+1];
            CotacaoVenda = result[index+3];
        }
        public string RetornaCotacaoCompra()
        {
            return CotacaoCompra;
        }
        public string RetornaCotacaoVenda()
        {
            return CotacaoVenda;
        }
    }
}
