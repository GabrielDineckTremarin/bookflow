# BookFlow

## Projeto de Sistema de Gerenciamento de Livros

**BookFlow** é um sistema de gerenciamento de livros, para bibliotecas ou sistemas de empréstimo de livros, com funcionalidades para gerenciar livros, exemplares, reservas, empréstimos e multas por atrasos. O sistema permite que os administradores, bibliotecários e membros interajam de maneira organizada, proporcionando um controle dos fluxos de empréstimos e devoluções de livros.

### Funcionalidades do Sistema:

1. **Gerenciamento de Livros e Exemplares**:
   - Cadastro de livros com informações detalhadas (título, autor, ISBN, data de publicação).
   - Cadastro e controle de exemplares físicos, incluindo condição (novo, usado, danificado, perdido).
   - Relacionamento claro entre o livro e suas cópias.

2. **Empréstimos**:
   - Realização de empréstimos de exemplares de livros para usuários.
   - Controle de datas de empréstimo e previsão de devolução.
   - Status do empréstimo (ativo, devolvido, atrasado).

3. **Reservas**:
   - Possibilidade de reservar livros quando todos os exemplares estão emprestados.
   - Controle das reservas realizadas por usuários, com status de reserva (ativa, cancelada, concluída).

4. **Multas**:
   - Aplicação de multas automáticas aos usuários que atrasarem a devolução de empréstimos.
   - Controle de valor da multa e status de pagamento.

5. **Usuários**:
   - Cadastro de usuários com informações como nome, CPF, e-mail, data de nascimento e papel no sistema.
   - Definição de papéis de usuário:
     - **Admin**: Acesso total para gerenciar todas as funções do sistema.
     - **Member**: Usuários comuns, que podem realizar reservas e empréstimos.

### Modelagem do Sistema (Diagrama UML):
![](./bookflow\bookflow.client\src\assets\images\diagrama_UML.png)
- O sistema foi modelado com um diagrama UML, representando as principais entidades:
  - **Book (Livro)**: Representa o livro em si, incluindo informações como título, autor e ISBN.
  - **Copy (Exemplar)**: Uma cópia física do livro, com condições (novo, usado, danificado, perdido).
  - **Loan (Empréstimo)**: Controle dos empréstimos realizados, incluindo data de empréstimo, data de devolução prevista e status.
  - **Reservation (Reserva)**: Controle das reservas de livros, permitindo que usuários reservem quando todos os exemplares estiverem indisponíveis.
  - **Fine (Multa)**: Penalidade aplicada por atraso na devolução de um empréstimo, relacionada a um usuário e a um empréstimo específico.
  - **User (Usuário)**: Representa os usuários do sistema, com diferentes papéis (Admin e Member).
 
### Regras de Negócio:

- **Reserva de Livro**: Um usuário só pode reservar um livro quando todos os exemplares estiverem emprestados.
- **Multa por Atraso**: Caso a data de devolução de um empréstimo seja ultrapassada e o status seja atualizado para "atrasado", uma multa será gerada automaticamente para o usuário.
- **Gerenciamento de Empréstimos**: O sistema controla os empréstimos de exemplares, permitindo ao usuário visualizar seus empréstimos atuais e passados.

### Projeto Acadêmico

Este projeto está sendo como parte da disciplina de **Sistemas da Informação** no curso de **Ciência da Computação** na **Universidade Federal do Pampa**. O objetivo é aplicar os conceitos aprendidos em sala de aula em um projeto prático.
