using System;
using System.Configuration;
using System.IO;
using System.Linq;


namespace Crawler_Cotacoes
{
    class XML
    {
        private StreamWriter writer { get; set; }
        private TextWriter oldOut { get; set; }
        private string Endereco_XML { get; set; }

        public void CriarArquivo(string datetime, string crawler_cotacao)
        {
            Endereco_XML = ConfigurationManager.AppSettings.Get("Endereco_XML");
            oldOut = Console.Out;
            var Out = (@Endereco_XML + crawler_cotacao + "_" + datetime + ".xml");
            writer = new StreamWriter(Out);
            Console.SetOut(writer);
        }
        public void EncerraCriaArquivo()
        {
            Console.SetOut(oldOut);
            writer.Close();
        }
    }
}
