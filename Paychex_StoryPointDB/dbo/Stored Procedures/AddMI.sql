CREATE USER [stm-identity-mi-east-n2b] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [stm-identity-mi-east-n2b];
ALTER ROLE db_datawriter ADD MEMBER [stm-identity-mi-east-n2b];
ALTER ROLE db_ddladmin ADD MEMBER [stm-identity-mi-east-n2b];
GO