using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CampoMinado
{
    /*INTEGRANTES do GRUPO: Ana Paula Cardoso, Barbara Giovana, Hayanne Santos, Jader Santos.
    Data da Criação: 15/06/2023      Última alteração: 
    Objetivo do Programa: */

    private int[,] tabuleiro;
    private bool[,] tabuleiroRevelado;
    private int linhas;
    private int colunas;
    private int totalBombas;

    static void Main(string[] args)
    {        
        // Criação do objeto CampoMinado e chamada do método IniciarJogo()
        CampoMinado campoMinado = new CampoMinado();
        campoMinado.IniciarJogo();
    }

    // Método para iniciar o jogo
    public void IniciarJogo()
    {
        //chamada do metodo para ler arquivo texto
        lerArquivoTexto();

        //inicializando tabuleiro com linhas e colunas em branco
        tabuleiro = new int[linhas, colunas];
        tabuleiroRevelado = new bool[linhas, colunas];

        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                tabuleiro[i, j] = 0;
                tabuleiroRevelado[i, j] = false;
            }
        }

        //chamada do metodo para preecnher os numeros de bombas adjacentes
        PreencherNumeros();

        //chamada do metodo para posicionar as bombas
        PreencherBombas();            
      

        while (true)
        {
            ImprimirTabuleiro();
            Console.WriteLine("Digite a linha: ");
            int linha = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite a coluna: ");
            int coluna = int.Parse(Console.ReadLine());


            if (tabuleiroRevelado[linha, coluna])
            {
                Console.WriteLine("Essa célula já foi descoberta. Tente novamente.");
                continue;
            }

            if (VerificarDerrota())
            {
                Console.WriteLine("Você perdeu! Acertou uma bomba.");
                break;
            }
           
            RevelarPosicao(linha, coluna);

            if (VerificarVitoria())
            {
                Console.WriteLine("Parabéns! Você venceu!");
                break;
            }
        }
    }

   

    private void lerArquivoTexto()
    {
        string filePath = @"C:\TrabalhoPratico\arquivo.txt";

      


    }



    // Método para preencher o tabuleiro com bombas aleatórias
    private void PreencherBombas()
    {
        Random rd = new Random();

        for (int i = 0; i < totalBombas; i++)
        {
            int linha;
            int coluna;

            do
            {
                linha = rd.Next(linhas);
                coluna = rd.Next(colunas);
            } while (tabuleiro[linha, coluna] == '*');

            tabuleiro[linha, coluna] = '*';
        }
    }

    // Método para mostrar o tabuleiro na tela
    private void ImprimirTabuleiro()
    {
        Console.WriteLine("Tabuleiro:");

        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                if (tabuleiroRevelado[i, j])
                {
                    Console.Write(tabuleiro[i, j]);
                }
                else
                {
                    Console.Write(".");
                }

                Console.Write(" ");
            }

            Console.WriteLine();
        }
    }

    // Método para preencher os números ao redor das bombas
    private void PreencherNumeros()
    {
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                if (tabuleiro[i, j] == '*')
                {
                    continue;
                }

                int count = 0;

                for (int x = i - 1; x <= i + 1; x++)
                {
                    for (int y = j - 1; y <= j + 1; y++)
                    {
                        if (x >= 0 && x < linhas && y >= 0 && y < colunas && tabuleiro[x, y] == '*')
                        {
                            count++;
                        }
                    }
                }

                tabuleiro[i, j] = count;
            }
        }
    }


    private void RevelarPosicao(int linha, int coluna)
    {
        tabuleiroRevelado[linha, coluna] = true;

        if (tabuleiro[linha, coluna] == 0)
        {
            DescobrirCelulasAdjacentes(linha, coluna);
        }
    }

    private void DescobrirCelulasAdjacentes(int linha, int coluna)
    {
        for (int i = linha - 1; i <= linha + 1; i++)
        {
            for (int j = coluna - 1; j <= coluna + 1; j++)
            {
                if (i >= 0 && i < linhas && j >= 0 && j < colunas && !tabuleiroRevelado[i, j])
                {
                    RevelarPosicao(i, j);
                }
            }
        }
    }

    // Método para verificar se o jogador ganhou o jogo
    private bool VerificarVitoria()
    {
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                if (!tabuleiroRevelado[i, j] && tabuleiro[i, j] != '*')
                {
                    return false;
                }
            }
        }

        return true;
    }

    // Método para verificar se o jogador perdeu o jogo
    private bool VerificarDerrota()
    {
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                if (tabuleiroRevelado[i, j] && tabuleiro[i, j] == '*')
                {
                    return true;
                }
            }
        }

        return false;
    }
}







