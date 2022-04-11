using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente : EntidadeBase
    {
        public Paciente(string nome, string cpf, string telefone)
        {
            Nome = nome;
            Cpf = cpf;
            Telefone = telefone;
        }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "Cpf: " + Cpf + Environment.NewLine +
                "Telefone: " + Telefone + Environment.NewLine;
        }
    }
}
