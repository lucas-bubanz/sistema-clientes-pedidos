# Fluxo e Funcionamento das Tabelas

## 1. Tabela Cliente

**Descrição:**  
Armazena informações dos clientes, como nome, endereço, etc.

**Operações:**

- **Consultar:** Buscar um cliente específico pelo ID.
- **Listar:** Retornar todos os clientes cadastrados.
- **Deletar:** Remover um cliente (verificar se não há dependências, como pedidos associados).
- **Atualizar:** Alterar informações do cliente, como nome ou endereço.

---

## 2. Tabela Produto

**Descrição:**  
Armazena informações dos produtos, como nome, valor e descrição.

**Operações:**

- **Consultar:** Buscar um produto específico pelo ID.
- **Listar:** Retornar todos os produtos cadastrados.
- **Deletar:** Remover um produto (verificar se não está associado a um pré-pedido ou pedido).
- **Atualizar:** Alterar informações do produto, como preço ou descrição.

---

## 3. Tabela Pré-Pedido

**Descrição:**  
Armazena informações temporárias de um pré-pedido, como o cliente associado e o endereço.

**Operações:**

- **Consultar:** Buscar um pré-pedido específico pelo ID.
- **Listar:** Retornar todos os pré-pedidos cadastrados.
- **Deletar:** Remover um pré-pedido (geralmente usado para cancelar um pré-pedido antes de ser convertido em pedido).
- **Atualizar:** Alterar informações do pré-pedido, como o cliente ou endereço.

---

## 4. Tabela Pré-Pedido Produto

**Descrição:**  
Tabela intermediária que associa produtos a um pré-pedido, incluindo a quantidade de cada produto.

**Operações:**

- **Consultar:** Buscar os produtos associados a um pré-pedido específico.
- **Listar:** Retornar todos os produtos de todos os pré-pedidos.
- **Deletar:** Remover um produto de um pré-pedido.
- **Atualizar:** Alterar a quantidade de um produto em um pré-pedido.

---

## 5. Tabela Pedido

**Descrição:**  
Armazena informações consolidadas de um pedido, como o cliente, data do pedido e valor total.

**Operações:**

- **Consultar:** Buscar um pedido específico pelo ID.
- **Listar:** Retornar todos os pedidos cadastrados.
- **Deletar:** Remover um pedido (geralmente não recomendado, mas pode ser usado para corrigir erros).
- **Atualizar:** Alterar informações do pedido, como status ou valor total (se necessário).

---

## 6. Tabela Pedido Produto

**Descrição:**  
Tabela intermediária que associa produtos a um pedido, incluindo a quantidade de cada produto.

**Operações:**

- **Consultar:** Buscar os produtos associados a um pedido específico.
- **Listar:** Retornar todos os produtos de todos os pedidos.
- **Deletar:** Remover um produto de um pedido.
- **Atualizar:** Alterar a quantidade de um produto em um pedido.

---

# Fluxo Geral

## Pré-Pedido

1. O cliente inicia um pré-pedido, associando produtos e suas quantidades.
2. As informações são armazenadas nas tabelas `pre_pedido` e `pre_pedido_produto`.

## Confirmação do Pedido

1. Após a confirmação, os dados do pré-pedido são transferidos para as tabelas `pedido` e `pedido_produto`.
2. O valor total do pedido é calculado com base nos produtos e suas quantidades.

## Consulta e Listagem

- As tabelas `pedido` e `pedido_produto` são usadas para listar pedidos consolidados e seus produtos.
- As tabelas `pre_pedido` e `pre_pedido_produto` são usadas para listar pré-pedidos em andamento.

## Atualização e Exclusão

- Pré-pedidos podem ser atualizados ou excluídos antes de serem confirmados.
- Pedidos consolidados podem ser atualizados (ex.: status) ou excluídos (se necessário).
