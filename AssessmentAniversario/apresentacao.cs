using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace AssessmentAniversario
{
    class apresentacao
    {
        public static void MenuSistema()
        {
            Console.WriteLine("Selecione uma operação abaixo:");
            Console.WriteLine("1 - Cadastrar novo aniversariante.");
            Console.WriteLine("2 - Editar aniversariante.");
            Console.WriteLine("3 - Excluir aniversariante..");
            Console.WriteLine("4 - Aniversariantes cadastrados no sistema.");
            Console.WriteLine("5 - Pesquisar aniversariante.");
            Console.WriteLine("0 - Sair");
            Console.WriteLine(" ");

            string OperacaoSelecionada = Console.ReadLine();

            switch (OperacaoSelecionada)
            {
                case "1":
                    CadastrarAniversariante();
                    break;
                case "2":
                    EditarAniversariante(); 
                    break;
                case "3":
                    ExcluirAniversariante(); 
                    break;
                case "4":
                    AniversariantesCadastrados();
                    break;
                case "5":
                    MenuPesquisa();
                    break;
                case "0":
                    Console.WriteLine("Até mais...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Operação Invalida!");
                    MenuSistema(); 
                    break;
            }
            
            Console.WriteLine("Pressione qualquer tecla para continuar..");
            Console.ReadKey();
            Console.Clear();
        }

        public static void CadastrarAniversariante()
        {         
            Console.WriteLine("Nome?");
            string nome = Console.ReadLine();
            Console.WriteLine("Sobrenome?");
            string sobrenome = Console.ReadLine();
            Console.WriteLine("Data de nascimento (dd/mm/aaaa)?");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine());

            Aniversariante aniversariante = new Aniversariante();
            aniversariante.Nome = nome;
            aniversariante.Sobrenome = sobrenome;
            aniversariante.DataNascimento = dataNascimento;
            aniversariante.DataCadastro = DateTime.Now;

            Repositorio.CadastrarAniversariante(aniversariante);
            Console.WriteLine("Aniversariante cadastrado com sucesso!");
            Console.Clear();
            MenuSistema();
        }

        public static void EditarAniversariante()
        {
            Console.WriteLine("Qual o nome do aniversariante que você deseja editar?");
            string nome = Console.ReadLine();

            var aniversariantesEncontrados = Repositorio.BuscarTodosAniversariantes(nome);

            int todosaniversariantesEncontrados = aniversariantesEncontrados.Count();

            if (todosaniversariantesEncontrados > 0)
            {
                Console.WriteLine("Aniversariantes encontrados no sistema:");
    
                foreach (var aniversariante in aniversariantesEncontrados)
                {
                    Console.WriteLine($"{aniversariante.Nome} {aniversariante.Sobrenome} nascido em: {aniversariante.DataNascimento:dd/MM/yyyy} ");
                }
            }
            else
            {
                Console.WriteLine("Não foi encontrado nenhum participante com este nome!");
                Console.WriteLine("Tente Novamente:");
                Console.WriteLine("");
                EditarAniversariante();
            }

            Console.WriteLine("Agora digite o sobrenome do aniversariante que voce deseja editar.");
            string sobrenomeAniver = Console.ReadLine();

            foreach (var aniversariante in aniversariantesEncontrados)
            {
                if (aniversariante.Sobrenome == sobrenomeAniver)
                {
                    Repositorio.Excluir(aniversariante.Nome);

                }
                else
                {
                    Console.WriteLine("Não foi encontrado nenhum participante com este sobrenome!");
                    Console.WriteLine("Tente Novamente:");
                    Console.WriteLine("");
                    EditarAniversariante();
                }
            }

            Console.WriteLine("Agora altere as informações deste amiversariante.");
            Console.WriteLine(" ");
            Console.WriteLine("Novo Nome?");
            string nomeNovo = Console.ReadLine();
            Console.WriteLine("Novo Sobrenome?");
            string sobrenomeNovo = Console.ReadLine();
            Console.WriteLine("Nova data de aniverário (dd/mm/aaaa)?");
            DateTime dataNova = DateTime.Parse(Console.ReadLine());

            Aniversariante aniversarianteEditado = new Aniversariante();
            aniversarianteEditado.Nome = nomeNovo;
            aniversarianteEditado.Sobrenome = sobrenomeNovo;
            aniversarianteEditado.DataNascimento = dataNova;
            aniversarianteEditado.DataCadastro = DateTime.Now;

            Repositorio.CadastrarAniversariante(aniversarianteEditado);
            MenuSistema();
        }

        public static void ExcluirAniversariante()
        {
            Console.WriteLine("Qual o nome do aniversariante que você deseja excluir?");
            string nome = Console.ReadLine();

            var aniversariantesEncontrados = Repositorio.BuscarTodosAniversariantes(nome);

            int todosaniversariantesEncontrados = aniversariantesEncontrados.Count();

            if (todosaniversariantesEncontrados > 0)
            {
                Console.WriteLine("Aniversariantes encontrados no sistema:");
                Console.WriteLine(" ");
                
                foreach (var aniversariante in aniversariantesEncontrados)
                {
                    Console.WriteLine($"{aniversariante.Nome} {aniversariante.Sobrenome} nascido em: {aniversariante.DataNascimento:dd/MM/yyyy} ");
                }
            }
            else
            {
                Console.WriteLine("Não foi encontrado nenhum participante com este sobrenome!");
                Console.WriteLine("Tente Novamente:");
                Console.WriteLine("");
                ExcluirAniversariante();
            }

            Console.WriteLine("Agora digite o sobrenome do aniversariante que voce deseja excluir.");
            string sobrenomeExcluir = Console.ReadLine();

            foreach (var aniversariante in aniversariantesEncontrados)
            {
                if (aniversariante.Sobrenome == sobrenomeExcluir)
                {
                    Repositorio.Excluir(aniversariante.Nome);
                }
            }
            MenuSistema();
        }

        public static void AniversariantesCadastrados()
        {
            Console.WriteLine("Aniversariantes Cadastrados No Sistema:");
            Console.WriteLine(" ");
            foreach (var aniversariante in Repositorio.BuscarTodosAniversariantes())
            {

                Console.WriteLine($"{aniversariante.Nome} {aniversariante.Sobrenome} nascido em: {aniversariante.DataNascimento:dd/MM/yyyy} ");
                int dia = aniversariante.DataNascimento.Day;
                int mes = aniversariante.DataNascimento.Month;
                if (DateTime.Today.Month < aniversariante.DataNascimento.Month)
                {
                    DateTime proximoNiver = new DateTime(DateTime.Today.Year, mes, dia);

                    double resultado = proximoNiver.Subtract(DateTime.Today).TotalDays;

                    Console.WriteLine("Faltam " + resultado + " dias para o seu próximo aniversário!");
                    Console.WriteLine(" ");
                }
                else
                {
                    DateTime proximoNiver = new DateTime(DateTime.Today.Year + 1, mes, dia);

                    double resultado = proximoNiver.Subtract(DateTime.Today).TotalDays;

                    Console.WriteLine("Faltam " + resultado + " dias para o seu próximo aniversário!");
                    Console.WriteLine(" ");
                }

                MenuSistema();
            }

        }
        public static void MenuPesquisa()
        {
            Console.WriteLine("Selecione como deseja pesquisar um aniversáriante.");
            Console.WriteLine("1 - Pesquisar aniversariante pelo nome.");
            Console.WriteLine("2 - Pesquisar aniversariante pela data de nascimento.");
            string PesquisaSelecionada = Console.ReadLine();
            Console.Clear();

            switch (PesquisaSelecionada)
            {
                case "1":
                    PesquisarPeloNome();
                    break;
                case "2":
                    PesquisaPelaData();
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
                    
            }
        }
        
        public static void PesquisarPeloNome()
        {
            Console.WriteLine("Qual é o nome do aniversariante?");
            string nome = Console.ReadLine();

            var aniversariantesEncontrados = Repositorio.BuscarTodosAniversariantes(nome);

            int todosaniversariantesEncontrados = aniversariantesEncontrados.Count();

            if (todosaniversariantesEncontrados > 0)
            {
                foreach (var aniversariante in aniversariantesEncontrados)
                {
                    Console.WriteLine($"{aniversariante.Nome} {aniversariante.Sobrenome} nascido em: {aniversariante.DataNascimento:dd/MM/yyyy} ");
                }
            }
            else
            {
                Console.WriteLine("Não foi encontrado nenhum participante com este nome!");
            }

            MenuSistema();
        }

        public static void PesquisaPelaData()
        {
            Console.WriteLine("Qual é a data de aniversario (dd/mm/aaaa)?:");
            DateTime data = DateTime.Parse(Console.ReadLine());
            var aniversariantesEncontrados = Repositorio.BuscarTodosAniversariantes(data);

            int todosaniversariantesEncontrados = aniversariantesEncontrados.Count();

            if (todosaniversariantesEncontrados > 0)
            {
                foreach (var aniversariante in aniversariantesEncontrados)
                {
                    Console.WriteLine($"{aniversariante.Nome} {aniversariante.Sobrenome} nascido em: {aniversariante.DataNascimento:dd/MM/yyyy} ");
                }

            }
            else
            {
                Console.WriteLine("Não foi encontrado nenhum participante nascido nesta data!");
            }
            MenuSistema();
        }

        public static void AniversarioDia()
        {
            Console.WriteLine("É aniversario de alguem hoje?");
            foreach (var aniversariante in Repositorio.BuscarTodosAniversariantes())
            {
                if (DateTime.Now.Month == aniversariante.DataNascimento.Month && DateTime.Now.Day == aniversariante.DataNascimento.Day)
                {
                    Console.WriteLine("Aniversariante do dia:");
                    Console.WriteLine($"{aniversariante.Nome} {aniversariante.Sobrenome} nascido em: {aniversariante.DataNascimento:dd/MM/yyyy} está fazendo {((DateTime.Now - aniversariante.DataNascimento).Days / 30 / 12) - 1} anos hoje!");
                    Console.WriteLine(" ");
                }
                else
                {
                }

            }
        }
    }
}

