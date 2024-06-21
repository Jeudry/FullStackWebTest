IF OBJECT_ID(N'[AppDb].[_EFAppDbMigrationHistory]') IS NULL
BEGIN
    IF SCHEMA_ID(N'AppDb') IS NULL EXEC(N'CREATE SCHEMA [AppDb];');
    CREATE TABLE [AppDb].[_EFAppDbMigrationHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK__EFAppDbMigrationHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'AppDb') IS NULL EXEC(N'CREATE SCHEMA [AppDb];');
GO

CREATE TABLE [AppDb].[AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppDb].[AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [LastName] nvarchar(256) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppDb].[Products] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [Code] nvarchar(512) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppDb].[AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AppDb].[AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AppDb].[AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppDb].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AppDb].[AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppDb].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AppDb].[AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AppDb].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppDb].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AppDb].[AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppDb].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AppDb].[AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AppDb].[AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AppDb].[AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AppDb].[AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AppDb].[AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AppDb].[AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AppDb].[AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Products_Name] ON [AppDb].[Products] ([Name]);
GO

INSERT INTO [AppDb].[_EFAppDbMigrationHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240619111110_Initial', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_Products_Name] ON [AppDb].[Products];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppDb].[AspNetUsers]') AND [c].[name] = N'LastName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AppDb].[AspNetUsers] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [AppDb].[AspNetUsers] DROP COLUMN [LastName];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppDb].[AspNetUsers]') AND [c].[name] = N'Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AppDb].[AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AppDb].[AspNetUsers] DROP COLUMN [Name];
GO

ALTER TABLE [AppDb].[Products] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [AppDb].[Products] ADD [Description] nvarchar(1024) NULL;
GO

ALTER TABLE [AppDb].[Products] ADD [Price] float NOT NULL DEFAULT 0.0E0;
GO

ALTER TABLE [AppDb].[Products] ADD [Stock] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [AppDb].[Products] ADD [UpdatedAt] datetime2 NULL;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AppDb].[AspNetRoles]'))
    SET IDENTITY_INSERT [AppDb].[AspNetRoles] ON;
INSERT INTO [AppDb].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES (N'dc043262-673a-491a-b811-446703743743', NULL, N'Admin', N'ADMIN'),
(N'dc043262-673a-491a-b811-446703743744', NULL, N'User', N'USER');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AppDb].[AspNetRoles]'))
    SET IDENTITY_INSERT [AppDb].[AspNetRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AppDb].[AspNetUsers]'))
    SET IDENTITY_INSERT [AppDb].[AspNetUsers] ON;
INSERT INTO [AppDb].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (N'dc043262-673a-491a-b811-446703743743', 0, N'07fd95fc-7916-4c0d-bc20-1ba60ece5920', N'admin@example.com', CAST(1 AS bit), CAST(0 AS bit), NULL, N'ADMIN@EXAMPLE.COM', N'ADMIN', NULL, N'18497505944', CAST(1 AS bit), N'55673a48-1a55-49c6-bd13-068861e13be5', CAST(0 AS bit), N'Admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AppDb].[AspNetUsers]'))
    SET IDENTITY_INSERT [AppDb].[AspNetUsers] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'CreatedAt', N'Description', N'Name', N'Price', N'Stock', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[AppDb].[Products]'))
    SET IDENTITY_INSERT [AppDb].[Products] ON;
INSERT INTO [AppDb].[Products] ([Id], [Code], [CreatedAt], [Description], [Name], [Price], [Stock], [UpdatedAt])
VALUES ('dc043262-673a-491a-b811-446703743743', N'PROD', '2024-06-20T17:35:34.1760020Z', N'Product description', N'Product', 100.0E0, 10, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'CreatedAt', N'Description', N'Name', N'Price', N'Stock', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[AppDb].[Products]'))
    SET IDENTITY_INSERT [AppDb].[Products] OFF;
GO

CREATE UNIQUE INDEX [IX_Products_Code] ON [AppDb].[Products] ([Code]);
GO

INSERT INTO [AppDb].[_EFAppDbMigrationHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240620173534_addedAll', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_Products_Code] ON [AppDb].[Products];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppDb].[Products]') AND [c].[name] = N'Code');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AppDb].[Products] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [AppDb].[Products] DROP COLUMN [Code];
GO

UPDATE [AppDb].[AspNetUsers] SET [ConcurrencyStamp] = N'12714d0c-ba6e-4315-b6be-9a30c3789f26', [SecurityStamp] = N'4c5a42e4-fe87-4f10-8441-501090657e2c'
WHERE [Id] = N'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

UPDATE [AppDb].[Products] SET [CreatedAt] = '2024-06-20T23:33:46.2185790Z'
WHERE [Id] = 'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

CREATE UNIQUE INDEX [IX_Products_Name] ON [AppDb].[Products] ([Name]);
GO

