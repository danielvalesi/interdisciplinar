create database cardapioDigital
GO

use cardapioDigital
GO

CREATE TABLE Pessoas
(
	pessoa_id	int				not null primary key identity,
	nome		varchar(50)		not null,
	email		varchar(100)	not null unique,
	senha		varchar(32)		not null,
	cpf			varchar(16)		not null unique,
	imagem		varchar(max)		null
)
GO

CREATE TABLE ClientesVIP
(
	pessoa_id				int				not null ,
	porcentagemDesconto		decimal(4,2)	not null
	primary key(pessoa_id),
	foreign key(pessoa_id)	references Pessoas(pessoa_id)
)
GO

CREATE TABLE Funcionarios
(
	pessoa_id		int				not null primary key,
	cargo			varchar(24)		not null,
	salario			decimal(10,2)	not null,
	horarioEntrada	time				null,
	horarioSaida	time				null
		
	foreign key(pessoa_id)	references Pessoas(pessoa_id)
)
GO

CREATE TABLE Produtos
(
	id				int				not null primary key identity,
	nome			varchar(100)	not null,
	preco			decimal(10,2)	not null,
	descricao		varchar(max)		null,
	categoria		int				not null,
	imagem			varchar(max)		null,
	tempoPreparo	int					null,
	estoque			int					null
)
GO

CREATE TABLE Localizacoes
(
	numero		int			not null primary key identity,
	qtdLugares	int				null,
	descricao	varchar(max)	null,
	status		int			not null
)
GO


CREATE TABLE Contas
(
	id				int			not null identity,
	localizacao_id	int			not null,
	cliente_id		int				null,
	dataAbertura	datetime	not null,
	dataFechamento	datetime		null,
	status			int			not null,
	valor			decimal(15,2)	null,
	formaPagamento	int				null

	PRIMARY KEY(id),
	foreign key(localizacao_id) references Localizacoes(numero),
	foreign key(cliente_id) references ClientesVIP(pessoa_id)
	
)
GO

CREATE TABLE Pedidos
(
	id			int				not null primary key identity,
	conta_id	int				not null,
	status		int				not null,
	dataEntrega datetime			null
	foreign key(conta_id) references Contas(id)
)
go

CREATE TABLE Itens
(
	produto_id			int				not null,
	funcionario_id		int					null,
	pedido_id			int				not null,
	qtd					int				not null,
	preco				decimal(10,2)	not null,
	dataEntrega			datetime			null,
	status				int				not null
	PRIMARY KEY(produto_id, pedido_id),
	FOREIGN KEY(produto_id) references Produtos(id),
	FOREIGN KEY(funcionario_id) references Funcionarios(pessoa_id),
	FOREIGN KEY(pedido_id) references Pedidos(id)
)
GO

CREATE VIEW Usuarios
AS
    SELECT Pessoas.pessoa_id,  Pessoas.nome, Pessoas.email, Pessoas.Senha, Funcionarios.cargo,
           IIF(Funcionarios.pessoa_id IS NULL, 'ClientesVIP', 'Funcionarios') AS Tipo
    FROM Pessoas
        LEFT JOIN Funcionarios ON Funcionarios.pessoa_id = Pessoas.pessoa_id
        LEFT JOIN ClientesVIP ON ClientesVIP.pessoa_id = Pessoas.pessoa_id
GO

CREATE PROCEDURE cadCliente
(
	@nome					varchar(50),
	@email					varchar(100),
	@senha					varchar(32),
	@cpf					varchar(16),
	@imagem					varchar(max)

)
as 
begin
	INSERT INTO Pessoas VALUES (@nome, @email, @senha, @cpf, @imagem)
	
	DECLARE @Desconto decimal(4,2)
	SET @Desconto = 0.00
	DECLARE @Id int
	SET @Id = SCOPE_IDENTITY()

	INSERT INTO ClientesVIP VALUES (@Id, @Desconto)
