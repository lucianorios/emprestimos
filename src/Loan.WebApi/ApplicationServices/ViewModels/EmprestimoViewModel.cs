using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.ApplicationServices.ViewModels
{
    public class EmprestimoViewModel
    {
        public long Id { get; set; }
        public DateTime Data { get; set; }
        public DateTime? Devolucao { get; set; }
        public PessoaViewModel Pessoa { get; set; }
        public ItemViewModel Item { get; set; }
        public int DiasDecorridos { get
            {
                var dataFim = this.Devolucao ?? DateTime.Now;

                return (int)dataFim.Subtract(this.Data).TotalDays;
            } }
    }
}
