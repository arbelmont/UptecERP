CREATE SEQUENCE [LoteSequence] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;

GO

CREATE SEQUENCE [NotaSaidaSequence] AS int START WITH 10000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;

GO

CREATE SEQUENCE [OrdemSequence] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;

GO

CREATE TABLE [Arquivos] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Nome] varchar(255) NOT NULL,
    [Tamanho] int NOT NULL,
    [Tipo] varchar(50) NOT NULL,
    [Dados] varchar(max) NOT NULL,
    [DataGravacao] datetime2 NOT NULL,
    CONSTRAINT [PK_Arquivos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [CabecalhosNfe] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [NomeEmitente] varchar(150) NOT NULL,
    [DocumentoEmitente] varchar(14) NOT NULL,
    [ChaveNfe] varchar(50) NOT NULL,
    [ValorTotal] decimal(18,4) NOT NULL,
    [DataEmissao] datetime2 NOT NULL,
    [Situacao] tinyint NOT NULL,
    [ManifestacaoDestinatario] int NULL,
    [JustificativaManifestacao] nvarchar(max) NULL,
    [NfeCompleta] bit NOT NULL,
    [TipoNfe] tinyint NOT NULL,
    [Versao] int NOT NULL,
    [DigestValue] varchar(50) NOT NULL,
    [NumeroCartaCorrecao] int NULL,
    [CartaCorrecao] varchar(250) NULL,
    [DataCartaCorrecao] datetime2 NULL,
    [DataCancelamento] datetime2 NULL,
    [JustificativaCancelamento] varchar(250) NULL,
    [DataInclusao] datetime2 NOT NULL,
    [DataManifestacao] datetime2 NULL,
    [Notificacao] nvarchar(max) NULL,
    CONSTRAINT [PK_CabecalhosNfe] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Componentes] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Codigo] varchar(30) NOT NULL,
    [Descricao] varchar(50) NOT NULL,
    [Unidade] tinyint NOT NULL,
    [Preco] decimal(18,4) NOT NULL,
    [Ncm] varchar(12) NOT NULL,
    [Quantidade] decimal(18,4) NOT NULL,
    [QuantidadeMinima] decimal(18,4) NOT NULL,
    CONSTRAINT [PK_Componentes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Fornecedores] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Cnpj] varchar(14) NOT NULL,
    [InscricaoEstadual] varchar(16) NULL,
    [RazaoSocial] varchar(200) NOT NULL,
    [NomeFantasia] varchar(200) NOT NULL,
    [Email] varchar(100) NULL,
    [Website] varchar(200) NULL,
    [Observacoes] varchar(1000) NULL,
    CONSTRAINT [PK_Fornecedores] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [LinhaProducao] (
    [Id] uniqueidentifier NOT NULL,
    [Cliente] nvarchar(max) NULL,
    [Materia] nvarchar(max) NULL,
    [Produto] nvarchar(max) NULL,
    [Entrada] decimal(18,2) NULL,
    [Saldo] decimal(18,2) NULL,
    [Producao] decimal(18,2) NULL,
    [Expedicao] decimal(18,2) NULL,
    CONSTRAINT [PK_LinhaProducao] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [LoteSaldo] (
    [Id] uniqueidentifier NOT NULL,
    [Cliente] nvarchar(max) NULL,
    [Codigo] nvarchar(max) NULL,
    [Produto] nvarchar(max) NULL,
    [Entrada] decimal(18,2) NULL,
    [Saldo] decimal(18,2) NULL,
    CONSTRAINT [PK_LoteSaldo] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [NotasEntrada] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [NumeroNota] varchar(50) NOT NULL,
    [Data] datetime2 NOT NULL,
    [DataConciliacao] datetime2 NULL,
    [Valor] decimal(18,4) NOT NULL,
    [Cfop] varchar(5) NOT NULL,
    [CnpjEmissor] varchar(14) NOT NULL,
    [NomeEmissor] varchar(100) NOT NULL,
    [EmailEmissor] varchar(100) NULL,
    [TipoEmissor] tinyint NOT NULL,
    [TipoEstoque] tinyint NOT NULL,
    [Status] tinyint NOT NULL,
    [ArquivoId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_NotasEntrada] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Transportadoras] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Cnpj] varchar(14) NOT NULL,
    [InscricaoEstadual] varchar(16) NULL,
    [RazaoSocial] varchar(200) NOT NULL,
    [NomeFantasia] varchar(200) NOT NULL,
    [Email] varchar(100) NULL,
    [Website] varchar(200) NULL,
    [TipoEntregaPadrao] int NOT NULL,
    [Observacoes] varchar(1000) NULL,
    CONSTRAINT [PK_Transportadoras] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ComponenteMovimentos] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [ComponenteId] uniqueidentifier NOT NULL,
    [Quantidade] decimal(18,4) NOT NULL,
    [Data] datetime2 NOT NULL,
    [TipoMovimento] tinyint NOT NULL,
    [PrecoUnitario] decimal(18,4) NOT NULL,
    [PrecoTotal] decimal(18,4) NOT NULL,
    [NotaFiscal] varchar(50) NULL,
    [Saldo] decimal(18,4) NOT NULL,
    [Historico] varchar(200) NULL,
    CONSTRAINT [PK_ComponenteMovimentos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ComponenteMovimentos_Componentes_ComponenteId] FOREIGN KEY ([ComponenteId]) REFERENCES [Componentes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [FornecedorEnderecos] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Logradouro] varchar(200) NULL,
    [Numero] varchar(10) NULL,
    [Complemento] varchar(100) NULL,
    [Bairro] varchar(100) NULL,
    [Cep] varchar(8) NULL,
    [Cidade] varchar(100) NULL,
    [Estado] varchar(2) NULL,
    [Tipo] tinyint NOT NULL,
    [FornecedorId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_FornecedorEnderecos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FornecedorEnderecos_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [FornecedorTelefones] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Numero] varchar(11) NULL,
    [Tipo] int NOT NULL,
    [Whatsapp] bit NOT NULL DEFAULT 0,
    [Observacoes] varchar(1000) NULL,
    [Contato] varchar(100) NULL,
    [FornecedorId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_FornecedorTelefones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FornecedorTelefones_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [NotaEntradaItens] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Codigo] varchar(20) NOT NULL,
    [Descricao] varchar(200) NOT NULL,
    [Cfop] varchar(5) NOT NULL,
    [Unidade] tinyint NOT NULL,
    [PrecoUnitario] decimal(18,4) NOT NULL,
    [PrecoTotal] decimal(18,4) NOT NULL,
    [Quantidade] decimal(18,4) NOT NULL,
    [Lote] int NULL,
    [DataFabricacao] datetime2 NULL,
    [DataValidade] datetime2 NULL,
    [Localizacao] varchar(30) NULL,
    [QtdeConcilia] decimal(18,4) NULL,
    [NumeroNotaCobertura] varchar(50) NULL,
    [Status] tinyint NOT NULL,
    [NotaEntradaId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_NotaEntradaItens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NotaEntradaItens_NotasEntrada_NotaEntradaId] FOREIGN KEY ([NotaEntradaId]) REFERENCES [NotasEntrada] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Clientes] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Cnpj] varchar(14) NOT NULL,
    [InscricaoEstadual] varchar(16) NULL,
    [RazaoSocial] varchar(200) NOT NULL,
    [NomeFantasia] varchar(200) NOT NULL,
    [Email] varchar(100) NULL,
    [Website] varchar(200) NULL,
    [Observacoes] varchar(1000) NULL,
    [TransportadoraId] uniqueidentifier NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Clientes_Transportadoras_TransportadoraId] FOREIGN KEY ([TransportadoraId]) REFERENCES [Transportadoras] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [TransportadoraEnderecos] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Logradouro] varchar(200) NULL,
    [Numero] varchar(10) NULL,
    [Complemento] varchar(100) NULL,
    [Bairro] varchar(100) NULL,
    [Cep] varchar(8) NULL,
    [Cidade] varchar(100) NULL,
    [Estado] varchar(2) NULL,
    [Tipo] tinyint NOT NULL,
    [TransportadoraId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_TransportadoraEnderecos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportadoraEnderecos_Transportadoras_TransportadoraId] FOREIGN KEY ([TransportadoraId]) REFERENCES [Transportadoras] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [TransportadoraTelefones] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Numero] varchar(11) NULL,
    [Tipo] int NOT NULL,
    [Whatsapp] bit NOT NULL DEFAULT 0,
    [Observacoes] varchar(1000) NULL,
    [Contato] varchar(100) NULL,
    [TransportadoraId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_TransportadoraTelefones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransportadoraTelefones_Transportadoras_TransportadoraId] FOREIGN KEY ([TransportadoraId]) REFERENCES [Transportadoras] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ClienteEnderecos] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Logradouro] varchar(200) NULL,
    [Numero] varchar(10) NULL,
    [Complemento] varchar(100) NULL,
    [Bairro] varchar(100) NULL,
    [Cep] varchar(8) NULL,
    [Cidade] varchar(100) NULL,
    [Estado] varchar(2) NULL,
    [Tipo] tinyint NOT NULL,
    [ClienteId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ClienteEnderecos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClienteEnderecos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ClienteTelefones] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Numero] varchar(11) NULL,
    [Tipo] int NOT NULL,
    [Whatsapp] bit NOT NULL DEFAULT 0,
    [Observacoes] varchar(1000) NULL,
    [Contato] varchar(100) NULL,
    [ClienteId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ClienteTelefones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClienteTelefones_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [NotasSaida] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [NumeroNota] varchar(50) NOT NULL,
    [NaturezaOperacao] varchar(200) NOT NULL,
    [Data] datetime2 NOT NULL,
    [AliquotaIpi] decimal(18,4) NOT NULL,
    [ValorBaseCalculo] decimal(18,4) NOT NULL,
    [ValorIcms] decimal(18,4) NOT NULL,
    [ValorTotalProdutos] decimal(18,4) NOT NULL,
    [ValorFrete] decimal(18,4) NOT NULL,
    [ValorSeguro] decimal(18,4) NOT NULL,
    [ValorDesconto] decimal(18,4) NOT NULL,
    [ValorOutrasDespesas] decimal(18,4) NOT NULL,
    [ValorIpi] decimal(18,4) NOT NULL,
    [ValorPis] decimal(18,4) NOT NULL,
    [ValorCofins] decimal(18,4) NOT NULL,
    [ValorTotalNota] decimal(18,4) NOT NULL,
    [TipoDestinatario] tinyint NOT NULL,
    [ArquivoId] uniqueidentifier NULL,
    [ClienteId] uniqueidentifier NULL,
    [FornecedorId] uniqueidentifier NULL,
    [TransportadoraId] uniqueidentifier NULL,
    [EnderecoId] uniqueidentifier NOT NULL,
    [ModalidadeFrete] tinyint NOT NULL,
    [LocalDestino] tinyint NOT NULL,
    [FinalidadeEmissao] tinyint NOT NULL,
    [Status] tinyint NOT NULL,
    [OutrasInformacoes] varchar(1000) NOT NULL,
    [ErroApi] varchar(1000) NULL,
    [Tipo] tinyint NOT NULL,
    CONSTRAINT [PK_NotasSaida] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NotasSaida_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_NotasSaida_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_NotasSaida_Transportadoras_TransportadoraId] FOREIGN KEY ([TransportadoraId]) REFERENCES [Transportadoras] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Ordens] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [OrdemNumero] int NOT NULL,
    [DataEmissao] datetime2 NOT NULL,
    [DataProducao] datetime2 NULL,
    [QtdeTotal] decimal(18,2) NOT NULL,
    [QtdeTotalProduzida] decimal(18,2) NULL,
    [MotivoExpedicao] tinyint NOT NULL,
    [Status] tinyint NOT NULL,
    [ClienteId] uniqueidentifier NULL,
    [FornecedorId] uniqueidentifier NULL,
    [CodigoPeca] varchar(30) NULL,
    [DescricaoPeca] varchar(50) NULL,
    CONSTRAINT [PK_Ordens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Ordens_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Ordens_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Pecas] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Codigo] varchar(30) NOT NULL,
    [CodigoSaida] varchar(30) NOT NULL,
    [Descricao] varchar(50) NOT NULL,
    [Unidade] tinyint NOT NULL,
    [Tipo] tinyint NOT NULL,
    [Preco] decimal(18,4) NOT NULL,
    [PrecoSaida] decimal(18,4) NOT NULL,
    [Ncm] varchar(12) NOT NULL,
    [Revisao] varchar(40) NULL,
    [ClienteId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Pecas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pecas_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [NotaSaidaItens] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Codigo] varchar(20) NOT NULL,
    [Descricao] varchar(200) NOT NULL,
    [Cfop] varchar(5) NOT NULL,
    [Ncm] varchar(12) NOT NULL,
    [Unidade] tinyint NOT NULL,
    [ValorUnitario] decimal(18,4) NOT NULL,
    [AliquotaBaseCalculo] decimal(18,4) NOT NULL,
    [AliquotaIcms] decimal(18,4) NOT NULL,
    [AliquotaIpi] decimal(18,4) NOT NULL,
    [AliquotaIva] decimal(18,4) NOT NULL,
    [AliquotaPis] decimal(18,4) NOT NULL,
    [AliquotaCofins] decimal(18,4) NOT NULL,
    [IcmsSituacaoTributaria] smallint NOT NULL,
    [PisSituacaoTributaria] tinyint NOT NULL,
    [CofinsSituacaoTributaria] tinyint NOT NULL,
    [ValorBaseCalculo] decimal(18,4) NOT NULL,
    [ValorIcms] decimal(18,4) NOT NULL,
    [ValorIpi] decimal(18,4) NOT NULL,
    [ValorPis] decimal(18,4) NOT NULL,
    [ValorCofins] decimal(18,4) NOT NULL,
    [ValorTotal] decimal(18,4) NOT NULL,
    [Quantidade] decimal(18,4) NOT NULL,
    [OrdemNumero] int NULL,
    [LoteNumero] int NOT NULL,
    [LoteSequencia] int NOT NULL,
    [NotaSaidaId] uniqueidentifier NOT NULL,
    [TipoItem] tinyint NOT NULL,
    CONSTRAINT [PK_NotaSaidaItens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NotaSaidaItens_NotasSaida_NotaSaidaId] FOREIGN KEY ([NotaSaidaId]) REFERENCES [NotasSaida] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Lotes] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Data] datetime2 NOT NULL,
    [LoteNumero] int NOT NULL,
    [Quantidade] decimal(18,4) NOT NULL,
    [Saldo] decimal(18,4) NOT NULL,
    [PrecoEntrada] decimal(18,4) NOT NULL,
    [CfopEntrada] varchar(5) NULL,
    [NotaFiscal] varchar(50) NULL,
    [NotaFiscalCobertura] varchar(50) NULL,
    [Status] tinyint NOT NULL,
    [DataFabricacao] datetime2 NOT NULL,
    [DataValidade] datetime2 NOT NULL,
    [Localizacao] varchar(30) NULL,
    [QtdeConcilia] decimal(18,4) NOT NULL,
    [PecaId] uniqueidentifier NOT NULL,
    [TipoPeca] tinyint NOT NULL,
    [ClienteId] uniqueidentifier NULL,
    [FornecedorId] uniqueidentifier NULL,
    [Sequencia] int NOT NULL,
    [EhCobertura] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Lotes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Lotes_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Lotes_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Lotes_Pecas_PecaId] FOREIGN KEY ([PecaId]) REFERENCES [Pecas] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [PecaComponente] (
    [PecaId] uniqueidentifier NOT NULL,
    [ComponenteId] uniqueidentifier NOT NULL,
    [Quantidade] decimal(18,4) NOT NULL,
    CONSTRAINT [PK_PecaComponente] PRIMARY KEY ([PecaId], [ComponenteId]),
    CONSTRAINT [FK_PecaComponente_Componentes_ComponenteId] FOREIGN KEY ([ComponenteId]) REFERENCES [Componentes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PecaComponente_Pecas_PecaId] FOREIGN KEY ([PecaId]) REFERENCES [Pecas] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [PecaFornecedorCodigo] (
    [PecaId] uniqueidentifier NOT NULL,
    [FornecedorId] uniqueidentifier NOT NULL,
    [FornecedorCodigo] varchar(30) NULL,
    CONSTRAINT [PK_PecaFornecedorCodigo] PRIMARY KEY ([PecaId], [FornecedorId]),
    CONSTRAINT [FK_PecaFornecedorCodigo_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PecaFornecedorCodigo_Pecas_PecaId] FOREIGN KEY ([PecaId]) REFERENCES [Pecas] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [LoteMovimentos] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [Data] datetime2 NOT NULL,
    [LoteId] uniqueidentifier NOT NULL,
    [LoteSequencia] int NOT NULL,
    [Quantidade] decimal(18,4) NOT NULL,
    [PrecoUnitario] decimal(18,4) NOT NULL,
    [PrecoTotal] decimal(18,4) NOT NULL,
    [NotaFiscal] varchar(50) NULL,
    [TipoMovimento] tinyint NOT NULL,
    [Historico] varchar(200) NULL,
    CONSTRAINT [PK_LoteMovimentos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoteMovimentos_Lotes_LoteId] FOREIGN KEY ([LoteId]) REFERENCES [Lotes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [OrdensLotes] (
    [Id] uniqueidentifier NOT NULL,
    [Deleted] bit NOT NULL,
    [LoteNumero] int NOT NULL,
    [LoteSequencia] int NOT NULL,
    [Qtde] decimal(18,2) NOT NULL,
    [QtdeProduzida] decimal(18,2) NULL,
    [Validade] datetime2 NULL,
    [MotivoExpedicao] tinyint NOT NULL,
    [NotaFiscalSaida] varchar(50) NULL,
    [LoteId] uniqueidentifier NOT NULL,
    [OrdemId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_OrdensLotes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrdensLotes_Lotes_LoteId] FOREIGN KEY ([LoteId]) REFERENCES [Lotes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrdensLotes_Ordens_OrdemId] FOREIGN KEY ([OrdemId]) REFERENCES [Ordens] ([Id]) ON DELETE CASCADE
);

GO

CREATE UNIQUE INDEX [IX_CabecalhosNfe_ChaveNfe] ON [CabecalhosNfe] ([ChaveNfe]);

GO

CREATE INDEX [IX_ClienteEnderecos_ClienteId] ON [ClienteEnderecos] ([ClienteId]);

GO

CREATE INDEX [IX_Clientes_TransportadoraId] ON [Clientes] ([TransportadoraId]);

GO

CREATE INDEX [IX_ClienteTelefones_ClienteId] ON [ClienteTelefones] ([ClienteId]);

GO

CREATE INDEX [IX_ComponenteMovimentos_ComponenteId] ON [ComponenteMovimentos] ([ComponenteId]);

GO

CREATE INDEX [IX_FornecedorEnderecos_FornecedorId] ON [FornecedorEnderecos] ([FornecedorId]);

GO

CREATE INDEX [IX_FornecedorTelefones_FornecedorId] ON [FornecedorTelefones] ([FornecedorId]);

GO

CREATE INDEX [IX_LoteMovimentos_LoteId] ON [LoteMovimentos] ([LoteId]);

GO

CREATE INDEX [IX_Lotes_ClienteId] ON [Lotes] ([ClienteId]);

GO

CREATE INDEX [IX_Lotes_FornecedorId] ON [Lotes] ([FornecedorId]);

GO

CREATE INDEX [IX_Lotes_PecaId] ON [Lotes] ([PecaId]);

GO

CREATE INDEX [IX_NotaEntradaItens_NotaEntradaId] ON [NotaEntradaItens] ([NotaEntradaId]);

GO

CREATE INDEX [IX_NotaSaidaItens_NotaSaidaId] ON [NotaSaidaItens] ([NotaSaidaId]);

GO

CREATE UNIQUE INDEX [IX_NotasEntrada_NumeroNota] ON [NotasEntrada] ([NumeroNota]);

GO

CREATE INDEX [IX_NotasSaida_ClienteId] ON [NotasSaida] ([ClienteId]);

GO

CREATE INDEX [IX_NotasSaida_FornecedorId] ON [NotasSaida] ([FornecedorId]);

GO

CREATE UNIQUE INDEX [IX_NotasSaida_NumeroNota] ON [NotasSaida] ([NumeroNota]);

GO

CREATE INDEX [IX_NotasSaida_TransportadoraId] ON [NotasSaida] ([TransportadoraId]);

GO

CREATE INDEX [IX_Ordens_ClienteId] ON [Ordens] ([ClienteId]);

GO

CREATE INDEX [IX_Ordens_FornecedorId] ON [Ordens] ([FornecedorId]);

GO

CREATE INDEX [IX_OrdensLotes_LoteId] ON [OrdensLotes] ([LoteId]);

GO

CREATE INDEX [IX_OrdensLotes_OrdemId] ON [OrdensLotes] ([OrdemId]);

GO

CREATE INDEX [IX_PecaComponente_ComponenteId] ON [PecaComponente] ([ComponenteId]);

GO

CREATE INDEX [IX_PecaFornecedorCodigo_FornecedorId] ON [PecaFornecedorCodigo] ([FornecedorId]);

GO

CREATE INDEX [IX_Pecas_ClienteId] ON [Pecas] ([ClienteId]);

GO

CREATE INDEX [IX_TransportadoraEnderecos_TransportadoraId] ON [TransportadoraEnderecos] ([TransportadoraId]);

GO

CREATE INDEX [IX_TransportadoraTelefones_TransportadoraId] ON [TransportadoraTelefones] ([TransportadoraId]);

GO

