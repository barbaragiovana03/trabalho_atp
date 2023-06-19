using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CampoMinado
{
    /*INTEGRANTES do GRUPO: Ana Paula Cardoso, Barbara Giovana, Hayanne Santos.
      Objetivo do Programa: Descobrir todas as células que nao tenha bomba.
      Data da Criação: 15/06/2023      Última alteração:                  */

    private int[,] tabuleiro;
    private bool[,] tabuleiroRevelado;
    private int linhas;
    private int colunas;
    private int totalBombas;
    private string nome;

    static void Main(string[] args)
    {
        //Criação do objeto CampoMinado e chamada do método IniciarJogo()
        CampoMinado campoMinado = new CampoMinado();
        Console.Write("CAMPO MINADO \n\nPor favor informe seu Nome: ");
        campoMinado.nome = Console.ReadLine();
        Console.WriteLine($"Bem-vindo(a) ao jogo {campoMinado.nome}!");
        Console.WriteLine($"OBS: O jogo contém 8 linhas e 8 colunas (0-7). Seu objetivo é revelar todas as posições sem acessar uma mina. \n\nBoa sorte!");

        campoMinado.IniciarJogo();
    }

    //Metodo para iniciar o jogo
    public void IniciarJogo()
    {
        lerArquivoTexto();

        //Inicializando tabuleiro com linhas e colunas = 0
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
        PreencherBombas();
        PreencherNumeros();

        while (true)
        {
            Console.WriteLine();
            ImprimirTabuleiro();

            Console.Write("\nDigite a linha: ");
            int linha = int.Parse(Console.ReadLine());
            Console.Write("Digite a coluna: ");
            int coluna = int.Parse(Console.ReadLine());

            RevelarPosicao(linha, coluna);

            if (VerificarVitoria())
            {
                Console.WriteLine($"Parabéns {nome}! Você venceu!");
                break;
            }

            if (VerificarDerrota())
            {
                Console.WriteLine($"\nQue pena {nome}, acertou uma bomba... você perdeu.");
                break;
            }
        }
    }


    //Metodo para ler arquivo contendo numero de linhas-colunas-bombas
    private void lerArquivoTexto()
    {
        this.linhas = 8;
        this.colunas = 8;
        this.totalBombas = 8;
    }

    //Metodo para posicionar as bombas de forma aleatoria no tabuleiro
    private void PreencherBombas()
    {
        Random rd = new Random();
        for (int contador = 0; contador < totalBombas; contador++)
        {
            int posicaolinha;
            int posicaocoluna;
            do
            {
                posicaolinha = rd.Next(linhas);
                posicaocoluna = rd.Next(colunas);
            } while (tabuleiro[posicaolinha, posicaocoluna] == '*');

            tabuleiro[posicaolinha, posicaocoluna] = '*';
        }
    }


    //ERRO
    //Metodo para preencher a quantidade de bombas adjacentes
    private void PreencherNumeros()
    {
        for (int linha = 0; linha < linhas; linha++)
        {
            for (int coluna = 0; coluna < colunas; coluna++)
            {
                if (tabuleiro[linha, coluna] != '*')
                {
                    int quantidadeBombasAdjacentes = ContarBombasAdjacentes(linha, coluna);
                    tabuleiro[linha, coluna] = quantidadeBombasAdjacentes;
                }
            }
        }
    }




    //Metodo para imprimir o tabuleiro completo na tela
    private void ImprimirTabuleiro()
    {
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                if (tabuleiroRevelado[i, j] == true)
                {
                    Console.Write(tabuleiro[i, j]);
                }
                else
                {
                    Console.Write("x");
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    //Metodo para exibir posicao escolhida pelo jogador
    private void RevelarPosicao(int linha, int coluna)
    {
        if (tabuleiroRevelado[linha, coluna] == true)
        {
            Console.WriteLine("\nEste campo já foi aberto! Escolha outra opção.");
            return;
        }

        tabuleiroRevelado[linha, coluna] = true;

        if (tabuleiro[linha, coluna] == 0)
        {
            ContarBombasAdjacentes(linha, coluna);
        }
        else if (tabuleiro[linha, coluna] == '*')
        {
            VerificarDerrota();
        }
    }

    private int ContarBombasAdjacentes(int linha, int coluna)
    {
        int quantidadeBombas = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int novaLinha = linha + i;
                int novaColuna = coluna + j;

                if (novaLinha >= 0 && novaLinha < linhas && novaColuna >= 0 && novaColuna < colunas)
                {
                    if (tabuleiro[novaLinha, novaColuna] == '*')
                    {
                        quantidadeBombas++;
                    }
                }
            }
        }

        return quantidadeBombas;
    }
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








  
