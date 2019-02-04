using Crawler_Cotacoes.Classes;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;



namespace Crawler_Cotacoes
{
    public partial class Form1 : Form
    {
        string moeda;
        decimal valida = 0;
        int time = 0;
        int completo = 0;
        string datetime;
        string timer_ativo;
        string Endereco;
        string crawler_cotacao;
        public Form1()
        {
            InitializeComponent();
            Automatico();
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            datetime = DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");
            var xml = new XML();
            if (crawler_cotacao == "chile")
            {
                moeda = "Dólar Observado";
                var tables = webBrowser1.Document.GetElementsByTagName("table");
                var chile = new Chile(tables, moeda);
                var cotacao = chile.RetornaCotacao();
                valida = Convert.ToDecimal(cotacao);
                completo = 1;
                xml.CriarArquivo(datetime, crawler_cotacao);
                Console.WriteLine("<?xml version=\'1.0\' encoding=\'utf-8\'?>");
                Console.WriteLine("<body>");
                Console.WriteLine("<cotacoes>");
                Console.WriteLine("<pais>" + crawler_cotacao + "</pais>");
                Console.WriteLine("<moeda>" + moeda + "</moeda>");
                Console.WriteLine("<valor_venda>" + cotacao + "</valor_venda>");
                Console.WriteLine("</cotacoes>");
                Console.Write("</body>");
                xml.EncerraCriaArquivo();
                timer_ativo = "N";
            }
            if (crawler_cotacao == "china")
            {
                moeda = "USD";
                var tables = webBrowser1.Document.GetElementsByTagName("table");
                var china = new China(tables, moeda);
                try
                {
                    var cotacao_compra = china.RetornaCotacaoCompra();
                    var cotacao_venda = china.RetornaCotacaoVenda();
                    valida = Convert.ToDecimal(cotacao_venda);
                    completo = 1;
                    xml.CriarArquivo(datetime, crawler_cotacao);
                    Console.WriteLine("<?xml version=\'1.0\' encoding=\'utf-8\'?>");
                    Console.WriteLine("<body>");
                    Console.WriteLine("<cotacoes>");
                    Console.WriteLine("<pais>" + crawler_cotacao + "</pais>");
                    Console.WriteLine("<moeda>" + moeda + "</moeda>");
                    Console.WriteLine("<valor_compra>" + cotacao_compra + "</valor_compra>");
                    Console.WriteLine("<valor_venda>" + cotacao_venda + "</valor_venda>");
                    Console.WriteLine("</cotacoes>");
                    Console.Write("</body>");
                    xml.EncerraCriaArquivo();
                    timer_ativo = "N";
                }
                catch
                {
                }
            }
            if (crawler_cotacao == "japao" && completo==0)
            {
                moeda = "USD";
                var tables = webBrowser1.Document.GetElementsByTagName("table");
                var japao = new Japao(tables, moeda);
                try
                {
                    var cotacao_compra = japao.RetornaCotacaoCompra();
                    var cotacao_venda = japao.RetornaCotacaoVenda();
                    valida = Convert.ToDecimal(cotacao_venda);
                    completo = 1;
                    xml.CriarArquivo(datetime, crawler_cotacao);
                    Console.WriteLine("<?xml version=\'1.0\' encoding=\'utf-8\'?>");
                    Console.WriteLine("<body>");
                    Console.WriteLine("<cotacoes>");
                    Console.WriteLine("<pais>"+ crawler_cotacao + "</pais>");
                    Console.WriteLine("<moeda>"+ moeda + "</moeda>");
                    Console.WriteLine("<valor_compra>" + cotacao_compra + "</valor_compra>");
                    Console.WriteLine("<valor_venda>" + cotacao_venda + "</valor_venda>");
                    Console.WriteLine("</cotacoes>");
                    Console.Write("</body>");
                    xml.EncerraCriaArquivo();
                    timer_ativo = "N";
                }
                catch
                {
                }
            }
            if (crawler_cotacao == "brasil" && completo == 0)
            {
                var tables = webBrowser1.Document.GetElementsByTagName("table");
                var brasil = new Brasil(tables, moeda);
                try
                {
                    var cotacao_compra = brasil.RetornaCotacaoCompra();
                    var cotacao_venda = brasil.RetornaCotacaoVenda();
                    valida = Convert.ToDecimal(cotacao_venda);
                    completo = 1;
                    xml.CriarArquivo(datetime, crawler_cotacao);
                    Console.WriteLine("<?xml version=\'1.0\' encoding=\'utf-8\'?>");
                    Console.WriteLine("<body>");
                    Console.WriteLine("<cotacoes>");
                    Console.WriteLine("<pais>" + crawler_cotacao + "</pais>");
                    Console.WriteLine("<moeda>USD</moeda>");
                    Console.WriteLine("<valor_compra>" + cotacao_compra + "</valor_compra>");
                    Console.WriteLine("<valor_venda>" + cotacao_venda + "</valor_venda>");
                    Console.WriteLine("</cotacoes>");
                    Console.Write("</body>");
                    xml.EncerraCriaArquivo();
                    timer_ativo = "N";
                }
                catch
                {
                }
            }
            if (completo == 1)
            {
                Print();
                completo = 2;
            }
        }
        public void wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            //Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                //Console.WriteLine("stop wait timer");
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void Chile()
        {
            Endereco = ConfigurationManager.AppSettings.Get("Endereco_Cotacao_Chile");
            webBrowser1.Navigate(@Endereco);
            crawler_cotacao = "chile";
            completo = 0;
            valida = 0;
            timer_ativo = "S";
        }