end
select * from Pessoas p, ClientesVIP c
where p.pessoa_id = c.pessoa_id

CREATE PROCEDURE cadFuncionario
(
	@nome				varchar(50),
	@email				varchar(100),
	@senha				varchar(32),
	@cpf				varchar(16),
	@imagem				varchar(max),
	
	@cargo				varchar(24),
	@salario			decimal(10,2),
	@horarioEntrada		time,
	@horarioSaida		time
	
)
as 
begin
	INSERT INTO Pessoas VALUES (@nome, @email, @senha, @cpf, @imagem)

	DECLARE @Id int
	SET @Id = SCOPE_IDENTITY()

	INSERT INTO Funcionarios VALUES (@Id, @cargo, @salario, @horarioEntrada, @horarioSaida)
end
--cadCliente 'Gustavo', 'gustavo@email.com', 'senha', '123.123.123-12', null, 0.02
CREATE PROCEDURE alterCliente
(
	@pessoa_id				int,
	@nome					varchar(50),
	@email					varchar(100),
	@senha					varchar(32),
	@cpf					varchar(16),
	@imagem					varchar(max),
	@porcentagemDesconto	decimal(4,2)

)
as 
begin
	update Pessoas set
		nome = @nome,
		email = @email,
		senha = @senha,
		cpf = @cpf,
		imagem = @imagem
	WHERE
		pessoa_id = @pessoa_id

	update ClientesVIP set
		porcentagemDesconto = @porcentagemDesconto
	WHERE
		pessoa_id = @pessoa_id
end

CREATE PROCEDURE alterFuncionario
(
	@pessoa_id				int,
	@nome					varchar(50),
	@email					varchar(100),
	@senha					varchar(32),
	@cpf					varchar(16),
	@imagem					varchar(max),
	
	@cargo					varchar(24),
	@salario				decimal(10,2),
	@horarioEntrada			time,
	@horarioSaida			time

)
as 
begin
	update Pessoas set
		nome	= @nome,
		email	= @email,
		senha	= @senha,
		cpf		= @cpf,
		imagem	= @imagem
	WHERE
		pessoa_id = @pessoa_id

	update Funcionarios set
		cargo			= @cargo,
		salario			= @salario,
		horarioEntrada	= @horarioEntrada,
		horarioSaida	= @horarioSaida
	WHERE
		pessoa_id = @pessoa_id
end

alterFuncionario 2, 'fulano', 'emmasddasdmail', 'saiuh', '11ishdasdasds-11', 'imagem.png', 'Gerente', 1000.00, '12:00:00', '18:00:00' 


INSERT INTO Localizacoes VALUES(4, 'mesa do fundo', 2)
INSERT INTO Localizacoes VALUES(3, 'mesa do inicio', 1)
go


INSERT INTO Pessoas	VALUES('DIOGO', 'DIOGO@GMAIL.COM', '654321', '321.321.321-21', null)
INSERT INTO Funcionarios VALUES(@@IDENTITY, 'GARÇOM', 350.00, GETDATE(), NULL)
go

INSERT INTO Pessoas	VALUES('DANIEL LEITE', 'DANIEL@GMAIL.COM', '123456', '123.123.123-12', null)
INSERT INTO ClientesVIP VALUES(@@IDENTITY, 0.2)
go

INSERT INTO Pessoas	VALUES('GUSTAVO', 'GUSTAVO@GMAIL.COM', '123456', '122.132.123-12', null)
INSERT INTO ClientesVIP VALUES(@@IDENTITY, 0.4)
go

INSERT INTO Contas VALUES(1, 2, getdate(), null, 0, null, 0)
go
INSERT INTO Produtos VALUES ('coca-cola', 4.50, 'refrigerante sabor cola da marca coca cola', 0, null, 0, 10)
INSERT INTO Produtos VALUES ('lanche de cheddar', 25.50, 'lanche de hamburguer, cebola caramelizada e cheddar', 1, null, 30, null)
go

