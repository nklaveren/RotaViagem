# Sistema de Rotas de Viagem

## Descrição do Teste
O projeto consiste em desenvolver um sistema que encontre a rota mais barata entre dois pontos, independentemente do número de conexões. O sistema deve permitir o registro de novas rotas e consultar o melhor caminho entre dois pontos considerando o menor custo total.

### Requisitos
* Linguagem: .NET C#
* Não utilizar algoritmo Dijkstra
* Evitar uso de frameworks ou bibliotecas externas
* Implementar testes unitários
* Seguir boas práticas de desenvolvimento

## Funcionalidades
1. Registro de novas rotas com persistência para consultas futuras
2. Consulta de melhor rota entre dois pontos

## Formato do Arquivo de Entrada
```
Origem,Destino,Valor

GRU,BRC,10
BRC,SCL,5
GRU,CDG,75
GRU,SCL,20
GRU,ORL,56
ORL,CDG,5
SCL,ORL,20
```

## Exemplo de Uso
```
Digite a rota: GRU-CGD
Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40

Digite a rota: BRC-SCL
Melhor Rota: BRC - SCL ao custo de $5
```

## Explicação do Algoritmo
O algoritmo implementa uma busca exaustiva para encontrar o caminho mais barato entre dois pontos:

1. **Estrutura de Dados**:
   - Utiliza um grafo direcionado onde os vértices são os aeroportos e as arestas são as rotas
   - Cada aresta possui um peso que representa o custo da rota

2. **Busca da Melhor Rota**:
   - Explora todas as possíveis rotas entre origem e destino
   - Mantém registro do menor custo encontrado
   - Armazena o caminho completo da melhor rota
   - Implementa controle de ciclos para evitar loops infinitos

3. **Otimizações**:
   - Poda caminhos que já excedem o menor custo encontrado
   - Utiliza estruturas de dados eficientes para armazenamento e busca

## Estrutura do Projeto
```
.
├── src/
│   └── RotaViagem.Domain/
│       ├── Entities/
│       │   └── Viagem.cs
│       └── ValueObjects/
│           └── Rota.cs
└── tests/
    └── RotaViagem.Domain.Tests/
        ├── ViagemTests.cs
        └── RotaTests.cs
```

## Como Executar o Projeto

### Pré-requisitos
- .NET SDK 8.0 ou superior
- Visual Studio 2022 ou VS Code com extensão C#

### Usando linha de comando

1. Clone o repositório
```bash
git clone [url-do-repositorio]
```

2. Navegue até o diretório do projeto
```bash
cd [nome-do-diretorio]
```

3. Execute os testes
```bash
dotnet test
```

### Usando Visual Studio
1. Abra a solução `RotaViagem.sln`
2. No menu Test, selecione "Run All Tests"

## Casos de Teste Implementados

### Testes de Rota
- Criação de rota com dados válidos
- Validação de dados inválidos (origem vazia, destino vazio, valor zero ou negativo)
- Conversão automática para maiúsculo

### Testes de Viagem
- Busca de melhor rota entre dois pontos
- Tratamento de rotas inexistentes
- Verificação de custos totais para diferentes combinações de origem e destino

## Tecnologias e Padrões Utilizados
- .NET 8.0
- xUnit para testes unitários
- Clean Architecture
- Domain-Driven Design (DDD)
- Value Objects
- Test-Driven Development (TDD)

## Decisões de Design
1. **Separação em Camadas**: Utilização de Clean Architecture para melhor organização e manutenibilidade
2. **Value Objects**: Implementação de Rota como Value Object para garantir imutabilidade e validações
3. **Testes Unitários**: Cobertura abrangente com casos de teste para garantir robustez
4. **Domínio Rico**: Encapsulamento de regras de negócio nas entidades e value objects

# Rota de Viagem

Sistema para encontrar a rota mais barata entre dois pontos, independente da quantidade de conexões.

## Estrutura do Projeto

```
src/
  ├── RotaViagem.Domain/        # Camada de domínio com entidades e regras de negócio
  ├── RotaViagem.AppConsole/    # Aplicação console com interface de usuário
tests/
  ├── RotaViagem.Domain.Tests/  # Testes unitários do domínio
  └── RotaViagem.App.Tests/     # Testes de integração da aplicação
```

## Pré-requisitos

- .NET 8.0 SDK
- Visual Studio 2022 ou VS Code

## Como Executar

1. Clone o repositório:
```bash
git clone [url-do-repositorio]
cd [nome-do-repositorio]
```

2. Restaure os pacotes e compile o projeto:
```bash
dotnet restore
dotnet build
```

3. Execute a aplicação:
```bash
cd src/RotaViagem.AppConsole
dotnet run
```

## Funcionalidades

O sistema possui duas funcionalidades principais:

1. **Carregar Rotas**: 
   - Permite carregar um arquivo com as rotas no formato `ORIGEM,DESTINO,VALOR`
   - Exemplo de arquivo:
   ```
   GRU,BRC,10
   BRC,SCL,5
   GRU,CDG,75
   ```

2. **Consultar Melhor Rota**:
   - Digite a rota no formato `ORIGEM-DESTINO`
   - O sistema retornará a rota mais barata e seu custo total
   - Exemplo:
   ```
   Digite a rota: GRU-CDG
   Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40
   ```

## Decisões de Design

1. **Arquitetura em Camadas**:
   - Domain: Contém as regras de negócio e entidades
   - AppConsole: Interface com usuário e serviços

2. **DDD (Domain-Driven Design)**:
   - Entidades e Value Objects bem definidos
   - Regras de negócio encapsuladas no domínio

3. **Princípios SOLID**:
   - Separação de responsabilidades
   - Inversão de dependência com interfaces
   - Classes coesas e focadas

4. **Padrões Utilizados**:
   - Command Pattern para opções do menu
   - Repository Pattern para persistência
   - Dependency Injection para acoplamento fraco

5. **Testes**:
   - Testes unitários para regras de domínio
   - Testes de integração para fluxos completos
   - Mocks para isolamento de dependências

## Algoritmo de Busca

- Implementação de busca em profundidade (DFS) com backtracking
- Encontra todas as rotas possíveis e seleciona a mais barata
- Evita ciclos usando conjunto de visitados
- Complexidade O(V + E) onde V são vértices e E são arestas

## Executando os Testes

```bash
dotnet test
```

## Limitações e Possíveis Melhorias

1. Persistência em memória (pode ser expandido para banco de dados)
2. Interface console (pode ser expandido para API ou UI)
3. Validações adicionais de entrada
4. Logging e tratamento de erros mais robusto