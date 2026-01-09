using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Globalization;
using System.Data;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Drawing;

namespace AnalisardorCartao
{
    /// <summary>
    /// Funções do sistema.
    /// </summary>
    public class Funcoes
    {
        public Funcoes()
        {
        }

        public enum TipoCrypto
        {
            ENCRYPT,
            DECRYPT
        };

        /// <summary>
        /// Verifica se o caminmho é termincado com \.
        /// </summary>
        /// <parametrosDao name="sCaminho">O caminho a ser verficado.</parametrosDao>
        /// <returns>O caminho terminado com \</returns>
        public static string SFormataCaminho(string sCaminho)
        {
            if (!sCaminho.EndsWith(@"\"))
            {
                sCaminho += @"\";
            }
            return sCaminho;
        }

        /// <summary>
        /// Verifica o CPF.
        /// </summary>
        /// <parametrosDao name="sCpf">Número do CPF.</parametrosDao>
        /// <returns>Verdadeiro ou False.</returns>
        public static bool ValidaCpf(string sCpf)
        {
            int nSoma = 0;
            int Ndigi1;
            int Ndigi2;
            string digito;

            if (sCpf.Length != 11)
            {
                return false;
            }

            if (sCpf == "11111111111" | sCpf == "22222222222" |
                sCpf == "33333333333" | sCpf == "44444444444" |
                sCpf == "55555555555" | sCpf == "66666666666" |
                sCpf == "77777777777" | sCpf == "88888888888" |
                sCpf == "99999999999")
            {
                return false;
            }

            digito = sCpf.Substring(sCpf.Length - 2, 2);
            for (int nII = 0; nII < 9; nII++)
            {
                Ndigi1 = Convert.ToInt32(sCpf.Substring(nII, 1)) * (nII + 1);
                nSoma += Ndigi1;
            }

            Ndigi1 = nSoma % 11;

            if (Ndigi1 >= 10)
            {
                Ndigi1 = 0;
            }

            nSoma = 0;

            for (int nII = 0; nII < 10; nII++)
            {
                Ndigi2 = Convert.ToInt32(sCpf.Substring(nII, 1)) * (nII);
                nSoma += Ndigi2;
            }

            Ndigi2 = nSoma % 11;

            if (Ndigi2 >= 10)
            {
                Ndigi2 = 0;
            }

            return (digito == Convert.ToString(Ndigi1) + Convert.ToString(Ndigi2));
        }

        private static string CalculaCGC(string Numero)
        {
            int prod = 0;

            if (Numero.Length == 0)
            {
                return "";
            }

            int mult = 9;
            for (int i = Numero.Length - 1; i >= 0; i--)
            {
                string sDig = Numero.Substring(i, 1);
                prod += Convert.ToInt32(sDig) * mult;
                mult -= 1;
                if (mult == 1)
                {
                    mult = 9;
                }
            }

            int digito = prod % 11;
            if (digito == 10)
            {
                digito = 0;
            }

            return digito.ToString().Trim();
        }

        /// <summary>
        /// Valida o CNPJ.
        /// </summary>
        /// <parametrosDao name="cnpj">Número do CNPJ.</parametrosDao>
        /// <returns>Verdadeiro ou False.</returns>
        public static bool Valida_CNPJ(string cnpj)
        {
            string sCnpj = SoNumeros(cnpj);

            if (sCnpj.Length != 14)
            {
                return false;
            }

            if (sCnpj == "11111111111111" | sCnpj == "22222222222222" |
                sCnpj == "33333333333333" | sCnpj == "44444444444444" |
                sCnpj == "55555555555555" | sCnpj == "66666666666666" |
                sCnpj == "77777777777777" | sCnpj == "88888888888888" |
                sCnpj == "99999999999999")
            {
                return false;
            }

            if (sCnpj.Trim().Length == 0)
            {
                return false;
            }

            string sDig = CalculaCGC(sCnpj.Substring(0, 12));
            if (sDig != sCnpj.Substring(12, 1))
            {
                return false;
            }

            sDig = CalculaCGC(sCnpj.Substring(0, 13));

            return (sDig == sCnpj.Substring(13, 1));
        }


        /// <summary>
        /// Verifica o EAN 8 ou 13.
        /// </summary>
        /// <parametrosDao name="sean">EAN contendo 8 ou 13 digitos.</parametrosDao>
        /// <returns>Verdadeiro ou False.</returns>
        public static bool ValidaEAN813(string sean)
        {
            int soma = 0;
            string vEan = sean.Substring(0, sean.Length - 1);
            string vDig = sean.Substring(sean.Length - 1, 1);

            for (int i = 0; i < vEan.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    soma += Convert.ToInt32(vEan.Substring(i, 1));
                }
                else
                {
                    soma += Convert.ToInt32(vEan.Substring(i, 1)) * 3;
                }
            }

            int digito = 10 - soma % 10;

            return (vDig == Convert.ToString(digito));
        }

        /// <summary>
        /// Coleta informação em uma string delimitada na posição informada.
        /// </summary>
        /// <parametrosDao name="sfrom">String contendo os dados.</parametrosDao>
        /// <parametrosDao name="delim">Delimitador.</parametrosDao>
        /// <parametrosDao name="Index">Posição dentro da string.</parametrosDao>
        /// <returns>String contendo os dados dentro do delimitador.</returns>
        public static string GetPiece(string sfrom, string delim, int Index)
        {
            string Temp;
            int Count;
            int Where;

            Temp = sfrom + delim;
            Where = Temp.IndexOf(delim);
            Count = 0;
            while (Where > 0)
            {
                Count += 1;
                if (Count == Index)
                {
                    return Temp.Substring(0, Where);
                }
                Temp = Temp.Substring(Where + 1);
                Where = Temp.IndexOf(delim);
            }
            if (Count == 0)
                return sfrom;
            else
                return "";
        }

        /// <summary>
        /// Último dia do mes/ano passado como parâmetro.
        /// </summary>
        /// <parametrosDao name="nMes">Número representando o mês (1 a 12)</parametrosDao>
        /// <parametrosDao name="nAno">Número representando o ano (yyyy)</parametrosDao>
        /// <returns>DateTime completo contendo o último dia</returns>
        public static DateTime UltimoDia(int nMes, int nAno)
        {
            DateTime dRet;
            string sd;
            int dias = DateTime.DaysInMonth(nAno, nMes);
            sd = dias.ToString("00") + "/" + nMes.ToString("00") + "/" + nAno.ToString("0000");
            dRet = DateTime.Parse(sd);
            if (dRet > DateTime.Today)
                dRet = DateTime.Today;

            return dRet;
        }

        ///<summary>
        ///Validação da chave NFe da nota fiscal.
        ///</summary>
        ///<parametrosDao name="Chave">Chave contendo 44 caracteres numéricos</parametrosDao>
        ///<returns>Verdadeiro ou Falso</returns>
        public static bool ValidaChaveNFE(string Chave)
        {
            int i;
            byte C;
            int Key = 0;
            int iDG;
            int vDg;

            if (Chave.Length != 44)
                return false;

            vDg = int.Parse(Chave.Substring(Chave.Length - 1));
            Chave = Chave.Substring(0, 43);
            C = 2;

            //faz um loop por cada número o mutiplicando-o pelos valores de C
            for (i = Chave.Length - 1; i >= 0; i--)
            {
                //vericica se o valor de c for maior que nove passa o valor para 2
                if (C > 9)
                    C = 2;

                //soma os valores mutiplicados
                Key += int.Parse(Chave.Substring(i, 1)) * C;
                C += 1;
            }

            //obtem o Digito Verificador
            if ((Key % 11) == 0 | (Key % 11) == 1)
                iDG = 0;
            else
                iDG = 11 - (Key % 11);

            return (iDG == vDg);
        }

        /// <summary>
        /// Cryptografa uma string usando uma chave.
        /// </summary>
        /// <parametrosDao name="sTexto">String a ser cryptografada</parametrosDao>
        /// <parametrosDao name="ChavePar">String chave para embaralhar o texto</parametrosDao>
        /// <parametrosDao name="Acao">Tipo de ação a realizar (encryptar ou decryptar)</parametrosDao>
        /// <returns>String cryptografada</returns>
        public static string Criptografia(string sTexto, string Chave, TipoCrypto Acao)
        {
            //define as variaveis usadas
            int Temp;
            int j;
            int n;
            string rtn = "";

            //Obtem os caracteres da chave do usuário
            //define o comprimento da chave do usuario usada na criptografia
            n = Chave.Length;

            //redimensiona o array para o tamanho definido
            byte[] userKeyASCIIS = new byte[Encoding.ASCII.GetByteCount(Chave)];
            for (int i = 0; i < Chave.Length; i++)
            {
                userKeyASCIIS[i] = Convert.ToByte(char.Parse(Chave.Substring(i, 1)));
            }

            //redimensiona o array com o tamanho do texto
            //obtem o caractere de texto
            byte[] baites = new byte[Encoding.ASCII.GetByteCount(sTexto)];

            //preenche o array com caracteres asc
            for (int i = 0; i < sTexto.Length; i++)
            {
                baites[i] = Convert.ToByte(char.Parse(sTexto.Substring(i, 1)));
            }

            //cifra/decifra
            j = 0;
            if (Acao == TipoCrypto.ENCRYPT)
            {
                foreach (byte bt in baites)
                {
                    Temp = bt + Convert.ToInt32(userKeyASCIIS[j]);
                    if (Temp > 255)
                        Temp -= 255;
                    rtn += (char)Temp;

                    if (j >= (n - 1))
                        j = 0;
                    else
                        j++;
                }
            }
            else if (Acao == TipoCrypto.DECRYPT)
            {
                foreach (byte bt in baites)
                {
                    Temp = bt - Convert.ToInt32(userKeyASCIIS[j]);
                    if (Temp < 0)
                        Temp += 255;

                    if (j >= (n - 1))
                        j = 0;
                    else
                        j++;

                    rtn += (char)Temp;
                }
                rtn = rtn.Trim();
            }

            //Retorna o texto
            return rtn;
        }

        /// <summary>
        /// Ler dados do arquivo xml de configuração.
        /// </summary>
        /// <parametrosDao name="sElemento">O elemento a ser lido.</parametrosDao>
        /// <returns>Os dados referente ao elemento lido.</returns>
        public static string LeConfiguracao(string sElemento)
        {
            XmlDocument xmlconfig = new XmlDocument();

            string xmlFile = Application.StartupPath + @"\config.xml";

            if (File.Exists(xmlFile))
            {
                xmlconfig.Load(xmlFile);
                XmlNode dxml = xmlconfig.GetElementsByTagName(sElemento).Item(0);
                if (dxml != null)
                    return dxml.InnerText;
            }
            return "";
        }

        public static void GravaConfiguracao(string sElemento, string valor)
        {
            XmlDocument xmlconfig = new XmlDocument();
            XmlNode dxml;
            XmlNode raiz;
            string xmlFile = Application.StartupPath + @"\config.xml";
            if (!File.Exists(xmlFile))
            {
                XmlNode declaration = xmlconfig.CreateXmlDeclaration("1.0", "UTF-8", "");
                xmlconfig.AppendChild(declaration);
                raiz = xmlconfig.CreateElement("configuracao");
                xmlconfig.AppendChild(raiz);
            }
            else
            {
                xmlconfig.Load(xmlFile);
                raiz = xmlconfig.GetElementsByTagName("configuracao").Item(0);
            }

            dxml = xmlconfig.GetElementsByTagName(sElemento).Item(0);
            if (dxml == null)
            {
                dxml = xmlconfig.CreateElement(sElemento);
                dxml.InnerText = valor;
                raiz.AppendChild(dxml);
            }
            else
            {
                dxml.InnerText = valor;
            }
            xmlconfig.Save(xmlFile);
        }

        /// <summary>
        /// Gera o dígito verificador do Nosso Número.
        /// </summary>
        /// <parametrosDao name="Numero">Número para o qual será gerado o dígito.</parametrosDao>
        /// <returns>O dígito gerado.</returns>
        public static string DigitoNosso(string Numero)
        {
            int nBase = 2;
            int nSoma = 0;
            int nDigito;

            Numero = SoNumeros(Numero);

            for (int i = Numero.Length - 1; i >= 0; i--)
            {
                nSoma += (Convert.ToInt32(Numero.Substring(i, 1)) * nBase);
                nBase++;
                if (nBase == 10)
                    nBase = 2;
            }
            nDigito = nSoma % 11;
            nDigito = 11 - nDigito;
            if (nDigito >= 10)
                nDigito = 0;

            return nDigito.ToString().Trim();
        }

        /// <summary>
        /// Retorna o dígito verificador do código de barras do boleto.
        /// </summary>
        /// <parametrosDao name="Numero">Código de barras</parametrosDao>
        /// <returns>Dígito verificador</returns>
        public static string DigitoBarra(string Numero)
        {
            int nBase = 2;
            int nSoma = 0;
            int nDigito;

            for (int i = Numero.Length - 1; i >= 0; i--)
            {
                nSoma += (Convert.ToInt32(Numero.Substring(i, 1)) * nBase);
                nBase++;
                if (nBase == 10)
                    nBase = 2;
            }
            nDigito = nSoma % 11;
            nDigito = 11 - nDigito;
            if (nDigito <= 0 | nDigito >= 10)
                nDigito = 1;

            return nDigito.ToString().Trim();
        }

        /// <summary>
        /// Retorna o dígito verificador do código digitével do boleto
        /// </summary>
        /// <parametrosDao name="Numero">Os números do código</parametrosDao>
        /// <returns>Dígito verificador</returns>
        public static string DigitoCodigo(string Numero)
        {
            int nSoma = 0;
            int nDigito;
            int nRet;
            string sRet;

            Numero = Numero.PadLeft(15, '0');
            for (int i = 14; i >= 0; i--)
            {
                if (i % 2 == 0)
                {
                    nSoma += (Convert.ToInt32(Numero.Substring(i, 1)) * 1);
                }
                else
                {
                    nRet = Convert.ToInt32(Numero.Substring(i, 1)) * 2;
                    sRet = nRet.ToString().Trim();
                    if (sRet.Length == 2)
                        nSoma += (Convert.ToInt32(sRet.Substring(0, 1)) + Convert.ToInt32(sRet.Substring(1, 1)));
                    else
                        nSoma += nRet;
                }
            }
            nDigito = nSoma % 10;
            if (nDigito > 0)
                nDigito = 10 - nDigito;

            return nDigito.ToString().Trim();
        }

        /// <summary>
        /// Permite apenas números em um TextBox
        /// </summary>
        /// <parametrosDao name="e">O argumento e do evento KeyPress do TextBox</parametrosDao>
        public static void AllowNumber(KeyPressEventArgs e)
        {
            string numeros = "0123456789.,";

            if (numeros.Contains(e.KeyChar.ToString()))
            {
                e.Handled = false;
            }
            else if (char.IsLetter(e.KeyChar) || //Letras
               char.IsWhiteSpace(e.KeyChar) || //Espaço
               char.IsPunctuation(e.KeyChar)) //Pontuação
            {
                e.Handled = true; //Não permitir
            }
            else if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
        }

        /// <summary>
        /// Permite apenas valores numéricos de ponto flutuante
        /// </summary>
        /// <parametrosDao name="e">O argumento e do evento KeyPress do TextBox</parametrosDao>
        /// <parametrosDao name="_campo">O TextBox</parametrosDao>
        public static void AllowNumber(KeyPressEventArgs e, TextBox _campo)
        {
            string numeros = "0123456789,.";

            if (numeros.Contains(e.KeyChar.ToString()))
            {
                e.Handled = false;
                if (e.KeyChar == '.')
                {
                    e.KeyChar = ',';
                }
                if (e.KeyChar == ',' || e.KeyChar == '.')
                {
                    if (_campo.Text.Contains(","))
                    {
                        e.Handled = true;
                    }
                }
            }
            else if (char.IsLetter(e.KeyChar) || //Letras
               char.IsSymbol(e.KeyChar) || //Símbolos
               char.IsWhiteSpace(e.KeyChar) || //Espaço
               char.IsPunctuation(e.KeyChar)) //Pontuação
            {
                e.Handled = true; //Não permitir
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        public static void Decimal(object sender, KeyPressEventArgs Event)
        {
            Event.Handled = true;
            bool FalseInput = !char.IsControl(Event.KeyChar) && !char.IsDigit(Event.KeyChar) && !char.IsControl(Event.KeyChar) && Event.KeyChar != 8 && Event.KeyChar != '.';
            if (!FalseInput)
            {
                Event.Handled = false;
                if (Regex.IsMatch(((TextBox)sender).Text, @"^\d+\,\d*$") && Event.KeyChar != 8)
                {
                    bool ContainDot = ((TextBox)sender).Text.Contains(",");
                    Event.Handled = true;
                    if (ContainDot && Event.KeyChar != 8 && Event.KeyChar != ',')
                    {
                        Event.Handled = Regex.IsMatch(((TextBox)sender).Text, @"\,\d\d");
                    }
                }
            }
        }

        /// <summary>
        /// Retorna apenas os números de uma string.
        /// </summary>
        /// <parametrosDao name="entrada">String a ser checada</parametrosDao>
        /// <returns>string contendo apenas números</returns>
        public static string SoNumeros(string entrada)
        {
            string saida = "";
            char[] teste = entrada.ToCharArray();

            for (int i = 0; i < teste.Length; i++)
            {
                if (char.IsNumber(teste[i]))
                {
                    saida += teste[i];
                }
            }
            return saida;
        }


        /// <summary>
        /// Envia um email com anexo, para enviar boletos por email.
        /// </summary>
        /// <parametrosDao name="host">Endereço smtp do servidor de email.</parametrosDao>
        /// <parametrosDao name="porta">Porta do servidor smtp.</parametrosDao>
        /// <parametrosDao name="user">Nome do usuario ou email.</parametrosDao>
        /// <parametrosDao name="pass">Senha do eamil.</parametrosDao>
        /// <parametrosDao name="addr">Endereço de email do remetente.</parametrosDao>
        /// <parametrosDao name="nome">Nome do remetente.</parametrosDao>
        /// <parametrosDao name="para">Endereço de email do destinatário.</parametrosDao>
        /// <parametrosDao name="mens">Texto do corpo da mensagem.</parametrosDao>
        /// <parametrosDao name="anexo">Arquivo para ser enviado como anexo.</parametrosDao>
        /// <returns>Verdadeiro ou falso indicando se o email foi enviado.</returns>
        public static bool EnviarEmail(string host, int porta, string user, string pass,
            string addr, string nome, string para, string mens, string anexo)
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage email = new MailMessage();
            Attachment at = null;
            bool ret = false;

            try
            {
                smtp.Timeout = 30000;
                smtp.Host = host;
                smtp.Port = porta;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(user, pass);

                //email.Sender = new MailAddress(addr, nome);
                email.From = new MailAddress(addr, nome);
                email.To.Add(new MailAddress(para));
                email.Subject = mens;
                email.Body = mens;
                email.IsBodyHtml = false;
                email.Priority = MailPriority.Normal;

                if (!anexo.Equals(""))
                {
                    if (File.Exists(anexo))
                    {
                        at = new Attachment(anexo);
                        email.Attachments.Add(at);
                    }
                }

                smtp.Send(email);
                ret = true;
            }
            catch (SmtpException e)
            {
                MessageBox.Show(e.Message);
            }
            email.Dispose();
            at.Dispose();
            smtp.Dispose();
            return ret;
        }

        /// <summary>
        /// Validação de endereço de email.
        /// </summary>
        /// <parametrosDao name="strIn">Email a ser testado.</parametrosDao>
        /// <returns>Verdadeiro ou falso.</returns>
        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// Retorna um valor double formatado sem pontos ou virgulas
        /// exemplo 234,6767 com 2 casas decimais e tamanho 10 -> 0000023467 
        /// </summary>
        /// <parametrosDao name="valor">Valor a ser formatado</parametrosDao>
        /// <parametrosDao name="decimais">Número de casas decimais</parametrosDao>
        /// <parametrosDao name="zerosdireita">tamanho total do retornocom zeros a direita</parametrosDao>
        /// <returns>String formatada</returns>
        public static string RetornaFormatado(double valor, int decimais, int zerosdireita)
        {
            string dec;

            string ret = valor.ToString().Replace(".", ",");
            if (ret.IndexOf(",") > 0)
            {
                dec = ret.Substring(ret.IndexOf(",") + 1);
                ret = ret.Substring(0, ret.IndexOf(","));
                if (dec.Length < decimais)
                {
                    dec += "".PadRight(decimais - dec.Length, '0');
                }
                else if (dec.Length > decimais)
                {
                    dec = dec.Substring(0, decimais);
                }
                ret += dec;
            }
            else
            {
                ret += "".PadRight(decimais, '0');
            }
            return ret.PadLeft(zerosdireita, '0');
        }

        /// <summary>
        /// Remove os acentos e caracteres especiais do texto.
        /// </summary>
        /// <parametrosDao name="text">Texto para remover os caracteres</parametrosDao>
        /// <returns>Texto sem acentos ou caracteres especiais</returns>
        public static string RemoverAcentuacao(string text)
        {
            return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }

        /// <summary>
        /// Verifica se um arquivo de imagem está corrompido.
        /// </summary>
        /// <parametrosDao name="bufi">Buffer da imagem a ser verificada</parametrosDao>
        /// <returns>Verdadeiro se a imagem estiver corrompida</returns>
        public static bool IsBadImage(Bitmap bufi)
        {
            int maxBadPixels = (int)(bufi.Width * bufi.Height * 0.05);
            int badPixels = 0;
            for (int j = 0; j < bufi.Height; j++)
            {
                for (int i = 0; i < bufi.Width; i++)
                {
                    if (bufi.GetPixel(i, j).B == 128 && bufi.GetPixel(i, j).G == 128 && bufi.GetPixel(i, j).R == 128)
                    {
                        badPixels++;
                        if (badPixels > maxBadPixels)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Retorna uma string formatada com os números em formato de telefone.
        /// </summary>
        /// <parametrosDao name="numeros">Os números que representam o telefone</parametrosDao>
        /// <returns>String formatada</returns>
        public static string FormataTelefone(string numeros)
        {
            string ret;
            ret = numeros;
            switch (numeros.Length)
            {
                case 8:
                    ret = string.Concat(numeros.Substring(0, 4), "-", numeros.Substring(4));
                    break;
                case 9:
                    ret = string.Concat(numeros.Substring(0, 5), "-", numeros.Substring(5));
                    break;
                case 10:
                    ret = string.Concat(numeros.Substring(0, 2), " ", numeros.Substring(2, 4), "-", numeros.Substring(6));
                    break;
                case 11:
                    ret = string.Concat(numeros.Substring(0, 2), " ", numeros.Substring(2, 5), "-", numeros.Substring(7));
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Retorna uma string formatada como CEP.
        /// </summary>
        /// <parametrosDao name="numeros">Os números do CEP</parametrosDao>
        /// <returns>String formatada</returns>
        public static string FormataCep(string numeros)
        {
            string ret;
            ret = numeros;
            if (numeros.Length == 8)
            {
                ret = string.Concat(numeros.Substring(0, 5), "-", numeros.Substring(5));
            }
            return ret;
        }

        /// <summary>
        /// Retorna uma string formatada como CPF ou CNPJ conforme número de dígitos.
        /// </summary>
        /// <parametrosDao name="numeros">Os números que representam o CPF ou CNPJ</parametrosDao>
        /// <returns>String formatada conforme número de dígitos</returns>
        public static string FormataCpfCnpj(string numeros)
        {
            string ret;
            ret = numeros;
            switch (numeros.Length)
            {
                case 11:
                    ret = Convert.ToUInt64(numeros).ToString(@"000\.000\.000\-00");
                    break;
                case 14:
                    ret = Convert.ToUInt64(numeros).ToString(@"00\.000\.000\/0000\-00");
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Troca as aspas por circunfléxo
        /// </summary>
        /// <parametrosDao name="entrada">String com aspas</parametrosDao>
        /// <returns>String sem aspas</returns>
        public static string TiraAspas(string entrada)
        {
            return entrada.Replace("'", "''");
        }


        /// <summary>
        /// Valida uma string contendo um endereço IP.
        /// </summary>
        /// <parametrosDao name="ip">Endereço IP.</parametrosDao>
        /// <returns>Verdadiero se o IP for válido ou falso caso não.</returns>
        public static bool IPvalido(string ip)
        {
            string str = @"^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$";
            Regex obj = new Regex(str);
            return obj.IsMatch(ip);
        }


        //adapitação
        public static string Codifica(string texto)
        {
            int i;
            int nCar;
            string cCar = "";
            for (i = 0; i < texto.Length; i++)
            {
                nCar = Convert.ToInt32(char.Parse(texto.Substring(i, 1)));
                if (i % 2 == 0)
                    nCar -= 5;
                else
                    nCar += 5;

                cCar += (char)nCar;
            }
            return cCar;
        }

        public static string Decodifica(string texto)
        {
            int i;
            int nCar;
            string cCar = "";
            for (i = 0; i < texto.Length; i++)
            {
                nCar = Convert.ToInt32(char.Parse(texto.Substring(i, 1)));
                if (i % 2 == 0)
                    nCar += 5;
                else
                    nCar -= 5;

                cCar += (char)nCar;
            }
            return cCar;
        }

        public static bool IsNumerico(string dados)
        {
            if (double.TryParse(dados, out double numero))
                return true;
            else
                return false;
        }

        public static bool IsDate(string data)
        {
            if (DateTime.TryParse(data, out _))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Permite apenas valores numéricos de ponto flutuante
        /// </summary>
        /// <parametrosDao name="e">O argumento e do evento KeyPress do TextBox</parametrosDao>
        /// <parametrosDao name="_campo">O DataGridViewCell</parametrosDao>
        /// <parametrosDao name="Decimais">Número de casas decimais</parametrosDao>
        public static void AllowNumberGrid(KeyEventArgs ke)
        {
            string numeros = "0123456789,.";
            char e = (char)ke.KeyValue;
            if (numeros.Contains(e))
            {
                ke.SuppressKeyPress = false;
                if (e == '.')
                {
                    e = ',';
                }
            }
            else if (char.IsLetter(e) || //Letras
               char.IsSymbol(e) || //Símbolos
               char.IsWhiteSpace(e) || //Espaço
               char.IsPunctuation(e)) //Pontuação
            {
                ke.SuppressKeyPress = true; //Não permitir
            }
            else if (char.IsControl(e))
            {
                ke.SuppressKeyPress = false;
            }
        }

        public static string AcertaLabel(string sTexto, string sChave, int nTamanho = 0)
        {
            int nTam;
            int tTexto;
            string sTemp;
            int sIndex;

            if (nTamanho == 0)
                nTam = 60;
            else
                nTam = nTamanho;

            tTexto = sTexto.Length;

            if (tTexto > nTam)
            {
                if (sChave != "")
                {
                    sTemp = sTexto.Substring(0, (sTexto.IndexOf(sChave) + 1));
                    sIndex = sTexto.Length - (nTam - (sTemp.Length + 3));
                    if (sIndex < 0)
                        sIndex = 0;
                    sTemp = string.Concat(sTemp, "...", sTexto.Substring(sIndex));
                }
                else
                {
                    sTemp = sTexto.Substring(0, 4);
                    sIndex = sTexto.Length - (nTam - (sTemp.Length + 3));
                    if (sIndex < 0)
                        sIndex = 0;
                    sTemp = string.Concat(sTemp, "...", sTexto.Substring(sIndex));
                }
            }
            else
                sTemp = sTexto;

            return sTemp;
        }

        public static string FormatarData(string numeros)
        {
            return Convert.ToUInt64(numeros).ToString(@"00\/00\/0000");
        }

        public static string FormatarHora(string numeros)
        {
            return Convert.ToUInt64(numeros).ToString(@"00\:00\:00");
        }

        public static string RemoverCaracteresEspeciais(string input)
        {
            input = RemoverAcentuacao(input);
            string pattern = @"[^\w\s]|[ºª]|[\\]";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            string result = rgx.Replace(input, replacement);
            return result;
        }

        public static bool IsIPPinging(string ip, int timeout = 1000)
        {
            if (!string.IsNullOrEmpty(ip) &&
                  IPAddress.TryParse(ip, out IPAddress iPAddressValid) &&
                  iPAddressValid != new IPAddress(0))
            {
                try
                {
                    Ping myPing = new Ping();
                    PingReply reply = myPing.Send(iPAddressValid, timeout);
                    if (reply.Status == IPStatus.Success)
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
            }
            else return false;
        }

        public static string IpByHostName(string hostName)
        {
            IPHostEntry host = Dns.GetHostEntry(hostName);
            IPAddress[] ipaddr = host.AddressList;
            // Loop through the IP Address array and add the IP address to IP List
            foreach (IPAddress addr in ipaddr)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    return addr.ToString();
                }
            }
            return "";
        }

        public static bool AtualizaODBC(string serverName)
        {
            RegistryKey regHandle;
            bool existe = false;
            try
            {
                RegistryKey reg = Registry.CurrentUser;

                string nomeOdbc = "PostgreSQLCartoes";

                string key = (string)(@"Software\ODBC\ODBC.INI\" + nomeOdbc); //cria no registro uma chave com os dados para a Fonte de Dados
                string key2 = (string)(@"Software\ODBC\ODBC.INI\ODBC Data Sources"); //Cria uma chave no Registro da Fonte de dados

                //verificar se fonte de dados existe
                string[] subKeys = Registry.CurrentUser.GetSubKeyNames();
                for (int i = 0; i < subKeys.Length; i++)
                {
                    if (subKeys[i] == nomeOdbc)
                    {
                        existe = true;
                        break;
                    }
                }

                if (existe == true)
                {
                    regHandle = reg.OpenSubKey(key);
                    regHandle.SetValue("Servername", serverName);
                    regHandle.Close();
                    reg.Close();
                }
                else
                {
                    //São atribuidos os valores para a Chave de Registro
                    regHandle = reg.CreateSubKey(key);
                    regHandle.SetValue("Driver", "C:\\Program Files\\psqlODBC\\1302\\bin\\psqlodbc35w.dll");
                    regHandle.SetValue("Description", "Banco de dados de conciliação de cartões do SuperSoft");
                    regHandle.SetValue("Servername", serverName);
                    regHandle.SetValue("CommLog", "0");
                    regHandle.SetValue("Debug", "0");
                    regHandle.SetValue("Fetch", "100");
                    regHandle.SetValue("UniqueIndex", "1");
                    regHandle.SetValue("UseDeclareFetch", "0");
                    regHandle.SetValue("UnknownSizes", "0");
                    regHandle.SetValue("TextAsLongVarchar", "1");
                    regHandle.SetValue("UnknownsAsLongVarchar", "0");
                    regHandle.SetValue("BoolsAsChar", "1");
                    regHandle.SetValue("Parse", "0");
                    regHandle.SetValue("MaxVarcharSize", "255");
                    regHandle.SetValue("MaxLongVarcharSize", "8190");
                    regHandle.SetValue("ExtraSysTablePrefixes", "");
                    regHandle.SetValue("Database", "cartoes");
                    regHandle.SetValue("Port", "5432");
                    regHandle.SetValue("Username", "postgres");
                    regHandle.SetValue("UID", "postgres");
                    regHandle.SetValue("Password", "Flavio2014");
                    regHandle.SetValue("ReadOnly", "0");
                    regHandle.SetValue("ShowOidColumn", "0");
                    regHandle.SetValue("FakeOidIndex", "0");
                    regHandle.SetValue("RowVersioning", "0");
                    regHandle.SetValue("ShowSystemTables", "0");
                    regHandle.SetValue("Protocol", "");
                    regHandle.SetValue("ConnSettings", "");
                    regHandle.SetValue("pqopt", "");
                    regHandle.SetValue("UpdatableCursors", "1");
                    regHandle.SetValue("LFConversion", "1");
                    regHandle.SetValue("TrueIsMinus1", "0");
                    regHandle.SetValue("BI", "0");
                    regHandle.SetValue("D6", "-101");
                    regHandle.SetValue("OptionalErrors", "0");
                    regHandle.SetValue("AB", "0");
                    regHandle.SetValue("ByteaAsLongVarBinary", "1");
                    regHandle.SetValue("UseServerSidePrepare", "1");
                    regHandle.SetValue("LowerCaseIdentifier", "0");
                    regHandle.SetValue("SSLmode", "disable");
                    regHandle.SetValue("KeepaliveTime", "-1");
                    regHandle.SetValue("KeepaliveInterval", "-1");
                    regHandle.SetValue("BatchSize", "100");
                    regHandle.SetValue("IgnoreTimeout", "0");
                    regHandle.SetValue("FetchRefcursors", "0");
                    regHandle.SetValue("XaOpt", "1");
                    regHandle.Close();

                    //São atribuídos os valores, para a Fonte de Dados
                    regHandle = reg.CreateSubKey(key2);
                    regHandle.SetValue(nomeOdbc, "PostgreSQL Unicode");
                    regHandle.Close();
                    reg.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ODBC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public static string ConvertStringEncoding(string txt, Encoding srcEncoding, Encoding dstEncoding)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return txt;
            }

            if (srcEncoding == null)
            {
                throw new System.ArgumentNullException(nameof(srcEncoding));
            }

            if (dstEncoding == null)
            {
                throw new System.ArgumentNullException(nameof(dstEncoding));
            }

            var srcBytes = srcEncoding.GetBytes(txt);
            var dstBytes = Encoding.Convert(srcEncoding, dstEncoding, srcBytes);
            return dstEncoding.GetString(dstBytes);
        }

        public static string AcertarAtuarizacao(string autorizacao)
        {
            autorizacao = autorizacao.Trim();
            if (IsNumerico(autorizacao))
            {
                return autorizacao.PadLeft(10, '0');
            }
            return autorizacao;
        }

        public static string AcertarNsu(string nsu)
        {
            if (IsNumerico(nsu))
            {
                return nsu.PadLeft(15, '0');
            }
            return nsu.Trim();
        }
    }
}