INSERT INTO Pedidos(conta_id, status, dataEntrega) VALUES(1, 0, GETDATE())
INSERT INTO Pedidos(conta_id, status, dataEntrega) VALUES(1, 1, GETDATE())
INSERT INTO Itens VALUES(2, 1, 1, 4, 120, getdate(), 1)






/*
create procedure cadcliente
(
	@nome					varchar(50),
	@email					varchar(100),
	@senha					varchar(32),
	@cpf					varchar(16),
	@porcentagemDesconto	decimal(4,2)	
)
as
begin
	INSERT INTO Pessoas	VALUES(@nome, @email, @senha, @cpf)
	INSERT INTO ClientesVIP VALUES(@@IDENTITY, @porcentagemDesconto)
end


go
create procedure cadfuncionario
(
	@nome					varchar(50),
	@email					varchar(100),
	@senha					varchar(32),
	@cpf					varchar(16),
	@cargo					varchar(24),
	@salario				decimal(10,2),
	@horarioEntrada			datetime,
	@horarioSaida			datetime	
)
as
begin
	INSERT INTO Pessoas	VALUES(@nome, @email, @senha, @cpf)
	INSERT INTO ClientesVIP VALUES(@@IDENTITY, @cargo, @salario, @horarioEntrada, @horarioSaida)
end 


-- cadastrar Localizações	INSERT INTO Localizacoes	VALUES (@qtdLugares, @descricao, @status)
-- cadastrar Produtos		INSERT INTO Produtos		VALUES (@nome, @preco, @descricao, @categoria, @imagem, @tempoPreparo @estoque)
-- cadastrar Contas			INSERT INTO 
select * from Produto

CREATE PROCEDURE abreConta
(
	@localizacao_id		int,
	@cliente_id			int,
	@dataAbertura		datetime,
	@dataFechamento		datetime,
	@status				int,
	@valor				decimal(15,2),
	@formaPagamento		int
)
as
begin
	INSERT INTO Contas VALUES(@localizacao_id, @cliente_id, @dataAbertura, @dataFechamento, @status, @valor, @formaPagamento)
end
*/





















