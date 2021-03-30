using System;
using System.IO;
using System.Threading;

namespace EditorDeTextos
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {

            short option = 0;

            Console.Clear();

            Console.WriteLine("O que você deseja fazer?");
            Console.WriteLine("1 - Abrir arquivo");
            Console.WriteLine("2 - Criar novo arquivo");
            Console.WriteLine("0 - Sair");

            try
            {
                option = short.Parse(Console.ReadLine());
            }
            catch
            {
                int tempoSeg = 3;

                while (tempoSeg > 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Não foi possível identificar o que você quer... retornando ao menu novamente em {tempoSeg} segundos...");
                    tempoSeg--;
                    Thread.Sleep(1000);
                }
                Menu();
            }

            switch (option)
            {
                case 0:
                    System.Environment.Exit(0);
                    break;
                case 1:
                    Abrir();
                    break;
                case 2:
                    Editar();
                    break;
                default:
                    Menu();
                    break;
            }
        }

        static void Abrir()
        {
            Console.Clear();
        tentarNovamente:
            Console.WriteLine("[Leitura] Qual o caminho do arquivo?");


            try
            {
                string path = Console.ReadLine();

                using (var file = new StreamReader(path))
                {
                    string texto = file.ReadToEnd();
                    Console.Clear();
                    Console.WriteLine(texto);
                    Console.WriteLine("[LEITURA FINALIZADA] Deseja voltar ao menu? pressione qualquer tecla...");
                }
                Console.ReadKey();
                Menu();
            }
            catch (Exception)
            {
                Console.WriteLine("[ERRO] Desculpe seu caminho ou arquivo está errado...");
                Console.WriteLine(@"[ERRO] Tente algo como: C:\balta\projeto\arquivo.txt");

                goto tentarNovamente;
            }

        }

        static void Editar()
        {
            int limiteTotal = 500;
            string[] texto = new string[limiteTotal];
            int linhaAtual = 1;

            Console.Clear();
            Console.WriteLine("[CRIAR] Digite seu texto abaixo. (ESC para salvar)");
            Console.WriteLine("*O texto é limitado a 500 linhas.*");

        novaLinha:
            Console.WriteLine($"↘ Você está escrevendo na {linhaAtual}ª linha agora. ↙");
            texto[linhaAtual - 1] += Console.ReadLine();
            linhaAtual++;

            do
            {
                Console.WriteLine($"Deseja adicionar uma nova linha? pressione qualquer tecla para sim. - (ESC para salvar)");
                if (Console.ReadKey().Key != ConsoleKey.Escape && linhaAtual <= 500)
                {
                    Console.Clear();
                    for (int i = 0; i < linhaAtual - 1; i++)
                    {
                        Console.WriteLine($"[]====> Escrita da {i + 1}ª linha <====[]");
                        Console.WriteLine(texto[i]);
                    }

                    Console.WriteLine(Environment.NewLine);
                    goto novaLinha;
                }
                else
                {
                    goto perguntarSeQuerSalvar;
                }
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape && linhaAtual <= 500);



        perguntarSeQuerSalvar:
            Console.WriteLine("CCarregando..."); // O console come 1 caracter por conta do readkey, então vamos colocar dois C para burlar isso.
            string textoASalvar = "";

            for (int i = 0; i < linhaAtual - 1; i++)
            {
                textoASalvar += i > 0 ? "\n" + texto[i] : texto[i];
            }
            Thread.Sleep(1500);
            Console.Clear();
            Console.WriteLine("==> Pressione qualquer tecla para visualizar o texto... <==");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("---------------------------");
            Console.WriteLine($"{textoASalvar}");
            Console.WriteLine("---------------------------");
            Console.WriteLine("1 - Para salvar /// 2- Refazer o texto");

            try
            {
                int escolha = int.Parse(Console.ReadLine());

                switch (escolha)
                {
                    case 1:
                        Salvar(textoASalvar);
                        break;
                    case 2:
                        Editar();
                        break;
                    default:
                        goto perguntarSeQuerSalvar;
                }
            }
            catch
            {
                goto perguntarSeQuerSalvar;
            }

        }


        static void Salvar(string text)
        {
            Console.Clear();
        tentarNovamente:
            Console.WriteLine("[SALVAR] Qual nome você quer dar para este arquivo de texto?\n[OBSERVE] O arquivo será salvo como TXT.");

            try
            {
                var path = Console.ReadLine();

                using (var file = new StreamWriter(path + ".txt"))
                {
                    file.Write(text);
                    Console.WriteLine($"Arquivo foi salvo no caminho: '{path}.txt'.");
                    Console.WriteLine($"Pressione qualquer tecla para voltar ao menu...");
                }
                Console.ReadKey();
                Menu();
            }
            catch
            {
                Console.WriteLine(@"[ERRO] Desculpe seu caminho está errado...\nTente algo como: C:\balta\projeto\NomeDoArquivo.txt");
                goto tentarNovamente;
            }

        }


    }
}
