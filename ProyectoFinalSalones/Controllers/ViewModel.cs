using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinalSalones.Controllers
{
    public class ViewModel
    {
        public IEnumerable<Salone> salones { get; set; }
        public IEnumerable<Transaccione> transaccion { get; set; }
    }
}