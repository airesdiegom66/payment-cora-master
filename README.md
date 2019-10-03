# payment-cora-master

1 – Rodar a solução: ler o document Pré-requisitos.docx;

2 – Baixar a collection do postman e importar. Já possui uns exemplos para ajudar.

3 – Solução: DDD, TDD, CQRS, Solid, Clean Code, Api Rest (BFF), Code First (o código cria o banco)

3.1. Domínio: usei SOLID e o conceito de domínio rico que é averso ao domínio anêmico (classe e propriedades). O Domínio está isolado e pode ser isolado, não possui depedências. Trabalhei com ValueObject para evitar o primitive obssessions. Também usei fluent validation, evitando o uso de if e deixando as validações mais fluídas.  CQRS: segregação por commands e querys: (commands: escritas e querys: leituras). Uso de fail fast validation, evitando idas ao banco de dados

3.2. Shared: conteúdo que você compartilha entre seus domínios. Serviria inclusive para múltiplos domínios;

3.3. Infrastrutura: Implementação do repository pattern, o repository ficou independente da persitência que você vai usar, podendo ser banco sql , orm, nosql… Usei orm entity in memory, já que era para teste. Usei code first;

4 - TDD método: Red: fazer falhar, Green: fazer passar, Refactor: refatorar o código. Para criar os testes eu tive que criar um gerador de ordem de pagamento.
