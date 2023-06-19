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
        this.linhas = 12;
        this.colunas = 12;
        this.totalBombas = 18;
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


    //Metodo para preencher a quantidade de bombas adjacentes
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

                int contador = 0;

               //Os loops for percorrem as células vizinhas no intervalo (i - 1) a (i + 1) para a linha
               //e (j - 1) a (j + 1) para a coluna.

                for (int posicaolinha_adj = i - 1; posicaolinha_adj <= i + 1; posicaolinha_adj++)
                {
                    for (int posicaocoluna_adj = j - 1; posicaocoluna_adj <= j + 1; posicaocoluna_adj++)
                    {

                        //A condição colunas garante que estamos dentro dos limites do tabuleiro, ou seja,
                        //estamos acessando uma posição válida.

                        if (posicaolinha_adj >= 0 && posicaolinha_adj < linhas && posicaocoluna_adj >= 0 && posicaocoluna_adj < colunas && tabuleiro[posicaolinha_adj, posicaocoluna_adj] == '*')
                        {
                            contador++;
                        }
                    }
                }
                tabuleiro[i, j] = contador;
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
                if (tabuleiroRevelado[i, j] == true )
                {
                    Console.Write(tabuleiro[i, j]);
                }
                else
                {
                    Console.Write("-");
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
            DescobrirCelulasAdjacentes(linha, coluna);
        }
        else if (tabuleiro[linha, coluna] == '*')
        {
            VerificarDerrota();
        }
    }

    //Metodo para revelar as celulas adjacentes da celula escolhida
    private void DescobrirCelulasAdjacentes(int linha, int coluna)
    {
        // Célula acima
        if (linha - 1 >= 0 && !tabuleiroRevelado[linha - 1, coluna])
        {
            RevelarPosicao(linha - 1, coluna);
        }
        // Célula abaixo
        if (linha + 1 < linhas && !tabuleiroRevelado[linha + 1, coluna])
        {
            RevelarPosicao(linha + 1, coluna);
        }
        // Célula à esquerda
        if (coluna - 1 >= 0 && !tabuleiroRevelado[linha, coluna - 1])
        {
            RevelarPosicao(linha, coluna - 1);
        }
        // Célula à direita
        if (coluna + 1 < colunas && !tabuleiroRevelado[linha, coluna + 1])
        {
            RevelarPosicao(linha, coluna + 1);
        }
        // Célula acima - esquerda
        if (linha - 1 >= 0 && coluna - 1 >= 0 && !tabuleiroRevelado[linha - 1, coluna - 1])
        {
            RevelarPosicao(linha - 1, coluna - 1);
        }
        // Célula acima - direita
        if (linha - 1 >= 0 && coluna + 1 < colunas && !tabuleiroRevelado[linha - 1, coluna + 1])
        {
            RevelarPosicao(linha - 1, coluna + 1);
        }
        // Célula abaixo - esquerda
        if (linha + 1 < linhas && coluna - 1 >= 0 && !tabuleiroRevelado[linha + 1, coluna - 1])
        {
            RevelarPosicao(linha + 1, coluna - 1);
        }
        // Célula abaixo - direita
        if (linha + 1 < linhas && coluna + 1 < colunas && !tabuleiroRevelado[linha + 1, coluna + 1])
        {
            RevelarPosicao(linha + 1, coluna + 1);
        }
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







