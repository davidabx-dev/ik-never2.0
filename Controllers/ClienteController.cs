using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Dapper;

namespace IkNever2.Controllers;

// DTO (Data Transfer Object) (Objeto de Transferência de Dados) para o Endereço
public class EnderecoDto
{
    public int Id { get; set; }
    public string Logradouro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
}

// DTO do Cliente contendo o Endereço embutido
public class ClienteDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public EnderecoDto? EnderecoDetalhado { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly string _connectionString = "Data Source=banco_otimizado.db";

    public ClienteController()
    {
        // Setup de Migração Automática usando SQLite
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Clientes (Id INTEGER PRIMARY KEY, Nome TEXT, Documento TEXT);
            CREATE TABLE IF NOT EXISTS Enderecos (Id INTEGER PRIMARY KEY, ClienteId INTEGER, Logradouro TEXT, Cidade TEXT);
            
            INSERT OR IGNORE INTO Clientes (Id, Nome, Documento) VALUES (1, 'David Abreu - Tech Lead', '999.888.777-66');
            INSERT OR IGNORE INTO Enderecos (Id, ClienteId, Logradouro, Cidade) VALUES (1, 1, 'Rua das Inovações, 404', 'Extrema-MG');
        ");
    }

    [HttpGet("otimizado/{id}")]
    public async Task<IActionResult> ObterClienteComEndereco(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        
        // SOLUÇÃO SÊNIOR: INNER JOIN resolve o Problema N+1 buscando tudo de uma vez
        const string sql = @"
            SELECT c.Id, c.Nome, c.Documento,
                   e.Id, e.Logradouro, e.Cidade
            FROM Clientes c
            INNER JOIN Enderecos e ON c.Id = e.ClienteId
            WHERE c.Id = @Id";

        // Multi-Mapping do Dapper: Corta os resultados no campo 'Id' (do Endereço) e monta o objeto completo
        var resultado = await connection.QueryAsync<ClienteDto, EnderecoDto, ClienteDto>(
            sql,
            (cliente, endereco) =>
            {
                cliente.EnderecoDetalhado = endereco;
                return cliente;
            },
            new { Id = id },
            splitOn: "Id"
        );

        var clienteFinal = resultado.FirstOrDefault();
        if (clienteFinal == null) return NotFound(new { mensagem = "Cliente não encontrado." });
        
        return Ok(clienteFinal);
    }
}
