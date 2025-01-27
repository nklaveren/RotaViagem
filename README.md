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
   - Implementação de busca em profundidade (DFS) com backtracking
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
src/
  ├── RotaViagem.Domain/        # Camada de domínio com entidades e regras de negócio
  ├── RotaViagem.AppConsole/    # Aplicação console com interface de usuário
tests/
  ├── RotaViagem.Domain.Tests/  # Testes unitários do domínio
  └── RotaViagem.App.Tests/     # Testes de integração da aplicação
```


## Como Executar o Projeto

### Pré-requisitos
- .NET SDK 8.0 ou superior
- Visual Studio 2022 ou VS Code com extensão C#

1. Clone o repositório:
```bash
git clone https://github.com/nklaveren/RotaViagem.git
cd RotaViagem
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

## Executando os Testes

```bash
dotnet test
```