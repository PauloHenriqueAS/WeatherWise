using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Text;
using System.IO;
using System;

namespace WsRoutine.Helpers;
public class Log
{
    #region Atributos e propriedades

    private static readonly string Directory = "C:\\transf\\log\\WsRoutine\\";
    private static readonly string LogFile = $"{Directory}{NomePrograma}_{DateTime.Now:yyyyMMdd}.txt";
    private static readonly string NomePrograma = "WsRoutine";
    private static readonly string DescricaoPrograma = "Rotina de consulta de informações do Clima-Tempo";

    #endregion

    #region Métodos

    public static void InicioProcesso()
    {
        if (!System.IO.Directory.Exists(Directory))
            System.IO.Directory.CreateDirectory(Directory);

        var header = new StringBuilder()
            .AppendLine("===================================================================")
            .AppendLine($"{NomePrograma} - {DescricaoPrograma}")
            .AppendLine($"{DateTime.Now}")
            .ToString();

        Console.WriteLine(header);
        using (StreamWriter writer = new StreamWriter(LogFile, true))
        {
            writer.WriteLine(header);
            writer.Close();
        }
    }

    public static void FimProcesso(bool erro = false)
    {
        var footer = $"Fim do Processo [{(erro ? "FALHA" : "SUCESSO")}]";

        AdicionarLinha();
        Console.WriteLine(footer);
        using (StreamWriter writer = new StreamWriter(LogFile, true))
        {
            writer.WriteLine(footer);
            writer.Close();
        }
    }

    public static void Salvar(string msg, bool erro = false)
    {
        var logMsg = $"{DateTime.Now} [{(erro ? "E" : "P")}] :: {msg}";
        Console.WriteLine(logMsg);
        using (StreamWriter writer = new StreamWriter(LogFile, true))
        {
            writer.WriteLine(logMsg);
            writer.Close();
        }
    }

    public static void AdicionarLinha()
    {
        Console.WriteLine(" ");
        using (StreamWriter writer = new StreamWriter(LogFile, true))
        {
            writer.WriteLine(" ");
            writer.Close();
        }
    }

    #endregion
}