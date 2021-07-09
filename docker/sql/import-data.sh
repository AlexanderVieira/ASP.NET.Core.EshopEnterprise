#aguardando 90 segundos para aguardar o provisionamento e start do banco
sleep 90s
#rodar o comando para criar o banco
/opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U SA -P "MeuDB@123" -i criacao-banco-docker.sql

SELECT name AS 'User Created Databases' FROM SYS.DATABASES WHERE name NOT IN ('master', 'model', 'msdb', 'tempdb', 'Resource', 'distribution' , 'reportserver', 'reportservertempdb')