/*insert into Pessoas(nome, email, senha, cpf) 
values ('Xablau', 'xablau@xablau.com', '654321', '123.123.123-12')

insert into Clientes(pessoa_id, porcentagemDesconto)
values ( 1, 0.1)   

select	p.id ID,
		p.nome Nome,
		p.senha Senha,
		p.cpf CPF,
			c.porcentagemDesconto Desconto
from Pessoas p, Clientes c
where p.id = c.pessoa_id
go
create procedure cadcliente
(
	@nome					varchar(50),
	@email					varchar(100),
	@senha					varchar(32),
	@cpf					varchar(16),
	@porcentagemDesconto	decimal(4,2)	
)
as
begin
	INSERT INTO Pessoas	VALUES(@nome, @email, @senha, @cpf)
	INSERT INTO Clientes VALUES(@@IDENTITY, @porcentagemDesconto)
end
exec cadcliente 'toblito', 'toblito@gmail.com', 'senha', '321.321.321-21', 0.05

go
create procedure cadfuncionario
(
	@nome					varchar(50),
	@email					varchar(100),
	@senha					varchar(32),
	@cpf					varchar(16),
	@cargo					varchar(24),
	@horarioEntrada			datetime,
	@horarioSaida			datetime
)
as
begin
	INSERT INTO Pessoas	VALUES(@nome, @email, @senha, @cpf)
	INSERT INTO Funcionarios VALUES(@@IDENTITY, @cargo, @horarioEntrada, @horarioSaida)
end

exec cadfuncionario 'Jack deniels', 'jackdeniels@gmail.com', 'senha', '321.321.311-21', 'garçom', null, null

select *
from Funcionarios f, Pessoas p
where p.id = f.pessoa_id
*/
--Tabela Localizacoes --
INSERT INTO Localizacoes VALUES(4, 'mesa do fundo', 1)
INSERT INTO Localizacoes VALUES(3, 'mesa do inicio', 1)
INSERT INTO Localizacoes VALUES(3, 'mesa do meio', 1)
go
--Tabela Produtos --
INSERT INTO Produtos VALUES ('Água Mineral', 2.00, 'Água mineral da marca X', 0, null, 0, 15)
INSERT INTO Produtos VALUES ('coca-cola', 4.50, 'refrigerante sabor cola da marca coca cola', 0, null, 0, 10)
INSERT INTO Produtos VALUES ('lanche de cheddar', 25.50, 'lanche de hamburguer, cebola caramelizada e cheddar', 1, null, 30, null)
go
--Tabela Pessoas --
INSERT INTO Pessoas	VALUES('Gustavo',   'gustavo@gmail.com',   '654321', '111.111.111-11', null)
INSERT INTO Pessoas	VALUES('Diogo',     'diogo@gmail.com',     '654321', '222.222.222-22', null)
INSERT INTO Pessoas	VALUES('Daniel',    'daniel@gmail.com',    '654321', '333.333.333-33', null)
INSERT INTO Pessoas	VALUES('Fulano',    'Fulano@gmail.com',    '654321', '444.444.444-44', null)
INSERT INTO Pessoas	VALUES('Ciclano',   'Ciclano@gmail.com',   '654321', '555.555.555-55', null)
INSERT INTO Pessoas	VALUES('Beltrano',  'Beltrano@gmail.com',  '654321', '666.666.666-66', null)
go
--Tabela ClientesVIP --
INSERT INTO ClientesVIP VALUES(1, 0.2)
INSERT INTO ClientesVIP VALUES(2, 0.2)
INSERT INTO ClientesVIP VALUES(3, 0.2)
go
--Tabela Funcionario --
INSERT INTO Funcionarios VALUES(4, 'Garçom', 1000.00, GETDATE(), NULL)
INSERT INTO Funcionarios VALUES(5, 'Cozinheiro', 1220.00, GETDATE(), NULL)
INSERT INTO Funcionarios VALUES(6, 'Gerente', 2000.00, GETDATE(), NULL)
go
--Tabela Contas --
INSERT INTO Contas VALUES(1, 2, getdate(), null, 0, null, null)
INSERT INTO Contas VALUES(2, null, getdate(), null, 0, null, null)
INSERT INTO Contas VALUES(3, 1, getdate(), null, 0, null, null)
go
--Tabela Pedidos --
INSERT INTO Pedidos VALUES(1, 0, GETDATE())
INSERT INTO Pedidos VALUES(2, 1, GETDATE())
INSERT INTO Pedidos VALUES(3, 0, GETDATE())
go
--Tabela Itens --
INSERT INTO Itens VALUES(1, 4, 1, 1, 2.00, null, 0)
INSERT INTO Itens VALUES(2, 4, 2, 2, 9.00, null, 0)
INSERT INTO Itens VALUES(3, 4, 3, 3, 76.50, null, 0)
go



----------------------------------------------------------------------------

--1. Procedure para cadastrar uma localização na tabela Localizacoes
go
CREATE PROCEDURE locAdd
(
	@qtdLugares	int,
	@descricao	varchar(max),
	@status	int	
)
as
begin
	INSERT INTO Localizacoes VALUES(@qtdLugares, @descricao, @status)	
end
-- Teste de execução -
locAdd 6, 'maior mesa do estabelecimento', 0

-- 2. Procedure para cadastrar um produto na tabela Produtos
go
CREATE PROCEDURE proAdd
(
	@nome			varchar(100),
	@preco			decimal(10,2),
	@descricao		varchar(max),
	@categoria		int,
	@imagem		varchar(max),
	@tempoPreparo		int,
	@estoque		int
)
as
begin 
	INSERT INTO Produtos VALUES(@nome, @preco, @descricao, @categoria, @imagem,@tempoPreparo, @estoque)
