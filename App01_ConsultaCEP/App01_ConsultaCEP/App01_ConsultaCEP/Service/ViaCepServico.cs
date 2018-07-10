using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using App01_ConsultaCEP.Model;
using System.Threading.Tasks;
using System.Net.Http;

namespace App01_ConsultaCEP.Service
{
    class ViaCEPServico
    {
        private static string EnderecoURL = "http://viacep.com.br/ws/{0}/json";

        public static Endereco BuscarEnderecoViaCEP(string cep)
        {
            string NovaURL = string.Format(EnderecoURL, cep);

            Endereco end;
            string result;

            result =  SicronoCEP(NovaURL);

            end = JsonConvert.DeserializeObject<Endereco>(result);

            if (end.cep == null) return null;

            return end;
        }

        private static string SicronoCEP(string uri)
        {
            WebClient wc = new WebClient();
            string conteudoJson = wc.DownloadString(uri);
            return conteudoJson;
        }

    }
}