INSERT INTO [AppDb].[_EFAppDbMigrationHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240620233346_removed code property and finished register', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AppDb].[AspNetUsers] SET [ConcurrencyStamp] = N'3d23e958-ec86-41ea-9f3b-d3f1adf9593d', [PasswordHash] = N'AQAAAAIAAYagAAAAELFbFa64k30THemtyYDoyEZi4BvWgpsXsAd5L1zNUcHD/nO8q/4eQNMt3DANe27qZg==', [SecurityStamp] = N'342cb56d-e60c-4018-9d8b-c02a8f122863'
WHERE [Id] = N'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

UPDATE [AppDb].[Products] SET [CreatedAt] = '2024-06-21T02:23:23.4620000Z'
WHERE [Id] = 'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

INSERT INTO [AppDb].[_EFAppDbMigrationHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240621022323_user seed', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AppDb].[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AppDb].[AspNetUserRoles] ON;
INSERT INTO [AppDb].[AspNetUserRoles] ([RoleId], [UserId])
VALUES (N'dc043262-673a-491a-b811-446703743743', N'dc043262-673a-491a-b811-446703743743'),
(N'dc043262-673a-491a-b811-446703743744', N'dc043262-673a-491a-b811-446703743743');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AppDb].[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AppDb].[AspNetUserRoles] OFF;
GO

UPDATE [AppDb].[AspNetUsers] SET [ConcurrencyStamp] = N'6d3703e1-a750-4c2f-a572-de791059c363', [PasswordHash] = N'AQAAAAIAAYagAAAAEFzycSzMvKEg4Jr2ryYxKbk7Iv5hSVUW0+7kvVJfN5Vx6oNBKCoxzoey3tymxHSR+A==', [SecurityStamp] = N'8d8a59cc-fd64-4388-86ee-be6949c3cf28'
WHERE [Id] = N'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

UPDATE [AppDb].[Products] SET [CreatedAt] = '2024-06-21T02:27:28.5020950Z'
WHERE [Id] = 'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

INSERT INTO [AppDb].[_EFAppDbMigrationHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240621022728_UserRolesAdded', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DELETE FROM [AppDb].[AspNetUserRoles]
WHERE [RoleId] = N'dc043262-673a-491a-b811-446703743744' AND [UserId] = N'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

UPDATE [AppDb].[AspNetUsers] SET [ConcurrencyStamp] = N'7ffb7526-f864-4c06-ba5d-38219d7f3ba9', [PasswordHash] = N'AQAAAAIAAYagAAAAEDLnq54W7zei6uie4qinjBVArGijTl2rXqgbUp350U95twXAsMLOqFXhh0OywYFJ0A==', [SecurityStamp] = N'ba54492d-7f7a-4c74-aabb-09f33bd051d8'
WHERE [Id] = N'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

UPDATE [AppDb].[Products] SET [CreatedAt] = '2024-06-21T02:28:27.5937980Z'
WHERE [Id] = 'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

INSERT INTO [AppDb].[_EFAppDbMigrationHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240621022827_removed one role data seed', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AppDb].[AspNetUsers] SET [ConcurrencyStamp] = N'36890ad1-6eae-467e-b655-fbc0e3fe6b1d', [PasswordHash] = N'AQAAAAIAAYagAAAAEMlcIVlkUIpOBhTIphdRyJ9HcNZ818W7S97CIxi6Mnt+Yo++Ys6MlJn49JCd67Wkdg==', [SecurityStamp] = N'6f8de872-4347-4e0a-a096-acf96a7e439d'
WHERE [Id] = N'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

UPDATE [AppDb].[Products] SET [CreatedAt] = '2024-06-21T02:45:49.6449300Z'
WHERE [Id] = 'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

INSERT INTO [AppDb].[_EFAppDbMigrationHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240621024549_trying to seed user roles', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

UPDATE [AppDb].[AspNetUsers] SET [ConcurrencyStamp] = N'66f396db-fcf3-4001-99e4-2f608b5bc27b', [PasswordHash] = N'AQAAAAIAAYagAAAAELniJoL4XB9zJP3Lw3nPrcbnpJATDgi3ednHR/XA6X5y8H/+VFxG8fqm/SuFobK3dg==', [SecurityStamp] = N'00c53988-e683-4881-8246-712ed05e49d6'
WHERE [Id] = N'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AppDb].[AspNetUsers]'))
    SET IDENTITY_INSERT [AppDb].[AspNetUsers] ON;
INSERT INTO [AppDb].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (N'dc043262-673a-491a-b811-446703743744', 0, N'b4627c97-6463-4653-8a2f-e566d721cd73', N'user@example.com', CAST(1 AS bit), CAST(0 AS bit), NULL, N'USER@EXAMPLE.COM', N'USER', N'AQAAAAIAAYagAAAAEK7f7ykX61lD01YigVD9Z5ijhdjYXowqM9kmOn8kmlPbDa/2eTQtATN/Td310DrVfQ==', N'18497505945', CAST(1 AS bit), N'11830716-3b3e-4714-91e1-be31bd5aea39', CAST(0 AS bit), N'User'),
(N'dc043262-673a-491a-b811-446703743745', 0, N'fdea4919-ce4a-4e54-8078-1d03076d9a8f', N'admin2@example.com', CAST(1 AS bit), CAST(0 AS bit), NULL, N'ADMIN@EXAMPLE.COM', N'ADMIN2', N'AQAAAAIAAYagAAAAEOP3lZM5Pw8NG5WjNcM6EllaviC0JH8oEDwqJZgosEKobDpPEUHznYtALMrIShYfdw==', N'18497505936', CAST(1 AS bit), N'7b50bc0a-1b4c-4584-8ef9-30c953d955fa', CAST(0 AS bit), N'Admin2'),
(N'dc043262-673a-491a-b811-446703743746', 0, N'ec09ead2-4799-404e-a89b-3695435cf25b', N'user2@example.com', CAST(1 AS bit), CAST(0 AS bit), NULL, N'USER@EXAMPLE.COM', N'USER2', N'AQAAAAIAAYagAAAAEOjhE85YpTpXiDEfh+TB2FrT86lPFh+IbHt67G3HUPCqNgT1vcyi8B+e41tWfG6Gpg==', N'18497505937', CAST(1 AS bit), N'042101a4-9b35-479d-bcc3-5fc45147266e', CAST(0 AS bit), N'User2');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AppDb].[AspNetUsers]'))
    SET IDENTITY_INSERT [AppDb].[AspNetUsers] OFF;
GO

UPDATE [AppDb].[Products] SET [CreatedAt] = '2024-06-21T13:25:07.8363620Z', [Description] = N'Its a chair to sit', [Name] = N'Chair'
WHERE [Id] = 'dc043262-673a-491a-b811-446703743743';
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Name', N'Price', N'Stock', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[AppDb].[Products]'))
    SET IDENTITY_INSERT [AppDb].[Products] ON;
