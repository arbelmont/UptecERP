alter table Lotes
add [Localizacao] varchar(30) NULL,
    [QtdeConcilia] decimal(18,4) NOT NULL default 0;

alter table NotaEntradaItens
add [Localizacao] varchar(30) NULL,
    [QtdeConcilia] decimal(18,4) NULL;

alter table OrdensLotes
add [Validade] datetime2 NULL;