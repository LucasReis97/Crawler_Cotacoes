using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Crawler_Cotacoes
{
    class Chile
    {
        private string Cotacao { get; set; }

        public Chile(HtmlElementCollection tables, string nome_cotacao)
        {
            var result = new List<String>();

            foreach (HtmlElement table in tables)
            {
                var tableRows = table.GetElementsByTagName("tr");
                foreach (HtmlElement tr in tableRows)
                {
                    var tableRowCells = tr.GetElementsByTagName("td");
                    foreach (HtmlElement td in tableRows)
                    {
                        if (td.InnerHtml.Contains(nome_cotacao))
                        {
                            result.Add(td.InnerHtml);
                        }

                    }
                }
            }
            result.ToArray();
            Cotacao = result[0];
            Cotacao = Cotacao.Replace("<td>", "");
            Cotacao = Cotacao.Replace("</td>", "");
            Cotacao = Cotacao.Replace("\n", "");
            Cotacao = Cotacao.Replace("\t", "");
            Cotacao = Cotacao.Replace(nome_cotacao, "");
            Cotacao = Cotacao.Replace(" ", "");
        }

        public string RetornaCotacao ()
        {
            return Cotacao;
        }
    }
}