INSERT INTO [AppDb].[Products] ([Id], [CreatedAt], [Description], [Name], [Price], [Stock], [UpdatedAt])
VALUES ('dc043262-673a-491a-b811-446703743744', '2024-06-21T13:25:07.8363640Z', N'Its a table to put things on', N'Table', 200.0E0, 20, NULL),
('dc043262-673a-491a-b811-446703743745', '2024-06-21T13:25:07.8363650Z', N'Its a sofa to sit', N'Sofa', 300.0E0, 30, NULL),
('dc043262-673a-491a-b811-446703743746', '2024-06-21T13:25:07.8363650Z', N'Its a bed to sleep', N'Bed', 400.0E0, 40, NULL),
('dc043262-673a-491a-b811-446703743747', '2024-06-21T13:25:07.8363650Z', N'Its a lamp to light', N'Lamp', 500.0E0, 50, NULL),
('dc043262-673a-491a-b811-446703743748', '2024-06-21T13:25:07.8363650Z', N'Its a curtain to cover', N'Curtains', 600.0E0, 60, NULL),
('dc043262-673a-491a-b811-446703743749', '2024-06-21T13:25:07.8363650Z', N'Its a carpet to walk', N'Carpet', 700.0E0, 70, NULL),
('dc043262-673a-491a-b811-446703743750', '2024-06-21T13:25:07.8363660Z', N'Its a painting to see', N'Painting', 800.0E0, 80, NULL),
('dc043262-673a-491a-b811-446703743751', '2024-06-21T13:25:07.8363660Z', N'Its a mirror to reflect', N'Mirror', 900.0E0, 90, NULL),
('dc043262-673a-491a-b811-446703743752', '2024-06-21T13:25:07.8363660Z', N'Its a vase to hold', N'Vase', 1000.0E0, 100, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'Name', N'Price', N'Stock', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[AppDb].[Products]'))
    SET IDENTITY_INSERT [AppDb].[Products] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AppDb].[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AppDb].[AspNetUserRoles] ON;
INSERT INTO [AppDb].[AspNetUserRoles] ([RoleId], [UserId])
VALUES (N'dc043262-673a-491a-b811-446703743744', N'dc043262-673a-491a-b811-446703743744'),
(N'dc043262-673a-491a-b811-446703743744', N'dc043262-673a-491a-b811-446703743745'),
(N'dc043262-673a-491a-b811-446703743743', N'dc043262-673a-491a-b811-446703743746');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AppDb].[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AppDb].[AspNetUserRoles] OFF;
GO

INSERT INTO [AppDb].[_EFAppDbMigrationHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240621132508_addedNewDataSeed', N'8.0.6');
GO

COMMIT;
GO

