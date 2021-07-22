FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu
RUN mkdir -p /usr/work
WORKDIR /usr/work
COPY /sql/db/mssql/ /usr/work/

EXPOSE 1433

CMD /bin/bash ./entrypoint.sh