        private void China()
        {
            Endereco = ConfigurationManager.AppSettings.Get("Endereco_Cotacao_China");
            webBrowser1.Navigate(@Endereco);
            crawler_cotacao = "china";
            completo = 0;
            valida = 0;
            timer_ativo = "S";
        }

        private void Japao()
        {
            completo = 0;
            Endereco = ConfigurationManager.AppSettings.Get("Endereco_Cotacao_Japao");
            webBrowser1.Navigate(@Endereco);
            crawler_cotacao = "japao";
            valida = 0;
            timer_ativo = "S";
        }
        private void Brasil()
        {
            completo = 0;
            Endereco = ConfigurationManager.AppSettings.Get("Endereco_Cotacao_Brasil"); ;
            var parametros = "&ChkMoeda=61&RadOpcao=3&DATAFIM=&DATAINI="+DateTime.Today.ToString("dd/MM/yyyy");
            webBrowser1.Navigate(@Endereco+parametros);
            crawler_cotacao = "brasil";
            valida = 0;
            timer_ativo = "S";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer_ativo == "S")
            {
                time++;
                if (time >= 9 && completo == 0 && crawler_cotacao == "japao")
                {
                    time = 0;
                    Japao();
                }
                if (time >= 30 && completo == 0 && crawler_cotacao == "china")
                {
                    time = 0;
                    China();
                }
                if (time >= 30 && completo == 0 && crawler_cotacao == "chile")
                {
                    time = 0;
                    Chile();
                }
            }
        }
        private void Print()
        {
            var Endereco_Imagens = ConfigurationManager.AppSettings.Get("Endereco_Imagens");
            Bitmap thumbnail = GenerateScreenshot(2048, 1536);
            thumbnail.Save(Endereco_Imagens+crawler_cotacao + "_"+datetime+".png", System.Drawing.Imaging.ImageFormat.Png);
        }

        public Bitmap GenerateScreenshot(int width, int height)
        {
            webBrowser1.ScrollBarsEnabled = false;
            webBrowser1.ScriptErrorsSuppressed = true;
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete) { Application.DoEvents(); }
            webBrowser1.Width = width;
            webBrowser1.Height = height;
            if (width == -1)
            {
                webBrowser1.Width = webBrowser1.Document.Body.ScrollRectangle.Width;
            }
            if (height == -1)
            {
                webBrowser1.Height = webBrowser1.Document.Body.ScrollRectangle.Height;
            }
            Bitmap bitmap = new Bitmap(webBrowser1.Width, webBrowser1.Height);
            webBrowser1.DrawToBitmap(bitmap, new Rectangle(0, 0, webBrowser1.Width, webBrowser1.Height));
            return bitmap;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Chile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            China();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Japao();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Brasil();
        }
        public void Automatico()
        {
            Chile();
            while (completo != 2)
            {
                wait(1000);
            }
            China();
            while (completo != 2)
            {
                wait(1000);
            }
            Japao();
            while (completo != 2)
            {
                wait(1000);
            }
            Brasil();
            while (completo != 2)
            {
                wait(1000);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Automatico();
        }
    }
}