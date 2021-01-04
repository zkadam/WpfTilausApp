using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTilausApp
{
    class cbPairAsiakas
    {
        public string asiakasNimi { get; set; }
        public int asiakasNro { get; set; }
        public string asiakasOsoite { get; set; }

        public cbPairAsiakas(string AsiakasNimi, int AsiakasNro, string AsiakasOsoite)
        {
            asiakasNimi = AsiakasNimi;
            asiakasNro = AsiakasNro;
            asiakasOsoite = AsiakasOsoite;
        }
    }
}
