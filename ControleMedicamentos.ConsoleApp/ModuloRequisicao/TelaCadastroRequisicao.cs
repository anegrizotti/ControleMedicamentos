using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class TelaCadastroRequisicao : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Requisicao> repositorioRequisicao;
        private readonly IRepositorio<Paciente> repositorioPaciente;
        private readonly IRepositorio<Medicamento> repositorioMedicamento;
        private readonly TelaCadastroPaciente telaCadastroPaciente;
        private readonly TelaCadastroMedicamento telaCadastroMedicamento;
        private readonly Notificador notificador;

        public TelaCadastroRequisicao(
            IRepositorio<Requisicao> repositorioRequisicao,
            IRepositorio<Paciente> repositorioPaciente,
            IRepositorio<Medicamento> repositorioMedicamento,
            TelaCadastroPaciente telaCadastroPaciente,
            TelaCadastroMedicamento telaCadastroMedicamento,
            Notificador notificador) : base("Cadastro de Requisicao")
        {
            this.repositorioRequisicao = repositorioRequisicao;
            this.repositorioPaciente = repositorioPaciente;
            this.repositorioMedicamento = repositorioMedicamento;
            this.telaCadastroPaciente = telaCadastroPaciente;
            this.telaCadastroMedicamento = telaCadastroMedicamento;
            this.notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Requisição");

            Requisicao novaRequisicao = ObterRequisicao();

            repositorioRequisicao.Inserir(novaRequisicao);

            notificador.ApresentarMensagem("Requisição cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Requisição");

            bool temRequisicoesCadastradas = VisualizarRegistros("Pesquisando");

            if (temRequisicoesCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma requisição cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            Requisicao requisicaoAtualizada = ObterRequisicao();

            bool conseguiuEditar = repositorioRequisicao.Editar(numeroRequisicao, requisicaoAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Requisição editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Requisição");

            bool temRequisicoesRegistradas = VisualizarRegistros("Pesquisando");

            if (temRequisicoesRegistradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma requisição cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioRequisicao.Excluir(numeroRequisicao);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Requisição excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Requisições");

            List<Requisicao> requisicoes = repositorioRequisicao.SelecionarTodos();

            if (requisicoes.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhuma requisição disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Requisicao requisicao in requisicoes)
                Console.WriteLine(requisicao.ToString());

            Console.ReadLine();

            return true;
        }

        private Requisicao ObterRequisicao()
        {
            Paciente pacienteSelecionado = ObtemPaciente();

            Medicamento medicamentoSelecionado = ObtemMedicamento();

            DateTime dataHora = DateTime.Now;

            bool aprovacao = true;

            return new Requisicao(pacienteSelecionado, medicamentoSelecionado, dataHora, aprovacao);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da requisição que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioRequisicao.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID da requisição não foi encontrada, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        private Paciente ObtemPaciente()
        {
            bool temPacienteDisponivel = telaCadastroPaciente.VisualizarRegistros("Pesquisando");

            if (!temPacienteDisponivel)
            {
                notificador.ApresentarMensagem("Não há nenhum paciente cadastrado.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o id do paciente: ");
            int idPaciente = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistro(idPaciente);

            return pacienteSelecionado;
        }

        private Medicamento ObtemMedicamento()
        {
            bool temMedicamentoDisponivel = telaCadastroMedicamento.VisualizarRegistros("Pesquisando");

            if (!temMedicamentoDisponivel)
            {
                notificador.ApresentarMensagem("Não há nenhum medicamento cadastrado.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o id do medicamento: ");
            int idMedicamento = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Medicamento medicamentoSelecionado = repositorioMedicamento.SelecionarRegistro(idMedicamento);

            return medicamentoSelecionado;
        }

    }
}
