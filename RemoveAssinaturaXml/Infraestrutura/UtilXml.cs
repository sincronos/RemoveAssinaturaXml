using RemoveAssinaturaXml.Model;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace RemoveAssinaturaXml.Infraestrutura
{
    public static class UtilXml
    {

        private static readonly XmlWriterSettings WriterSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false, NewLineHandling = NewLineHandling.None, NewLineChars = string.Empty, NewLineOnAttributes = false };
        private static readonly XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "http://www.portalfiscal.inf.br/nfe") });

        public static T DeserializeObject<T>(string fileName)
        {

            // try
            //{
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream(fileName, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            T i;

            i = (T)serializer.Deserialize(reader);
            fs.Close();

            return i;

            //}
            //catch (Exception)
            //{
            //    Exception ex = new Exception("XML inconcistente!");
            //    throw ex;
            //}
        }

        public static void Serialize<T>(object arq, FileInfo file)
        {
            try
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
                using (StringWriter sww = new StringWriter())
                using (XmlWriter writer = XmlWriter.Create(sww, WriterSettings))
                {
                    xsSubmit.Serialize(writer, arq, Namespaces);

                    using (StreamWriter outputFile = new StreamWriter(file.FullName, false))
                    {

                        var s = sww.ToString();
                        var s2 = s.Replace("<motDesICMS />", "");

                        Regex re = new Regex("\r\n$");
                        re.Replace(s2, "");

                        outputFile.WriteLine(s2);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("ERRO ao converter XML.");
            }
        }

        internal static StatusModel RemoveAssinatura(ConfigFieldsAjust camposAjust, FileInfo arq, DateTime dtInicial, DateTime dtFinal)
        {
            var status = new StatusModel();

            try
            {
                TNFe nfe = DeserializeObject<TNFe>(arq.FullName);
                nfe.Signature = null;

                var cod103 = new TNFeInfNFeDetImpostoICMS();
                cod103.Item = new TNFeInfNFeDetImpostoICMSICMSSN102() { orig = 0, CSOSN = TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item103 };

                var cod400 = new TNFeInfNFeDetImpostoICMS();
                cod400.Item = new TNFeInfNFeDetImpostoICMSICMSSN102() { orig = 0, CSOSN = TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item400 };

                //if (nfe.infNFe.emit.IE != null)
                //{
                //    if (nfe.infNFe.emit.IE == "86623366")
                //    {
                //        nfe.infNFe.emit.IE = "87199037";
                //    }
                //}

                if (nfe.infNFe.dest != null)
                {
                    if (nfe.infNFe.dest.enderDest.fone != null)
                    {
                        nfe.infNFe.dest.enderDest.fone = UString.SoNumero(nfe.infNFe.dest.enderDest.fone);
                    }

                    if (nfe.infNFe.dest.enderDest.xBairro == null)
                    {
                        nfe.infNFe.dest.enderDest.xBairro = "Centro";
                    }

                    if (nfe.infNFe.dest.enderDest.xLgr == null || nfe.infNFe.dest.enderDest.xLgr.Length == 1)
                    {
                        nfe.infNFe.dest.enderDest.xLgr = "Não informado";
                    }

                    if (nfe.infNFe.dest.enderDest.nro == null)
                    {
                        nfe.infNFe.dest.enderDest.nro = "0";
                    }

                    if (nfe.infNFe.dest.enderDest.CEP != null)
                    {
                        if (nfe.infNFe.dest.enderDest.CEP.Length != 8)
                        {
                            nfe.infNFe.dest.enderDest.CEP = "28100000";
                        }
                    }
                    else
                    {
                        nfe.infNFe.dest.enderDest.CEP = "28100000";
                    }

                }

                if ( nfe.infNFe.infAdic != null)
                {
                    nfe.infNFe.infAdic.infCpl = Regex.Replace(nfe.infNFe.infAdic.infCpl, @"\t|\n|\r", "");
                }


                foreach (var prod in nfe.infNFe.det)
                {
                    if (camposAjust.CbCEST && prod.prod.CEST != null)
                        prod.prod.CEST = prod.prod.CEST.ToString().PadLeft(7, '0');

                    if (camposAjust.CbNCM)
                    {
                        if (prod.prod.NCM != null)
                        {
                            if (prod.prod.NCM.Length != 8) { prod.prod.NCM = "00000000"; }
                        }
                        else
                        {
                            prod.prod.NCM = "00000000";
                        }
                    }

                    if (camposAjust.Rej384 && prod.imposto.Items != null)
                    {

                        if (prod.imposto.Items[0].GetType() == typeof(TNFeInfNFeDetImpostoICMS))
                        {
                            TNFeInfNFeDetImpostoICMS x = prod.imposto.Items[0] as TNFeInfNFeDetImpostoICMS;
                            TNFeInfNFeDetImpostoICMSICMSSN102 s = x.Item as TNFeInfNFeDetImpostoICMSICMSSN102;

                            if (s != null)
                            {
                                if (s.GetType() == typeof(TNFeInfNFeDetImpostoICMSICMSSN102))
                                {

                                    if (s.CSOSN == TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item103)
                                    {
                                        var x2 = new TNFeInfNFeDetImpostoICMS();
                                        x2.Item = new TNFeInfNFeDetImpostoICMSICMSSN102() { orig = 0, CSOSN = TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item102 };

                                        prod.imposto.Items[0] = x2;
                                    }

                                    if (s.CSOSN == TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item400)
                                    {
                                        var x2 = new TNFeInfNFeDetImpostoICMS();
                                        x2.Item = new TNFeInfNFeDetImpostoICMSICMSSN102() { orig = 0, CSOSN = TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item300 };

                                        prod.imposto.Items[0] = x2;
                                    }
                                }
                            }
                        }
                    }

                    //if (prod.imposto.Items == null)
                    //{
                    //    var novoImposto = new TNFeInfNFeDetImpostoICMS();
                    //    novoImposto.Item = new TNFeInfNFeDetImpostoICMSICMSSN102() { orig = 0, CSOSN = TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item102 };
                    //    prod.imposto.Items = new object[1];
                    //    prod.imposto.Items[0] = novoImposto;
                    //}


                    if (prod.prod.uCom == ",")
                    {
                        prod.prod.uCom = "UN";
                        prod.prod.uTrib = "UN";
                    }

                    if (prod.imposto.Items[0].GetType() == typeof(TNFeInfNFeDetImpostoICMS))
                    {
                        TNFeInfNFeDetImpostoICMS x = prod.imposto.Items[0] as TNFeInfNFeDetImpostoICMS;
                        var s = x.Item;
                        if (s.GetType() == typeof(TNFeInfNFeDetImpostoICMSICMS40))
                        {
                            var x2 = new TNFeInfNFeDetImpostoICMS();
                            x2.Item = new TNFeInfNFeDetImpostoICMSICMS40() { motDesICMS = TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Default };

                            prod.imposto.Items[0] = x2;
                        }
                    }
                }

                if (Convert.ToDateTime(nfe.infNFe.ide.dhEmi).Date >= dtInicial && Convert.ToDateTime(nfe.infNFe.ide.dhEmi).Date <= dtFinal)
                {
                    Serialize<TNFe>(nfe, arq);
                }

                status.DtEmissao = nfe.infNFe.ide.dhEmi.ToString();
                status.NumNfce = nfe.infNFe.ide.nNF.ToString();
                status.Chave = nfe.infNFe.Id;
                status.Valor = nfe.infNFe.total.ICMSTot.vNF.ToString();

                if (Convert.ToDateTime(nfe.infNFe.ide.dhEmi).Date >= dtInicial && Convert.ToDateTime(nfe.infNFe.ide.dhEmi).Date <= dtFinal)
                    status.StatusProcesso = "OK";
                else
                    status.StatusProcesso = "Não processado. Fora do período.";
            }
            catch (Exception e)
            {
                status.Chave = arq.FullName;
                status.StatusProcesso = e.Message;
            }

            return status;
        }
    }
}
