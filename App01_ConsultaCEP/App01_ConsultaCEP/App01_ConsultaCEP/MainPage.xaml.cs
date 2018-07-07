using App01_ConsultaCEP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App01_ConsultaCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            ButaoPesquisar.Clicked += BuscarCEP;
		}

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = CEPEntry.Text.Trim();

            if (isValid(cep)) {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null)
                    {
                        LabelResultado.Text = string.Format("Endereço: {0}, {1}, {2}, {3}",
                                                            end.logradouro, end.bairro, end.localidade, end.uf);
                    }
                    else
                    {
                        DisplayAlert("Erro", "O endereço não foi encontrado para o CEP informado: " + cep, "Ok");
                    }
                }
                catch(Exception e)
                {
                    DisplayAlert("Erro Crítico", e.Message, "Ok");
                }
            }
        }

        private bool isValid(string cep)
        {
            bool result = true;
            int NumeroCaracteres = 8;
            int NumeroCaracteresComHifen = 9;
            int HifenPosicao = 5;

            if(cep.Length != NumeroCaracteres)
            {
                if (cep.Length == NumeroCaracteresComHifen && cep[HifenPosicao] != '-')
                {
                    result = false;
                    DisplayAlert("Erro", "CEP invalido! O CEP deve conter 8 caracteres", "Ok");
                }
                cep = cep.Remove(HifenPosicao);
            }
            int cepNumber = 0;
            if (!int.TryParse(cep, out cepNumber))
            {
                result = false;
                DisplayAlert("Erro", "CEP invalido! O CEP deve conter somente numeros", "Ok");

            }

            return result;
        }
	}
}