end
-- Teste de execução -
proAdd 'espeto de carne', 4.00, 'Espeto de carne patinho', 0, null, 15, 30

-- 3. Procedure para cadastrar uma Pessoa na tabela Pessoas
go
CREATE PROCEDURE pessAdd
(
	@nome		varchar(50),
	@email		varchar(100),
	@senha		varchar(32),
	@cpf		varchar(16),
	@imagem	varchar(max)
)
as
begin
	INSERT INTO Pessoas VALUES(@nome, @email, @senha, @cpf, @imagem)
end
-- Teste de execução -
exec pessAdd 'Fulano',   'Fulan@hotmail.com', '123456', '123.123.123.12', null
exec pessAdd 'Beltrano', 'Beltrano@hotamil.com', '123456', '321.321.321.21', null 

-- 4. Procedure para cadastrar um Cliente na tabela ClientesVIP
go
CREATE PROCEDURE cliAdd
(
	@pessoa_id				int,
	@porcentagemDesconto	decimal(4,2)
)
as
begin
	INSERT INTO ClientesVIP VALUES(@pessoa_id, @porcentagemDesconto)
end

--Teste de execução -
cliAdd 1, 0

-- 5. Procedure para cadastrar um Funcionario na tabela Funcionarios
go
CREATE PROCEDURE funAdd
(
	@pessoa_id		int,
	@cargo			varchar(24),
	@salario		decimal(10,2),
	@horarioEntrada	time,
	@horarioSaida	time
)
as
begin
	INSERT INTO Funcionarios VALUES(@pessoa_id, @cargo, @salario, @horarioEntrada, @horarioSaida)
end

-- Teste de execução -
EXEC funAdd 2, 'Garçom', 880.00, '18:00:00' , '00:00:00'

-- 6. Procedure para cadastrar uma Conta na tabela Contas
go

CREATE PROCEDURE conAdd
(
	@localizacao_id	int,
	@cliente_id		int,
	@dataAbertura	datetime,
	@dataFechamento	datetime,
	@status			int,
	@valor			decimal(15,2),
	@formaPagamento	int
	
)
as
begin
	
	INSERT INTO Contas VALUES(@localizacao_id, @cliente_id, @dataAbertura, @dataFechamento, @status, @valor, @formaPagamento)
end
-- Teste de execução
declare @tmp datetime
set @tmp = GETDATE()
exec conAdd 1, 1, @tmp, null, 0, null, null

-- 7. Procedure para cadastrar pedido na tabela Pedidos
go
CREATE PROCEDURE pedAdd
(
	@conta_id	int,
	@status	int,
	@dataEntrega 	datetime
)
as
begin
	INSERT INTO Pedidos VALUES (@conta_id, @status, @dataEntrega)
end
pedAdd 1, 0, null

-- 8. Procedure para cadastrar item da tabela Itens
go
CREATE PROCEDURE itemAdd
(
	@produto_id			int,
	@funcionario_id		int,
	@pedido_id			int,
	@qtd				int,
	@preco				decimal(10,2),
	@dataEntrega			datetime,
	@status			int
)
as
begin
	INSERT INTO Itens VALUES(@produto_id, @funcionario_id, @pedido_id, @qtd, @preco, @dataEntrega, @status)
end
-- Teste de Execução
itemAdd 1, 2, 1, 4, 16.00, null, 0




--9. Procedure para atualizar um produto na tabela Produtos.
go
CREATE PROCEDURE prodAlt
(
	@id			int,
	@nome			varchar(100),
	@preco			decimal(10,2),
	@descricao		varchar(max),
	@categoria		int,
	@imagem		varchar(max),
	@tempoPreparo		int,
	@estoque		int 
)
as
begin
     update Produtos set 
            	nome			= @nome,
            	preco			= @preco,
            	descricao		= @descricao,
        	categoria		= @categoria,
		imagem			= @imagem,
		tempoPreparo		= @tempoPreparo,
		estoque		= @estoque

      where id = @id
