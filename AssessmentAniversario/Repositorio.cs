using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;


namespace AssessmentAniversario
{
    class Repositorio
    {
        private static string ResgatarNomeDoArquivo()
        {
            var pastaDesktop = Environment.SpecialFolder.Desktop;

            string localDaPastaDesktop = Environment.GetFolderPath(pastaDesktop);
            string nomeDoArquivo = @"\AniversariantesDB.txt";

            return localDaPastaDesktop + nomeDoArquivo;
        }
        public static IEnumerable<Aniversariante> BuscarTodosAniversariantes()
        {
            string nomeDoArquivo = ResgatarNomeDoArquivo();

            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo))
            {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            string resultado = File.ReadAllText(nomeDoArquivo);

            string[] aniversariantes = resultado.Split(';');

            List<Aniversariante> aniversariantesList = new List<Aniversariante>();

            for (int i = 0; i < aniversariantes.Length - 1; i++)
            {
                string[] dadosDoAniversariante = aniversariantes[i].Split(',');

                string nome = dadosDoAniversariante[0];
                string sobrenome = dadosDoAniversariante[1];
                DateTime dataNascimento = Convert.ToDateTime(dadosDoAniversariante[2]);
                DateTime dataCadastro = Convert.ToDateTime(dadosDoAniversariante[3]);

                Aniversariante aniversariante = new Aniversariante(nome, sobrenome, dataNascimento, dataCadastro);

                aniversariantesList.Add(aniversariante);
            }

            return aniversariantesList;
        }
        public static void CadastrarAniversariante(Aniversariante aniversariante)
        {
            string nomeDoArquivo = ResgatarNomeDoArquivo();

            string formato = $"{aniversariante.Nome},{aniversariante.Sobrenome},{aniversariante.DataNascimento.ToString()},{aniversariante.DataCadastro.ToString()};";

            File.AppendAllText(nomeDoArquivo, formato);
        }
        public static IEnumerable<Aniversariante> BuscarTodosAniversariantes(string nome)
        {
             https://docs.microsoft.com/pt-br/dotnet/csharp/programming-guide/concepts/linq/
            return (from x in BuscarTodosAniversariantes()
                    where x.Nome.Contains(nome)
                    orderby x.Nome
                    select x);
        }
        public static IEnumerable<Aniversariante> BuscarTodosAniversariantes(DateTime dataNascimento)
        {
             https://docs.microsoft.com/pt-br/dotnet/csharp/programming-guide/concepts/linq/
            return (from x in BuscarTodosAniversariantes()
                    where x.DataNascimento == dataNascimento
                    orderby x.Nome
                    select x);
        }

        public static void Excluir(string nome)
        {
            var todosOsAniversariantes = BuscarTodosAniversariantes();
            List<Aniversariante> aniversariantesUpdate = new List<Aniversariante>();
            foreach (var aniversariante in todosOsAniversariantes)
            {
                if (nome != aniversariante.Nome)
                {
                    aniversariantesUpdate.Add(aniversariante);
                }
                else
                {
                }
            }
            RefazerArquivo(aniversariantesUpdate);

        }
        public static void RefazerArquivo(List<Aniversariante> aniversariantesUpdate)
        {
            string nomeDoArquivo = ResgatarNomeDoArquivo();
            File.Delete(ResgatarNomeDoArquivo());
            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo))
            {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }
            foreach (var aniversariante in aniversariantesUpdate)
            {
                CadastrarAniversariante(aniversariante);
            }

        }

    }
}
