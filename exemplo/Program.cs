using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CampoMinado
{
    /*INTEGRANTES do GRUPO: Ana Paula Cardoso, Barbara Giovana, Hayanne Santos.
    Data da Criação: 15/06/2023      Última alteração:*/

    private int[,] tabuleiro;
    private bool[,] tabuleiroRevelado;
    private int linhas;
    private int colunas;
    private int totalBombas;
    private string nome;

    static void Main(string[] args)
    {
        // Criação do objeto CampoMinado e chamada do método IniciarJogo()
        CampoMinado campoMinado = new CampoMinado();
        Console.WriteLine("Bem-Vindo ao CAMPO MINADO! \nPor favor informe seu Nome:");
        campoMinado.nome = Console.ReadLine();

       // Console.WriteLine($"Muito bem {campoMinado.nome} vamos iniciar!\nObjetivo do JOGO: revele todas as posições sem selecionar uma mina. Boa sorte:");
        

        campoMinado.IniciarJogo();
    }

    // Método para iniciar o jogo
    public void IniciarJogo()
    {
        //Chamada do metodo para ler arquivo texto
        lerArquivoTexto();

        //Inicializando tabuleiro de linhas e colunas com 0
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

        //Chamada do metodo para posicionar as bombas
        PreencherBombas();
        //Chamada do metodo para preencher o numero de bombas adjacentes
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
                Console.WriteLine("Parabéns! Você venceu!");
                break;
            }
           if (VerificarDerrota())
            {
                Console.WriteLine("Você perdeu! Acertou uma bomba.");
                break;
            }
        }
    }

    private void lerArquivoTexto()
    {               
        this.linhas = 8;
        this.colunas = 8;
        this.totalBombas = 8;
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

    // Método para mostrar o tabuleiro na tela
    private void ImprimirTabuleiro()
    {     
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
                    Console.Write("x");
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    } 

    private void RevelarPosicao(int linha, int coluna)
    {
        if (tabuleiroRevelado[linha, coluna])
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

    private void DescobrirCelulasAdjacentes(int linha, int coluna)
    {
        // Verificar célula acima
        if (linha - 1 >= 0 && !tabuleiroRevelado[linha - 1, coluna])
        {
            RevelarPosicao(linha - 1, coluna);
        }
        // Verificar célula abaixo
        if (linha + 1 < linhas && !tabuleiroRevelado[linha + 1, coluna])
        {
            RevelarPosicao(linha + 1, coluna);
        }
        // Verificar célula à esquerda
        if (coluna - 1 >= 0 && !tabuleiroRevelado[linha, coluna - 1])
        {
            RevelarPosicao(linha, coluna - 1);
        }
        // Verificar célula à direita
        if (coluna + 1 < colunas && !tabuleiroRevelado[linha, coluna + 1])
        {
            RevelarPosicao(linha, coluna + 1);
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