end
select * from Produtos
--Teste de Execução -
prodAlt 1, 'produto 3', 2.00, 'descrição aqui', 1, '/produto3.png', 0, 10

-- 10.Procedure para atualizar uma pessoa 
go
CREATE PROCEDURE pessAlt
(
	@id		int,
	@nome		varchar(50),
	@email		varchar(100),
	@senha		varchar(32),
	@cpf		varchar(16),
	@imagem	varchar(max)
)
as
begin
	update Pessoas set
	nome	= @nome,
	email	= @email,
	senha	= @senha,
	cpf	= @cpf,
	imagem	= @imagem

end
-- Teste de Execução -
pessAlt 1, 'Fulana', 'Fulana@hotmail.com', '654321', '123.123.123-21', '/Fulana.png'

--11. Procedure para atualizar um Cliente na tabela CLientesVIP
CREATE PROCEDURE cliAlt
(
	@pessoa_id				int,
	@porcentagemDesconto			decimal(4,2)
)
as
begin
	update ClientesVIP set
			procentagemDesconto = @porcentagemDesconto
			where pessoa_id = @pessoa_id
end
--Teste de execução -
cliAlt 1, 0.15

--12. Procedure para atualizar um Funcionario na tabela Funcionarios
CREATE PROCEDURE funAlt
(
	@pessoa_id		int,
	@cargo			varchar(24),
	@salario		decimal(10,2),
	@horarioEntrada	time,
	@horarioSaida	time
)
as
begin
	update Funcionarios set
	cargo			= @cargo,
	salario		= @salario,
	horarioEntrada	= @horarioEntrada,
	horarioSaida	      	= @horarioSaida
	where pessoa_id     	= @pessoa_id
end	
-- Teste de Execução -
funAlt 1, 'Gerente', 1200.00, '12:00:00', '00:00:00'

--13. Procedure para atualizar uma Conta na tabela Contas
CREATE PROCEDURE conAlt
(
	@id			int,
	@localizacao_id	int,
	@cliente_id		int,
	@dataFechamento	datetime,
	@status			int,
	@valor			decimal(15,2),
	@formaPagamento	int		
)
as
begin
	update Contas set
	localizacao_id	= @localizacao_id,
	cliente_id		= @cliente_id,
	dataFechamento	= @dataFechamento,
	status			= @status,
	valor			= @valor,
	formaPagamento	= @formaPagamento
	where id = @id
end
--Teste de Execução -
declare @tmp datetime
set @tmp = getdate() 
exec conAlt 1, 1, 1, @tmp, 1 ,300.00, 2

--14. Procedure para atualizar um Pedido na tabela Pedidos
CREATE PROCEDURE pedAlt
(
	@id		int,
	@conta_id	int,
	@status	int,
	@dataEntrega datetime
)
as
begin
	update Pedidos set
	conta_id	= @conta_id,
	status		= @status,
	dataEntrega = @dataEntrega
	where id	= @id
end
-- Teste de Execução -
declare @tmp datetime
set @tmp = getdate() 
exec pedAlt 1, 2, 1, @tmp

--15. Procedure para atualizar um Item na tabela Itens
CREATE PROCEDURE itemAlt
(	
	@produto_id			int,
	@funcionario_id		int,
	@pedido_id			int,
	@qtd				int,
	@preco				decimal(10,2),
	@dataEntrega		       datetime,
	@status				int
)
as
begin
	update Itens set
	qtd		= @qtd,
	preco		= @preco,
	dataEntrega	= @dataEntrega,
	status		= @status
	where produto_id = @produto_id and funcionario_id = @funcionario_id and pedido_id = @pedido_id
end
-- Teste de Execução -
declare @tmp datetime
set @tmp = getdate() 
exec itemAlt 1, 2, 1, 3, 14.00, @tmp, 1

