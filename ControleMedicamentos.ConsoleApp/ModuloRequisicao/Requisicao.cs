using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using System;

namespace ControleMedicamentos.ConsoleApp
{
    public class Requisicao : EntidadeBase
    {

        public Requisicao(Paciente paciente, Medicamento medicamento, DateTime dataHora, bool aprovada)
        {
            Paciente = paciente;
            Medicamento = medicamento;
            DataHora = dataHora;
            Aprovada = aprovada;
        }

        public Paciente Paciente { get; set; }
        public Medicamento Medicamento { get; set; }
        public DateTime DataHora { get; set; }
        public bool Aprovada { get; set; }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Paciente: " + Paciente + Environment.NewLine +
                "Medicamento: " + Medicamento + Environment.NewLine +
                "Data e hora: " + DataHora + Environment.NewLine +
                "Aprovação: " + Aprovada + Environment.NewLine;
        }

    }
}

