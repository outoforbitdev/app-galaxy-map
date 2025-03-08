#! /bin/bash

# su postgres
PGPASSWORD=password psql -U postgres -f /data/db/create-db.sql
PGPASSWORD=password psql -U postgres -d galaxy -f /data/db/migration.sql
# PGPASSWORD=password psql -U postgres -d galaxy -f /data/db/add-db-data.sql
sh /data/db/add-db-data.sh
PGPASSWORD=password psql -U postgres -d galaxy -c 'GRANT SELECT ON ALL TABLES IN SCHEMA public TO app_galaxy_map_user;'
