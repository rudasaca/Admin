USE [AdmonClientes]
GO

/****** Object:  Table [dbo].[Clientes]    Script Date: 14/06/2022 3:23:24 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Clientes](
	[NumeroIdentificacion] [int] NOT NULL,
	[Nombres] [nvarchar](50) NOT NULL,
	[IdTipoIdentificacion] [int] NOT NULL,
	[Correo] [varchar](80) NULL,
	[Celular] [nvarchar](15) NULL,
	[Direccion] [nvarchar](30) NULL,
	[DireccionDeInstalacion] [nvarchar](30) NULL,
	[Activo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[NumeroIdentificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TipoIdentificacion](
	[IdTipoIdentificacion] [int] IDENTITY(1,1) NOT NULL,
	[TipoIdentificacion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoIdentificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [Fk_Clientes_TipoIdentificacion] FOREIGN KEY([IdTipoIdentificacion])
REFERENCES [dbo].[TipoIdentificacion] ([IdTipoIdentificacion])
GO

ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [Fk_Clientes_TipoIdentificacion]
GO


