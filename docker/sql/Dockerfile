FROM mcr.microsoft.com/mssql/rhel/server:2019-CU1-rhel-8
RUN mkdir -p /usr/work
WORKDIR /usr/work
COPY . /usr/work/
#EXPOSE 1433

#CMD /bin/bash ./entrypoint.sh
#COPY import-data.sh /import-data.sh
#COPY entrypoint.sh /entrypoint.sh
#ENTRYPOINT ["/entrypoint.sh"]

COPY /sql/import-data.sh /usr/work/
#COPY /sql/entrypoint.sh /usr/work/
COPY /sql/create_database.sql /usr/work/

# Create app directory
#RUN mkdir -p /usr/src/app
#WORKDIR /usr/src/app

# Copy initialization scripts
#COPY . /usr/src/app

# Grant permissions for the run-initialization script to be executable
#RUN chmod +x /usr/work/import-data.sh

# Set environment variables, not to have to write them with docker run command
# Note: make sure that your password matches what is in the run-initialization script 
#ENV SA_PASSWORD MeuDB@123
#ENV ACCEPT_EULA Y
#ENV MSSQL_PID Developer

# Expose port 1433 in case accesing from other container
EXPOSE 1433

# Run Microsoft SQl Server and initialization script (at the same time)
# Note: If you want to start MsSQL only (without initialization script) you can comment bellow line out, CMD entry from base image will be taken
#CMD /bin/bash ./entrypoint.sh

#RUN /opt/mssql/bin/sqlservr
#RUN /usr/work/import-data.sh & /opt/mssql/bin/sqlservr
#aguardando 90 segundos para aguardar o provisionamento e start do banco
#RUN sleep 90s
#CMD ["sleep", "90"]
#rodar o comando para criar o banco
#RUN /opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U SA -P "MeuDB@123" -i create_database.sql
#RUN /opt/mssql/bin/sqlservr
RUN (/opt/mssql/bin/sqlservr --accept-eula Y ) &&  /opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U SA -P "MeuDB@123" -i create_database.sql
