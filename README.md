<div align="center">
  
  # 🚀 ik-nerver2.0 — API REST de Alta Performance & Solução do Problema N+1
  
</div>

Este projeto foi desenvolvido com o objetivo de servir como um portfólio de prova técnica para a vaga de Desenvolvedor(a).NET iK. A aplicação demonstra de forma prática como construir um ecossistema de Back-end moderno utilizando as melhores práticas de arquitetura, focando em performance extrema de acesso a dados e integração limpa com interfaces legadas.

---

## 🛠️ O Que Foi Feito e Implementado

No desenvolvimento do **ik-nerver2.0**, resolvemos desafios críticos de infraestrutura, redes e banco de dados que mimetizam perfeitamente o cenário real de sustentação e evolução de sistemas de grande porte:

1. **Construção de uma API — Application Programming Interface (Interface de Programação de Aplicação) REST — Representational State Transfer (Transferência de Estado Representacional):** Arquitetada sob o ecossistema moderno do .NET 8, utilizando controladores desacoplados para expor rotas assíncronas e tipadas.
2. **Resolução Cirúrgica do Problema N+1:** Implementamos a técnica de **Multi-Mapping** do Dapper. Em sistemas corporativos, carregar coleções ou objetos aninhados (como um Cliente que possui um Endereço) costuma gerar centanas de consultas redundantes ao banco. Criamos uma consulta estruturada utilizando um `INNER JOIN` e ensinamos o Dapper a segmentar a linha de dados na memória RAM (Random Access Memory) (Memória de Acesso Aleatório), reduzindo centenas de viagens de rede para **apenas 1 única chamada** de alta velocidade.
3. **Migração Automática Resiliente (Auto-Migration):** O construtor do controlador foi projetado para verificar a existência do Banco de dados local no momento do boot. Caso não exista, o próprio sistema cria os esquemas relacionais de tabelas e insere uma massa de dados estável para testes automáticos.
4. **Segurança contra Injeção de SQL (SQL Injection):** Todas as instruções lógicas disparadas para o motor relacional utilizam parâmetros nomeados anônimos, impedindo a manipulação de strings por agentes maliciosos.
5. **Configuração Defensiva de CORS (Cross-Origin Resource Sharing) (Compartilhamento de Recursos de Origem Cruzada):** Implementamos políticas explícitas no pipeline HTTP — HyperText Transfer Protocol (Protocolo de Transferência de Hipertexto) para permitir que servidores locais de Front-end (como o Live Server na porta 5500) consumam os dados sem bloqueios nativos de segurança do navegador.
6. **Controle de Versão Profissional:** O repositório foi inicializado e estruturado localmente com Git, aplicando um arquivo `.gitignore` otimizado para evitar o vazamento de binários pesados e configurações sensíveis de ambiente de desenvolvimento.

---

## 📦 Bibliotecas e Dependências Instaladas

A stack de desenvolvimento foi selecionada visando o menor consumo possível de CPU — Central Processing Unit (Unidade Central de Processamento) e memória, utilizando drivers agnósticos:

* **Dapper (v2.1.79):** Micro-ORM — Object-Relational Mapper (Mapeador Objeto-Relacional) de alta performance. Ele estende a interface nativa de conexões e realiza o mapeamento direto de tabelas SQL — Structured Query Language(Linguagem de Consulta Estruturada) para objetos C# em velocidade próxima ao ADO.NET puro.
* **Microsoft.Data.Sqlite (v10.0.8):** Driver oficial da Microsoft para o motor leve e embutido do SQLite. Substitui instâncias robustas do SQL Server para fins de portabilidade do ambiente local de desenvolvimento, gravando os dados diretamente em um arquivo binário local independente.

---

## 💻 Tecnologias Utilizadas (Tech Stack)

* **Back-end:** C# no ecossistema .NET 8.0 (SDK 8.0.417)
* **Acesso a Dados:** Dapper (Micro-ORM)
* **Banco De Dados:** SQLite (.db local autônomo)
* **Front-end / Interface Legada:** HTML — HyperText Markup Language (Linguagem de Marcação de Hipertexto) com integração assíncrona baseada em AJAX — Asynchronous JavaScript and XML (JavaScript e XML Assíncronos) via biblioteca **jQuery (v3.7.1)**.
* **Versionamento:** Git integrado

---

## 🖥️ Guia do Terminal — Execução Sem Erros: CLI — Command Line Interface (Interface de Linha de Comando)

Para testar o ecossistema completo localmente sem enfrentar travamentos de arquivos ocupados, desvios indesejados de SSL — Secure Sockets Layer (Camada de Sockets Segura) ou erros de conexões recusadas, siga rigorosamente o passo a passo de comandos no seu prompt de comando:

### Passo 1: Derrubar Processos Fantasmas e Limpar Cache
Se você tentou rodar a aplicação anteriormente e o Windows travou o executável na memória, limpe o ambiente executando estes comandos no seu terminal:
```powershell
# Matar qualquer instância travada que esteja bloqueando o binário no disco
Stop-Process -Name "ik-nerver2.0" -Force -ErrorAction SilentlyContinue

# Limpar rastros de compilações anteriores corrompidas
dotnet clean
```

---

### Passo 2: Compilar a Aplicação
Valide a estrutura do código e certifique-se de que o compilador gere o pacote binário com sucesso:
```powershell
dotnet build
```

---

### Passo 3: Iniciar o Servidor da API
Inicie o servidor embutido (Kestrel) travando o pipeline para escutar exclusivamente em HTTP puro na porta 5281. Isso impede desvios para HTTPS local que geram falhas de certificado no navegador:
```powershell
dotnet run
```
 >*Atenção: Deixe o terminal aberto e ativo mantendo a aplicação viva.*

---

### Passo 4: Inicializar a Interface Visual (Front-end)

1. Com a API rodando, certifique-se de que o arquivo `index.html` está criado na raiz da pasta.

2. Abra o arquivo `index.html` no seu navegador (usando o Live Server do VS Code ou dando um duplo clique direto no arquivo pelo Windows Explorer).

3. Clique no botão "Executar Chamada jQuery" e veja o painel ser populado de forma imediata com os dados do banco, provando a integração legada!

---

### 🛡️ Como Defender Este Projeto na Entrevista com a iK

1. Por que Dapper e não Entity Framework? "Em ambientes de alto volume, o EF adiciona um overhead de rastreamento de estado. O Dapper foi adotado por ser leve e executar queries brutas em tempo recorde."

2. Como você tratou o problema N+1? "Desenhei um INNER JOIN no SQL e apliquei a técnica de Multi-Mapping do Dapper. Trouxemos o cliente e o endereço em uma única viagem de rede (Roundtrip)."

3. Por que usar SQLite no ambiente de desenvolvimento? "Para garantir portabilidade e resiliência. O projeto não exige que o avaliador possua uma instância pesada do SQL Server configurada. No ambiente de produção, basta alterar a string de conexão para apontar para o SQL Server."

---

Desenvolvido por **DavidABx.**
