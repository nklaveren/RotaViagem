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