-------------------------------------------------------------------
--1. Visualização da tabela Localizacoes.
CREATE VIEW V_Localizacoes
AS
	select	l.numero		'Número do Local',
			l.qtdLugares	'Quantidade de Lugares',
			l.descricao		'Descrição',
			l.status		Status

	from Localizacoes l
--Teste de Execução -
SELECT * FROM V_Localizacoes

--2. Visualização da tabela Produtos.
CREATE VIEW V_Produtos
AS
	select	pr.id			ID,
			pr.nome			Nome,
			pr.preco		'Preço',
			pr.descricao	'Descrição',
			pr.categoria	Categoria,
			pr.imagem		Imagem,
			pr.tempoPreparo	'Tempo de Preparo',
			pr.estoque		Estoque
	
	from Produtos pr
--Teste de Execução -
SELECT * FROM V_Produtos

--3. Visualização da tabela Pessoas.
CREATE VIEW V_Pessoas
AS
	select 	p.id		ID,
			p.nome		Nome,
			p.email		Email,
			p.senha		Senha,
			p.cpf		CPF

	from Pessoas p

--Teste de Execução -
select * from V_Pessoas

--4. Visualização da tabela ClientesVIP.
CREATE VIEW V_ClientesVIP
AS
	select	c.pessoa_id						ID,
			c.porcentagemDesconto*100	'Porcentagem de desconto'
	
	from ClientesVIP c

--Teste de Execução -
select * from V_CLientesVIP

--5. Visualização da tabela Funcionarios.
CREATE VIEW V_Funcionarios
AS
	select	f.pessoa_id			ID,
			f.cargo				Cargo,
			f.salario			'Salário',
			f.horarioEntrada	'Horário de Entrada',
			f.horarioSaida		'Horário de Saída'
	
	from Funcionarios f

--Teste de Execução -
select * from V_Funcionarios

--6. Visualização da tabela Contas.
CREATE VIEW V_Contas
AS
	select	co.id				ID,
			co.localizacao_id	'ID da Localizacao',
			co.cliente_id		'ID do Cliente',
			co.dataAbertura		'Data de Abertura',
			co.dataFechamento	'Data de Fechamento',
			co.status			Status,
			co.valor			Valor,
			co.formaPagamento	'Forma de Pagamento'
	
	from Contas co

--Teste de Execução -
select * from V_Contas

--7. Visualização da tabela Pedidos.
CREATE VIEW V_Pedidos
AS
	select	pd.id					ID,
			pd.conta_id				status,
			pd.dataEntrega			'Data em que foi entregue'
	
	from Pedidos pd

--Teste de Execução -
select * from V_Pedidos

--8. Visualização da tabela Itens.
CREATE VIEW V_Itens
AS
	select	i.produto_id			'ID de Produto',
			i.funcionario_id		'ID de Funcionario',
			i.pedido_id				'ID de Pedido',
			i.qtd					Quantidade,
			i.preco					Preco,
			i.dataEntrega			'Data de Entrega'
	
	from Itens i

--Teste de execução -
select * from V_Itens

--9. Visualização da tabela Clientes e Pessoas.1
CREATE VIEW dadosClientes
AS
	select	p.id						ID,
			p.nome						Nome,
			p.email						Email,
			p.senha						Senha,
			p.cpf						CPF,
			p.imagem					Imagem,
			c.porcentagemDesconto*100	'Porcentagem de Desconto'

	from Pessoas p, ClientesVIP c
	where p.id = c.pessoa_id

--Teste de Execução -
select * from dadosClientes

--10. Visualização da tabela Funcionarios e Pessoas.2
CREATE VIEW dadosFuncionarios
AS
	select	p.id						ID,
			p.nome						Nome,
			p.email						Email,
			p.senha						Senha,
			p.cpf						CPF,
			p.imagem					Imagem,
			f.cargo						Cargo,


	from Pessoas p, Funcionarios f

--Teste de Execução -
select * from dadosClientes




select * 