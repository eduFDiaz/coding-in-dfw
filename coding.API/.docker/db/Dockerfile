FROM mysql:5.7

COPY initial.sql initial.sql

COPY docker-entrypoint.sh docker-entrypoint.sh

RUN chmod +x docker-entrypoint.sh

ENTRYPOINT [ "docker-entrypoint.sh" ]

